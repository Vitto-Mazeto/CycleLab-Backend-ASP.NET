using DTOs.Responses;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Gera um token de autenticação para um usuário.
        /// </summary>
        /// <returns>Resposta contendo o token gerado e a data de expiração do token.</returns>
        Task<TokenResponseDto> GenerateToken(IdentityUser user);
    }
}
