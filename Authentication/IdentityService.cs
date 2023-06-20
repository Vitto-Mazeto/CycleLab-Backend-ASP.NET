using AutoMapper;
using DTOs.Responses;
using DTOs.ViewModels;
using Repository.Interfaces;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Authentication
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            IIdentityRepository identityRepository)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _identityRepository = identityRepository;
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel userRegister)
        {
            var identityUser = _mapper.Map<IdentityUser>(userRegister);

            var result = await _identityRepository.CreateUserAsync(identityUser, userRegister.Senha);
            if (result.Succeeded)
            {
                var roleName = userRegister.IsAdmin ? "ADMIN" : "USER";
                await _identityRepository.AddToRoleAsync(identityUser, roleName);
            }

            var userRegisterResponse = _mapper.Map<UserRegisterResponse>(result);
            if (!result.Succeeded && result.Errors.Count() > 0)
                userRegisterResponse.AdicionarErros(result.Errors.Select(r => r.Description));

            return userRegisterResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Senha, false, true);
            if (result.Succeeded)
            {
                var user = await _identityRepository.FindByEmailAsync(userLogin.Email);
                var tokenResponse = await _tokenService.GenerateToken(user);
                return new UserLoginResponse(true, tokenResponse.Token, tokenResponse.DataExpiracao);
            }

            var userLoginResponse = _mapper.Map<UserLoginResponse>(result);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    userLoginResponse.AddError("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    userLoginResponse.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    userLoginResponse.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    userLoginResponse.AddError("Usuário ou senha estão incorretos");
            }

            return userLoginResponse;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync()
        {
            var users = await _identityRepository.GetUsersAsync();

            var usersWithRoles = users.Select(async user =>
            {
                var roles = await _identityRepository.GetRolesAsync(user);

                return new UserResponseDto
                {
                    Login = user.UserName,
                    Roles = roles.ToList()
                };
            });

            return await Task.WhenAll(usersWithRoles);
        }

        public async Task<bool> ChangeUserPermissionAsync(string login)
        {
            var user = await _identityRepository.FindByEmailAsync(login);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var isAdmin = await _identityRepository.IsInRoleAsync(user, "ADMIN");

            if (isAdmin)
            {
                await _identityRepository.RemoveFromRoleAsync(user, "ADMIN");
                await _identityRepository.AddToRoleAsync(user, "USER");
            }
            else
            {
                await _identityRepository.RemoveFromRoleAsync(user, "USER");
                await _identityRepository.AddToRoleAsync(user, "ADMIN");
            }

            return true;
        }

        public async Task<bool> RemoveUserAsync(string login)
        {
            var user = await _identityRepository.FindByEmailAsync(login);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var result = await _identityRepository.DeleteUserAsync(user);

            return result.Succeeded;
        }
    }
}
