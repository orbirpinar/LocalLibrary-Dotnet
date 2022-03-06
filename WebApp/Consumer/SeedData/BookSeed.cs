using System.Collections.Generic;
using WebApp.Consumer.SeedData;
using WebApp.Models;

namespace WebApp.Consumer
{
    public class BookSeed
    {
        
        
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string? Isbn { get; set; }
        public ICollection<GenreSeed> Genres { get; set; }
    }
}