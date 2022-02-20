using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public ICollection<BookInstance> Instances { get; set; }
    }
}