using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Data.Identity
{
    public class AuthContextSeed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthContextSeed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAdminUser()
        {
                var user = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                if (!_roleManager.Roles.Any(r => r.Name == "ADMIN"))
                {
                    await _roleManager.CreateAsync(new IdentityRole {Name = "ADMIN"});
                }

                if (!_userManager.Users.Any(u => u.UserName == user.UserName))
                {
                    await _userManager.CreateAsync(user, "123_Secret");
                    await _userManager.AddToRoleAsync(user, "ADMIN");
                }
        }
    }
}