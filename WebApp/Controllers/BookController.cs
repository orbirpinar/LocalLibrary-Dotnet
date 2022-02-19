using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.CreateModel;

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

        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            PopulateDropDownAuthors();
            PopulateDropDownLanguages();
            return View();
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookModel bookModel)
        {
            if (!ModelState.IsValid) return View();
            var language = await _context.Languages.Where(l => l.Id == bookModel.LanguageId).FirstAsync();
            var author = await _context.Authors.Where(a => a.Id == bookModel.AuthorId).FirstAsync();
            Book book = new()
            {
                Language = language,
                Title = bookModel.Title,
                Isbn = bookModel.Isbn,
                Summary = bookModel.Summary,
                Author = author
            };
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDownAuthors()
        {
            var authors = _context.Authors.ToList();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
        }

        private void PopulateDropDownLanguages()
        {
            var languages = _context.Languages.ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "Name");
        }
    }
}