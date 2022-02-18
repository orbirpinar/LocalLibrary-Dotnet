using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryContext _context;
        // GET
        public BookController(LibraryContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(book => book.Author).ToListAsync();
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