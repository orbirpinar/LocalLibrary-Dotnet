using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserBookInstanceController: Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserBookInstanceController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetAuthenticatedUser();
            var books = await _context.BookInstance
                .Include(b => b.Book)
                .Where(b => b.Borrower != null && b.Borrower.Id == user.Id)
                .ToListAsync();
            return View(books);
        }

        [Authorize]
        public async Task<IActionResult> Loan()
        {
            var books = await _context.Book.Where(b => b.Instances != null)
                .ToListAsync();
            ViewBag.Books = new SelectList(books, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost("/userBookInstance/loan/{bookInstanceId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Loan(Guid bookInstanceId)
        {
            if (!ModelState.IsValid) return View();
            var user = await GetAuthenticatedUser();
            var bookInstance = await _context.BookInstance
                .Where(b => b.Id.Equals(bookInstanceId))
                .FirstAsync();
            bookInstance.LoanStatus = LoanStatus.OnLoan;
            bookInstance.BorrowerId = user.Id;
            bookInstance.DueBack = DateTime.Now.AddDays(15);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<User> GetAuthenticatedUser()
        {
            return _userManager.GetUserAsync(User);
        }


    }
}