using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApp.Dto;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Seeder
{
    public class DatabaseSeederRepository : ISeederRepository
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public DatabaseSeederRepository(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task SaveAll(List<SeedData>? seedDataList)
        {
            foreach (var seedData in seedDataList!)
            {
                var book = _mapper.Map<Book>(seedData.Book);
                var author = _mapper.Map<Author>(seedData.Author);
                var genres = _mapper.Map<List<Genre>>(seedData.Book.Genres);
                book.Author = author;
                book.Genres = genres;
                await _bookRepository.CreateAsync(book);
                await _bookRepository.SaveAsync();
            }
        }
    }
}