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
    internal sealed class BookInstanceRepository : IBookInstanceRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public BookInstanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookInstance>> GetAllAsync()
        {
            return await _context.BookInstance.ToListAsync();
        }

        public async Task<BookInstance?> GetByIdAsync(Guid id)
        {
            return await _context.BookInstance.FindAsync(id);
        }

        public async Task CreateAsync(BookInstance bookInstance)
        {
            await _context.BookInstance.AddAsync(bookInstance);
        }

        public void Update(BookInstance bookInstance)
        {
            _context.Entry(bookInstance).State = EntityState.Modified;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var bookInstance = await GetByIdAsync(id);
            if (bookInstance is null) return false;
            _context.Remove(bookInstance);
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<BookInstance?> GetWithBookAndBorrowerById(Guid id)
        {
            return await _context.BookInstance
                .Include(bi => bi.Book)
                .Include(bi => bi.Borrower)
                .Where(bi => bi.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookInstance>> GetByBookId(int bookId)
        {
            return await _context.BookInstance.Where(b => b.Book!.Id == bookId)
                .ToListAsync();
        }

        public async Task<int> GetCountAvailableAsync()
        {
            return await _context.BookInstance
                .Where(b => b.LoanStatus == LoanStatus.Available)
                .CountAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.BookInstance.CountAsync();
        }

        private bool _disposed;

        private void Dispose(bool disposing)
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
            Dispose(true);
        }
    }
}