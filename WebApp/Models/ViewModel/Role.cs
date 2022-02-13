
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class Role
    {
        public string? Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}