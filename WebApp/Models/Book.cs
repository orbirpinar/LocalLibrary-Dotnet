using System.Collections.Generic;

namespace WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Isbn { get; set; }
        public Author? Author { get; set; }
        
        public int AuthorId { get; set; }

        public string? SmallCoverLink { get; set; }

        public string? MediumCoverLink { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public Language? Language { get; set; }
        public IEnumerable<BookInstance>? Instances { get ; set; }
    }
}
