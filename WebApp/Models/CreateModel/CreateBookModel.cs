using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.CreateModel
{
    public class CreateBookModel
    {
        [Required]
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Isbn { get; set; }
        public int AuthorId { get; set; }
        
        public int LanguageId { get; set; }
    }
}