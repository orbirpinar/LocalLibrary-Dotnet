using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Library;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories.Implementations
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Instances)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Instances)
                .Where(b => b.Id == id)
                .FirstAsync();
        }

        public async Task CreateAsync(Book book)
        {
            await _context.Book.AddAsync(book);
        }

        public async Task CreateOrUpdateAsync(Book book)
        {
            var bookExists =  _context.Book.AnyAsync(b => b.Title == book.Title).Result;
            if (!bookExists)
            {
                await _context.Book.AddAsync(book);
            }
            
        }

        public void Update(int id, Book book)
        {
            _context.Book.Update(book);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book!);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Book.CountAsync();
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