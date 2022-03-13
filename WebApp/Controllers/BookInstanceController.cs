using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.CreateModel;
using WebApp.Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class BookInstanceController : Controller
    {

        private readonly IBookInstanceRepository _bookInstanceRepository;
        private readonly IBookRepository _bookRepository;


        public BookInstanceController(IBookInstanceRepository bookInstanceRepository, IBookRepository bookRepository)
        {
            _bookInstanceRepository = bookInstanceRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var bookInstance = await _bookInstanceRepository.GetWithBookAndBorrowerById(id);
            if (bookInstance is null)
            {
                return NotFound();
            }
            return View(bookInstance);
        }


        
        [HttpPost("/bookInstance/{bookId:int}")]
        public async Task<IActionResult> Create(int bookId,CreateBookInstanceModel bookInstanceModel)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            await _bookInstanceRepository.CreateAsync(new BookInstance
            {
                Book = book,
                Id = Guid.NewGuid(),
                Imprint = bookInstanceModel.Imprint,
                LoanStatus = LoanStatus.Available
            });
            await _bookInstanceRepository.SaveAsync();
            return RedirectToAction("Detail","Book",new{id = bookId});
        }
        
    }
}