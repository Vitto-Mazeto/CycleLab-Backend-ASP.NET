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
            IdentityUser identityUser = _mapper.Map<IdentityUser>(userRegister);

            IdentityResult result = await _identityRepository.CreateUserAsync(identityUser, userRegister.Senha);
            if (result.Succeeded)
            {
                string roleName = userRegister.IsAdmin ? "ADMIN" : "USER";
                await _identityRepository.AddToRoleAsync(identityUser, roleName);
            }

            UserRegisterResponse userRegisterResponse = _mapper.Map<UserRegisterResponse>(result);
            if (!result.Succeeded && result.Errors.Count() > 0)
                userRegisterResponse.AdicionarErros(result.Errors.Select(r => r.Description));

            return userRegisterResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Senha, false, true);
            if (result.Succeeded)
            {
                IdentityUser user = await _identityRepository.FindByEmailAsync(userLogin.Email);
                TokenResponseDto tokenResponse = await _tokenService.GenerateToken(user);
                return new UserLoginResponse(true, tokenResponse.Token, tokenResponse.DataExpiracao);
            }

            UserLoginResponse userLoginResponse = _mapper.Map<UserLoginResponse>(result);
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
            List<IdentityUser> users = await _identityRepository.GetUsersAsync();

            List<UserResponseDto> usersWithRoles = new List<UserResponseDto>();

            foreach (var user in users)
            {
                IList<string> roles = await _identityRepository.GetRolesAsync(user);

                var userResponseDto = new UserResponseDto
                {
                    Login = user.UserName,
                    Roles = roles.ToList()
                };

                usersWithRoles.Add(userResponseDto);
            }

            return usersWithRoles;
        }

        public async Task<bool> ChangeUserPermissionAsync(string login)
        {
            IdentityUser user = await _identityRepository.FindByEmailAsync(login);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            bool isAdmin = await _identityRepository.IsInRoleAsync(user, "ADMIN");

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
            IdentityUser user = await _identityRepository.FindByEmailAsync(login);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            IdentityResult result = await _identityRepository.DeleteUserAsync(user);

            return result.Succeeded;
        }
    }
}
