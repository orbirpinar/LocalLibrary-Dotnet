using System.Threading.Tasks;
using LocalLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;

        private readonly LibraryContext _context;
        
        public AuthorController(ILogger<AuthorController> logger, LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            var authors=  await _context.Authors.ToListAsync();
            return View(authors);
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
            {
                _logger.Log(LogLevel.Information,"Author not found");
                return NotFound();
            }
            _logger.Log(LogLevel.Information,"Accessed to Author Detail Page");
            return View(author);
        }
    }
}