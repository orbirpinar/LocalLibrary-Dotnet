using AutoMapper;
using WebApp.Consumer.SeedData;
using WebApp.Models;

namespace WebApp.Mapping
{
    public class SeederProfile: Profile
    {
        public SeederProfile()
        {
            CreateMap<GenreSeed, Genre>();
            CreateMap<BookSeed, Book>();
            CreateMap<AuthorSeed, Author>();
        }
    }
}