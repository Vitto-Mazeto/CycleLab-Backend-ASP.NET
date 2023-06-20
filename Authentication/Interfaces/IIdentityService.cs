using DTOs.Responses;
using DTOs.ViewModels;

namespace Authentication.Interfaces
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel userRegister);
        Task<UserLoginResponse> Login(UserLoginViewModel userLogin);
        Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync();
        Task<bool> ChangeUserPermissionAsync(string login);
        Task<bool> RemoveUserAsync(string login);

    }
}
