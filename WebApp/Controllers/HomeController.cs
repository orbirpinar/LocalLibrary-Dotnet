using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IBookInstanceRepository _bookInstanceRepository;
        private readonly IAuthorRepository _authorRepository;

        public HomeController(ILogger<HomeController> logger, IAuthorRepository authorRepository, IBookInstanceRepository bookInstanceRepository, IBookRepository bookRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
            _bookInstanceRepository = bookInstanceRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["numberOfBooks"] = await _bookRepository.GetCountAsync();
            ViewData["numberOfAuthors"] = await _authorRepository.GetCountAsync();
            ViewData["numberOfBookInstances"] = await _bookInstanceRepository.GetCountAsync();
            ViewData["numberOfAvailableInstances"] = await _bookInstanceRepository.GetCountAvailableAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}