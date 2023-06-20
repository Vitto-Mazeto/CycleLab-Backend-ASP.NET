using Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddToRoleAsync(IdentityUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            await _userManager.SetLockoutEnabledAsync(user, enabled);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task RemoveFromRoleAsync(IdentityUser user, string role)
        {
            await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
