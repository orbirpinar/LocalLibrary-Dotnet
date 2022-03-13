using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Dto
{
    public class AuthorDetailDto
    {
        
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }
        
        public string? Info { get; set; }

        public IEnumerable<BookViewDto> Books { get; set; }
    }
}