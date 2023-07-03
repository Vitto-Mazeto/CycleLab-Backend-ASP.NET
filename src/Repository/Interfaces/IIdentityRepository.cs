using Microsoft.AspNetCore.Identity;

namespace Repository.Interfaces
{
    public interface IIdentityRepository
    {

        /// <summary>
        /// Cria um novo usuário assincronamente.
        /// </summary>
        /// <returns>O resultado da criação do usuário.</returns>
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);

        /// <summary>
        /// Adiciona um usuário a uma função (role) assincronamente.
        /// </summary>
        Task AddToRoleAsync(IdentityUser user, string role);

        /// <summary>
        /// Busca um usuário pelo seu e-mail assincronamente.
        /// </summary>
        /// <returns>O usuário encontrado.</returns>
        Task<IdentityUser> FindByEmailAsync(string email);

        /// <summary>
        /// Verifica se um usuário pertence a uma função (role) assincronamente.
        /// </summary>
        /// <returns>Um valor booleano indicando se o usuário pertence à função.</returns>
        Task<bool> IsInRoleAsync(IdentityUser user, string role);

        /// <summary>
        /// Remove um usuário de uma função (role) assincronamente.
        /// </summary>
        Task RemoveFromRoleAsync(IdentityUser user, string role);

        /// <summary>
        /// Deleta um usuário assincronamente.
        /// </summary>
        /// <returns>O resultado da operação de deleção.</returns>
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);

        /// <summary>
        /// Obtém uma lista de todos os usuários assincronamente.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        Task<List<IdentityUser>> GetUsersAsync();

        /// <summary>
        /// Obtém as funções (roles) do usuário assincronamente.
        /// </summary>
        /// <returns>Uma lista de funções do usuário.</returns>
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}
