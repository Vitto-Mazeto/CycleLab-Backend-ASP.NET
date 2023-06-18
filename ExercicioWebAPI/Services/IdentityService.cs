using ExercicioWebAPI.Configurations;
using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using ExercicioWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExercicioWebAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
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
                return await GerarToken(usuarioLogin.Email);

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

        private async Task<UserLoginResponse> GerarToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var tokenClaims = await ObterClaims(user);

            var dataExpiracao = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserLoginResponse
            (
                sucesso: true,
                token: token,
                dataExpiracao: dataExpiracao
            );
        }

        private async Task<IList<Claim>> ObterClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            //Claims padrões para ter no token
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            //Papel do usuario
            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}
