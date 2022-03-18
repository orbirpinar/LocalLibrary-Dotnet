using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Library;
using WebApp.Dto;
using WebApp.Models;

namespace WebApp.Seeder
{
    public class DatabaseSeederRepository : ISeederRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public DatabaseSeederRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeedData?> SaveAsync(SeedData? seedData)
        {
            if (seedData is null) return seedData;
            var book = _mapper.Map<Book>(seedData.Book);
            if (CheckBookExists(book))
            {
                return null;
            }

            var author = _mapper.Map<Author>(seedData.Author);
            await SetGenres(seedData,book);


            book.Author = author;
            if (CheckAuthorExists(author))
            {
                var existingAuthor = await _context.Author.FirstAsync(a => a.Name == author.Name);
                book.Author = existingAuthor;
            }

            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            return seedData;
        }

        private async Task SetGenres(SeedData? seedData,Book book)
        {
            var listOfGenre = new List<Genre>();
            foreach (var bookGenre in seedData!.Book.Genres)
            {
                var existingGenre = await _context.Genre.FirstOrDefaultAsync(g => g.Name == bookGenre.Name);
                if (existingGenre != null)
                {
                    listOfGenre.Add(existingGenre);
                }
                else
                {
                    listOfGenre.Add(new Genre
                    {
                        Name = bookGenre.Name
                    });
                }
            }
            book.Genres = listOfGenre;
        }

        private bool CheckAuthorExists(Author author)
        {
            return _context.Author.Any(a => a.Name == author.Name);
        }

        private bool CheckBookExists(Book book)
        {
            return _context.Book.Any(b => b.Title == book.Title);
        }
    }
}