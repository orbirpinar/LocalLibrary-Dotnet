using System.Threading.Tasks;
using Library.Models;
using LocalLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryContext _context;
        // GET
        public BookController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.
                Include(book => book.Author).ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var book = await _context.Books
                .Include(b => b.Instances)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        
        public  IActionResult Create()
        {
            return View();
        }
    }
}