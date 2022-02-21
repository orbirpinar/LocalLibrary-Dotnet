using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository, IDisposable
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task CreateAsync(Author author)
        {
            await _context.AddAsync(author);
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }

        public async void DeleteByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is not null)
            {
                _context.Remove(author);
            }
        }

        public async Task<Author?> GetWithBooksAndInstancesByIdAsync(int id)
        {
            return await _context.Authors
                .Include(author => author.Books)
                .ThenInclude(book => book.Instances)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Authors.CountAsync();
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