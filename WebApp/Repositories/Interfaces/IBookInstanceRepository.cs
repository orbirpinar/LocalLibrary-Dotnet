using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories.Interfaces
{
    public interface IBookInstanceRepository
    {
        
        Task<IEnumerable<BookInstance>> GetAllAsync();
        Task<BookInstance?> GetByIdAsync(Guid id);
        Task CreateAsync(BookInstance bookInstance);
        void Update(BookInstance bookInstance);
        Task<bool> DeleteByIdAsync(Guid id);
        Task SaveAsync();
        Task<BookInstance?> GetWithBookAndBorrowerById(Guid id);

        Task<IEnumerable<BookInstance>> GetByBookId(int bookId);

        Task<int> GetCountAvailableAsync();

        Task<int> GetCountAsync();
        
    }
}