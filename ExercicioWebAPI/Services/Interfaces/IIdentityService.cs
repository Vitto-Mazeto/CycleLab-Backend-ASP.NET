using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ExercicioWebAPI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel userRegister);
        Task<UserLoginResponse> Login(UserLoginViewModel userLogin);
        Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync();
        Task<bool> AlterarPermissaoUsuarioAsync(string login);
        Task<bool> RemoverUsuarioAsync(string login);

    }
}
