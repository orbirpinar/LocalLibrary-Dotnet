using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories.Implementations
{
    public class BookInstanceRepository: IBookInstanceRepository,IDisposable
    {

        private readonly LibraryContext _context;

        public BookInstanceRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookInstance>> GetAllAsync()
        {
            return await _context.BookInstances.ToListAsync();
        }

        public async Task<BookInstance?> GetByIdAsync(Guid id)
        {
            return await _context.BookInstances.FindAsync(id);
        }

        public async Task CreateAsync(BookInstance bookInstance)
        {
            await _context.BookInstances.AddAsync(bookInstance);
        }

        public void Update(BookInstance bookInstance)
        {
            _context.Entry(bookInstance).State = EntityState.Modified;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var bookInstance = await GetByIdAsync(id);
            _context.Remove(bookInstance);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<BookInstance?> GetWithBookAndBorrowerById(Guid id)
        {
            return await _context.BookInstances
                .Include(bi => bi.Book)
                .Include(bi => bi.Borrower)
                .FirstAsync();
        }

        public async Task<IEnumerable<BookInstance>> GetByBookId(int bookId)
        {
            return await _context.BookInstances.Where(b => b.Book.Id == bookId)
                .ToListAsync();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}