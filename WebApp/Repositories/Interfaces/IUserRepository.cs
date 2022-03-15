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
        Task<IdentityResult> CreateAsync(User user,string password);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteByIdAsync(string id);

        Task<User> GetByEmailAsync(string email);

        Task<IdentityResult> AssignRoleAsync(User user, string roleName);

        Task<IList<string>> GetRoleNamesAsync(User user);

        Task<User> GetLoggedInUser(ClaimsPrincipal user);

    }
}