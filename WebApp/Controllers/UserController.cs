using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        // GET
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var userViewModel = new UserViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                Username = user.UserName,
                RoleName = string.Join(",", roles)
            };
            return View(userViewModel);
        }

        [Authorize]
        public async Task<IActionResult> LoggedInDetail()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            var userViewModel = new UserViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                Username = user.UserName,
                RoleName = string.Join(",", roles)
            };
            return View(userViewModel);
        }
        
        [Authorize(Roles = "ADMIN")]
        public  IActionResult AssignRole()
        {
            PopulateDropdownRoles();
            return View();
        }

        [HttpPost("user/assignRole/{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AssignRole(string userId,Role roleModel)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, roleModel.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Detail", "User", new {userId});
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        private void PopulateDropdownRoles()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.RoleName = new SelectList(roles, "Name", "Name");
        }
    }
}