using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModel;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleRepository.GetAllAsync();
            return View(roles);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role roleModel)
        {
            if (!ModelState.IsValid) return View();
            var role = new IdentityRole
            {
                Name = roleModel.Name
            };
            var result = await _roleRepository.CreateAsync(role);
            if (result.Succeeded)
            {
                return Redirect(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}