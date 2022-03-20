using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.ViewModel;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userRepository.GetRoleNamesAsync(user);
            var userViewModel = new UserViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                Username = user.UserName,
                RoleNames = roles
            };
            return View(userViewModel);
        }

        [Authorize]
        public async Task<IActionResult> LoggedInDetail()
        {

            var user = await _userRepository.GetLoggedInUser(User);
            var roles = await _userRepository.GetRoleNamesAsync(user);
            var userViewModel = new UserViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                Username = user.UserName,
                RoleNames = roles
            };
            return View(userViewModel);
        }
        
        [Authorize(Roles = "ADMIN")]
        public async  Task<IActionResult> AssignRole()
        {
            await PopulateDropdownRoles();
            return View();
        }

        [HttpPost("user/assignRole/{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AssignRole(string userId,Role roleModel)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }
            var result = await _userRepository.AssignRoleAsync(user, roleModel.Name);
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

        private async Task PopulateDropdownRoles()
        {
            var roles = await _roleRepository.GetAllAsync();
            ViewBag.RoleName = new SelectList(roles, "Name", "Name");
        }
    }
}