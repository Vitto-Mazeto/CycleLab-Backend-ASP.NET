using ExercicioWebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Identity;

namespace ExercicioWebAPI.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GerarToken(IdentityUser user);
    }
}
