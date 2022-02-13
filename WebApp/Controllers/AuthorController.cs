using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Data;
using WebApp.Dto;

namespace WebApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;

        private readonly LibraryContext _context;

        private readonly IMapper _mapper;
        
        public AuthorController(ILogger<AuthorController> logger, LibraryContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async  Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var author = await _context.Authors
                .Include(author => author.Books)
                .ThenInclude(book => book.Instances)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
            {
                _logger.Log(LogLevel.Information,"Author not found");
                return NotFound();
            }
            var mappedAuthor = _mapper.Map<AuthorDetailDto>(author);
            _logger.Log(LogLevel.Information,"Accessed to Author Detail Page");
            return View(mappedAuthor);
        }
    }
}