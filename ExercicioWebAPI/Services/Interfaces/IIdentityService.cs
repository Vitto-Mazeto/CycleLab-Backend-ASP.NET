using ExercicioWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ExercicioWebAPI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
        Task<UserLoginResponse> Login(UserLoginRequest userLogin);
        Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync();
        Task<bool> AlterarPermissaoUsuarioAsync(string login);
        Task<bool> RemoverUsuarioAsync(string login);

    }
}
