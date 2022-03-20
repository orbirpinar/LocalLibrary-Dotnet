using Microsoft.Build.Framework;

namespace WebApp.Models.UpdateModel
{
    public class UpdateUserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required] public string Email { get; set; } = null!;

        [Required] public string Username { get; set; } = null!;
        
        public string? PhoneNumber { get; set; }
    }
}