using AutoMapper;
using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using ExercicioWebAPI.Repository.Interfaces;
using ExercicioWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercicioWebAPI.Services
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

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel usuarioCadastro)
        {
            var identityUser = _mapper.Map<IdentityUser>(usuarioCadastro);

            var result = await _identityRepository.CreateUserAsync(identityUser, usuarioCadastro.Senha);
            if (result.Succeeded)
            {
                var roleName = usuarioCadastro.IsAdmin ? "ADMIN" : "USER";
                await _identityRepository.AddToRoleAsync(identityUser, roleName);
                await _identityRepository.SetLockoutEnabledAsync(identityUser, false);
            }

            var usuarioCadastroResponse = _mapper.Map<UserRegisterResponse>(result);
            if (!result.Succeeded && result.Errors.Count() > 0)
                usuarioCadastroResponse.AdicionarErros(result.Errors.Select(r => r.Description));

            return usuarioCadastroResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);
            if (result.Succeeded)
            {
                var user = await _identityRepository.FindByEmailAsync(usuarioLogin.Email);
                var tokenResponse = await _tokenService.GerarToken(user);
                return new UserLoginResponse(true, tokenResponse.Token, tokenResponse.DataExpiracao);
            }

            var usuarioLoginResponse = _mapper.Map<UserLoginResponse>(result);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    usuarioLoginResponse.AdicionarErro("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    usuarioLoginResponse.AdicionarErro("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    usuarioLoginResponse.AdicionarErro("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    usuarioLoginResponse.AdicionarErro("Usuário ou senha estão incorretos");
            }

            return usuarioLoginResponse;
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

        public async Task<bool> AlterarPermissaoUsuarioAsync(string login)
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

        public async Task<bool> RemoverUsuarioAsync(string login)
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
