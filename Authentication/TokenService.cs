using ExercicioWebAPI.Configurations;
using DTOs.Responses;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentication
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenService(IOptions<JwtOptions> jwtOptions, UserManager<IdentityUser> userManager)
        {
            // Dessa forma consigo acessar os valores salvos no appsettings.json
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<TokenResponseDto> GenerateToken(IdentityUser user)
        {
            IList<Claim> tokenClaims = await GetClaims(user);

            DateTime dataExpiracao = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponseDto(token, dataExpiracao);
        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            //Claims padrões para ter no token
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id)); 
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            //Papéis do usuario
            foreach (string role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}
