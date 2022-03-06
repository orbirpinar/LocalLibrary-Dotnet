using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.ViewModel;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid) return View();
            var user = new User
            {
                UserName = register.Email,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName
            };
            var result = await _userRepository.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login loginModel, string? returnUrl)
        {
            if (!ModelState.IsValid) return View();
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username or Password incorrect");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}