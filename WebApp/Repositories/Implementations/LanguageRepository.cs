using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories.Implementations
{
    public class LanguageRepository : ILanguageRepository, IDisposable
    {
        private readonly LibraryContext _context;

        public LanguageRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Language.ToListAsync();
        }

        public async Task<Language?> GetByIdAsync(int id)
        {
            return await _context.Language.FindAsync(id);
        }

        public async Task CreateAsync(Language language)
        {
            await _context.AddAsync(language);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var language = await _context.Language.FindAsync(id);
            if (language is not null)
            {
                _context.Language.Remove(language);
            }
        }

        public void Update(Language language)
        {
            _context.Entry(language).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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