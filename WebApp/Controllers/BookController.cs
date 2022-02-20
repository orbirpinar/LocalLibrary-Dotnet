using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Models.CreateModel;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILanguageRepository _languageRepository;


        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ILanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _languageRepository = languageRepository;
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Detail(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropDownAuthors();
            await PopulateDropDownLanguages();
            return View();
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookModel bookModel)
        {
            if (!ModelState.IsValid) return View();
            var language = await _languageRepository.GetByIdAsync(bookModel.LanguageId);
            var author = await _authorRepository.GetByIdAsync(bookModel.AuthorId);
            Book book = new()
            {
                Language = language,
                Title = bookModel.Title,
                Isbn = bookModel.Isbn,
                Summary = bookModel.Summary,
                Author = author
            };
            await _bookRepository.CreateAsync(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropDownAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
        }

        private async Task PopulateDropDownLanguages()
        {
            var languages = await _languageRepository.GetAllAsync();
            ViewBag.Languages = new SelectList(languages, "Id", "Name");
        }
    }
}