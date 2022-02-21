using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        
        Task<IEnumerable<IdentityRole>> GetAllAsync();
        Task<IdentityRole?> GetByIdAsync(string id);
        Task<IdentityResult> CreateAsync(IdentityRole role);
        Task<bool> UpdateAsync(IdentityRole role);
        Task<bool> DeleteByIdAsync(string id);
    }
}