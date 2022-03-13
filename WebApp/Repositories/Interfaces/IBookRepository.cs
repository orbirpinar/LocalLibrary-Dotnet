using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task CreateAsync(Book book);

        Task CreateOrUpdateAsync(Book book);
        void Update(int id, Book book);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();

        Task<int> GetCountAsync();
    }
}