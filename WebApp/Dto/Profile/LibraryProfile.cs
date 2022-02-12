using WebApp.Models;

namespace WebApp.Dto.Profile
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