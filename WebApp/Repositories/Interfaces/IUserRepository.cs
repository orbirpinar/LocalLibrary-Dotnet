using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteByIdAsync(string id);

        Task<IdentityResult> AssignRoleAsync(User user, string roleName);

        Task<IList<string>> GetRoleNamesAsync(User user);

        Task<User> GetLoggedInUser(ClaimsPrincipal user);

    }
}