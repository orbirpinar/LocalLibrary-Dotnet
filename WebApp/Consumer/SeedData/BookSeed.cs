using System.Collections.Generic;

namespace WebApp.Consumer.SeedData
{
    public sealed class BookSeed
    {
        
        
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string? Isbn { get; set; }
        
        public string? SmallCoverLink { get; set; }

        public string? MediumCoverLink { get; set; }
        public ICollection<GenreSeed> Genres { get; set; }
    }
}