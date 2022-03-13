using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dto;
using WebApp.Producer;

namespace WebApp.Controllers
{
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
                return RedirectToAction("Index","Home");
            }
            
            ModelState.AddModelError("","Unexpected result!");
            return View();
        }
    }
}