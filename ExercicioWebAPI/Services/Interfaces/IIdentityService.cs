using ExercicioWebAPI.Models.DTOs;

namespace ExercicioWebAPI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
        Task<UserLoginResponse> Login(UserLoginRequest userLogin);
        Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync();

    }
}
