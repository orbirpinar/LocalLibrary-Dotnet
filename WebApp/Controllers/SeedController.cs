using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dto;
using WebApp.Producer;

namespace WebApp.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class SeedController : Controller
    {

        private readonly IProducerService _producerService;

        public SeedController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public  IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(SearchParamDto searchParamDto)
        {
            var result = _producerService.Send(searchParamDto);
            if (result)
            {
                return RedirectToAction(nameof(Search));
            }
            
            ModelState.AddModelError("","Unexpected result!");
            return View();
        }
    }
}