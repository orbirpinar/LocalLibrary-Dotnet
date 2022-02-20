using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language?> GetByIdAsync(int id);
        Task CreateAsync(Language language);
        Task DeleteByIdAsync(int id);
        void Update(Language language);

        Task SaveAsync();
    }
}