using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Dto
{
    public class BookViewDto
    {
        
        public int Id { get; set; }
        
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Isbn { get; set; }
        
        public string? SmallCoverLink { get; set; }

        public string? MediumCoverLink { get; set; }

        public ICollection<Genre> Genres { get; set; } = default!;
        public Language? Language { get; set; }

        public IEnumerable<BookInstance>? Instances { get; set; }
    }
}