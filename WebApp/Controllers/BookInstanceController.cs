using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.CreateModel;

namespace WebApp.Controllers
{
    public class BookInstanceController : Controller
    {

        private readonly LibraryContext _context;

        public BookInstanceController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var bookInstance = await _context.BookInstances
                .Include(b => b.Book)
                .Include(b => b.Borrower).FirstAsync();
            return View(bookInstance);
        }

        [HttpGet("/bookInstance/{bookId:int}")]
        public async Task<IActionResult> Index(int bookId)
        {
            var instances = await _context.BookInstances
                .Where(instance => instance.Book.Id == bookId)
                .ToListAsync();
            return View(instances);
        }
        
        [HttpPost("/bookInstance/{bookId:int}")]
        public async Task<IActionResult> Create(int bookId,CreateBookInstanceModel bookInstanceModel)
        {
            var book = await _context.Books.Where(book => book.Id == bookId).FirstAsync();
            await _context.BookInstances.AddAsync(new BookInstance
            {
                Book = book,
                Id = Guid.NewGuid(),
                Imprint = bookInstanceModel.Imprint,
                LoanStatus = LoanStatus.Available
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail","Book",new{id = bookId});
        }
        
    }
}