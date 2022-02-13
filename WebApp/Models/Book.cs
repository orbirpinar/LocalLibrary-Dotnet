using System.Collections.Generic;

namespace WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string? Isbn { get; set; }
        public Author? Author { get; set; }
        public ICollection<Genre> genres { get; set; }
        public Language language { get; set; }

        public IEnumerable<BookInstance>? Instances { get; set; }

    }
}