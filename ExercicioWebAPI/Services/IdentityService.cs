using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using ExercicioWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExercicioWebAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel usuarioCadastro)
        {
            var identityUser = new IdentityUser
            {
                UserName = usuarioCadastro.Email,
                Email = usuarioCadastro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, usuarioCadastro.Senha);
            if (result.Succeeded)
            {
                if (usuarioCadastro.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(identityUser, "ADMIN");
                }
                else
                {
                    await _userManager.AddToRoleAsync(identityUser, "USER");
                }

                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }

            var usuarioCadastroResponse = new UserRegisterResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
                usuarioCadastroResponse.AdicionarErros(result.Errors.Select(r => r.Description));

            return usuarioCadastroResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginViewModel usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(usuarioLogin.Email);
                var tokenResponse = await _tokenService.GerarToken(user);
                return new UserLoginResponse(true, tokenResponse.Token, tokenResponse.DataExpiracao);
            }

            var usuarioLoginResponse = new UserLoginResponse(result.Succeeded);
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
            var users = await _userManager.Users.ToListAsync();

            var usersWithRoles = new List<UserResponseDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userWithRole = new UserResponseDto
                {
                    Login = user.UserName,
                    Roles = roles.ToList()
                };

                usersWithRoles.Add(userWithRole);
            }

            return usersWithRoles;
        }

        public async Task<bool> AlterarPermissaoUsuarioAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");

            if (isAdmin)
            {
                await _userManager.RemoveFromRoleAsync(user, "ADMIN");
                await _userManager.AddToRoleAsync(user, "USER");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "USER");
                await _userManager.AddToRoleAsync(user, "ADMIN");
            }

            return true;
        }

        public async Task<bool> RemoverUsuarioAsync(string login)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}
