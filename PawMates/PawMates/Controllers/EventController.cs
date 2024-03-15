using Microsoft.AspNetCore.Mvc;
using PawMates.Data;
using PawMates.Data.Models;
using PawMates.Models;
using System.Globalization;
using System.Security.Claims;
using static PawMates.Data.DataConstants;

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

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.StartsOn,
                EventStartDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState
                    .AddModelError(nameof(model.StartsOn), $"Invalid date! Format must be: {EventStartDateFormat}");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Event()
            {
                StartsOn = DateTime.Now,
                Description = model.Description,
                Name = model.Name,
                OrganiserId = GetUserId(),
            };

            await data.Events.AddAsync(entity);

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
