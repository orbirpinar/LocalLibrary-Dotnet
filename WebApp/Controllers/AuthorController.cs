using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Dto;
using WebApp.Models;
using WebApp.Models.CreateModel;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;

        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;
        
        public AuthorController(ILogger<AuthorController> logger, IMapper mapper, IAuthorRepository authorRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async  Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllAsync();
            return View(authors);
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var author = await _authorRepository.GetWithBooksAndInstancesByIdAsync(id);
            if (author == null)
            {
                _logger.Log(LogLevel.Information,"Author not found");
                return NotFound();
            }
            var mappedAuthor = _mapper.Map<AuthorDetailDto>(author);
            _logger.Log(LogLevel.Information,"Accessed to Author Detail Page");
            return View(mappedAuthor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorModel authorModel)
        {
            if (!ModelState.IsValid) return View();
            Author author = new()
            {
                Name = authorModel.Name,
                DateOfBirth = authorModel.DateOfBirth,
                DateOfDeath = authorModel.DateOfDeath
            };
            await _authorRepository.CreateAsync(author);
            await _authorRepository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}