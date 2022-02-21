using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task CreateAsync(Author author);
        void Update(Author author);
        void DeleteByIdAsync(int id);

        Task<Author?> GetWithBooksAndInstancesByIdAsync(int id);
        Task SaveAsync();

        Task<int> GetCountAsync();
    }
}