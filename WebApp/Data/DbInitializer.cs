using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Data
{
    public static class DbInitializer
    {
        public static  void SeedUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (userManager.Users.Any(user => user.UserName == "admin"))
            {
                return;
            }

            var admin = new User()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var result = userManager.CreateAsync(admin, "123_Secret").Result;

            if (!result.Succeeded) return;
            var roleResult = roleManager.CreateAsync(new IdentityRole
            {
                Name = "ADMIN"
            }).Result;
            if (roleResult.Succeeded)
            {
                userManager.AddToRoleAsync(admin, "ADMIN");
            }
        }
    }
}