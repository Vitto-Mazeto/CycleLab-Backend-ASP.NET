using DTOs.Responses;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GenerateToken(IdentityUser user);
    }
}
