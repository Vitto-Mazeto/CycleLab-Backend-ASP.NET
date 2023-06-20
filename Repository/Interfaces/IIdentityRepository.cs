using Microsoft.AspNetCore.Identity;

namespace Repository.Interfaces
{
    public interface IIdentityRepository
    {
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task AddToRoleAsync(IdentityUser user, string role);
        Task SetLockoutEnabledAsync(IdentityUser user, bool enabled);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<bool> IsInRoleAsync(IdentityUser user, string role);
        Task RemoveFromRoleAsync(IdentityUser user, string role);
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
        Task<List<IdentityUser>> GetUsersAsync();
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}
