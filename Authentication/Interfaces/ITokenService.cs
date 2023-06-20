using DTOs.Responses;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GerarToken(IdentityUser user);
    }
}
