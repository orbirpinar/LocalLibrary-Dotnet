using WebApp.Dto;
using WebApp.Models;

namespace WebApp.Mapping
{
    public class LibraryProfile: AutoMapper.Profile
    {
        public LibraryProfile()
        {
            CreateMap<Author, AuthorDetailDto>();
            CreateMap<Book, BookViewDto>();

        }
    }
}