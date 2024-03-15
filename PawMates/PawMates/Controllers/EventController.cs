using Microsoft.AspNetCore.Mvc;
using PawMates.Data;
using PawMates.Models;

namespace PawMates.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext data;

        public EventController(ApplicationDbContext context)
        {
            data = context;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();

            return View(model);
        }
    }
}
