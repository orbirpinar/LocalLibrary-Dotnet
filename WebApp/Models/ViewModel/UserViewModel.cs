using System.Collections.Generic;

namespace WebApp.Models.ViewModel
{
    public class UserViewModel
    {
        public string Email { get; set; } = default!;
        public string? FullName { get; set; }
        public string Username { get; set; } = default!;
        public IList<string> RoleNames { get; set; } = default!;
    }
}