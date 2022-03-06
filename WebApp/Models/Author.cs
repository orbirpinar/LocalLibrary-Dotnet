using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }


        [Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DateOfDeath { get; set; }
        
        public String? Info { get; set; }

        public IEnumerable<Book>? Books { get; set; }

    }
}
