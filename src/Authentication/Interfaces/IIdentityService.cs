using DTOs.Responses;
using DTOs.ViewModels;

namespace Authentication.Interfaces
{
    public interface IIdentityService
    {
        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <returns>Resposta contendo os detalhes do registro do usuário.</returns>
        Task<UserRegisterResponse> RegisterUser(UserRegisterViewModel userRegister);

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <returns>Resposta contendo os detalhes do login do usuário e o token.</returns>
        Task<UserLoginResponse> Login(UserLoginViewModel userLogin);

        /// <summary>
        /// Obtém a lista de usuários com suas respectivas roles.
        /// </summary>
        /// <returns>Uma lista de DTOs contendo os usuários e suas roles.</returns>
        Task<IEnumerable<UserResponseDto>> GetUsersWithRolesAsync();

        /// <summary>
        /// Altera a permissão de um usuário entre ADMIN e USER.
        /// </summary>
        /// <returns>True se a alteração de permissão for bem-sucedida, False caso contrário.</returns>
        Task<bool> ChangeUserPermissionAsync(string login);

        /// <summary>
        /// Remove um usuário.
        /// </summary>
        /// <returns>True se a remoção do usuário for bem-sucedida, False caso contrário.</returns>
        Task<bool> RemoveUserAsync(string login);

    }
}
