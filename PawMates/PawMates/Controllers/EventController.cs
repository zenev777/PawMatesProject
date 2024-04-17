using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PetViewModels;
using PawMates.Core.Services.EventService;
using PawMates.Extensions;
using PawMates.Infrastructure.Data.Models;
using System.Globalization;
using System.Security.Claims;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventsAsync();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new EventFormViewModel();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {

            var userId = User.Id();

            var result = await eventService.CreateEventAsync(model, userId);

            if (result == false)
            {
                //ModelState.AddModelError(nameof(model.StartsOn), $"Invalid date! Format must be: {EventStartDateFormat}");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if ((await eventService.ExistsAsync(Id)) == false)
            {
                return NotFound();
            }

            var userId = User.Id();
            if (await eventService.SameOrganiserAsync(Id, userId) == false)
            {
                return Forbid();
            };

            var eventModel = await eventService.EventByIdAsync(Id);

            if (eventModel==null)
            {
                return NotFound();
            }

            var model = new EventFormViewModel()
            {
                Id = eventModel.Id,
                Description = eventModel.Description,
                Location = eventModel.Location,
                Name = eventModel.Name,
                StartsOn = eventModel.StartsOn.ToString(EventStartDateFormat, CultureInfo.InvariantCulture),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (await eventService.ExistsAsync(model.Id) == false)
            {
                return NotFound();
            }

            if (await eventService.SameOrganiserAsync(model.Id, User.Id()) == false)
            {
                return Forbid();
            };

            if (await eventService.EditEventAsync(model.Id, model) == -1)
            {
                //ModelState.AddModelError(nameof(model.StartsOn), $"Invalid Date! Format must be:{EventStartDateFormat}");
                return NotFound();
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await eventService.ExistsAsync(id) == false))
            {
                return NotFound();
            }

            if (await eventService.SameOrganiserAsync(id, User.Id()) == false)
            {
                return Forbid();
            };

            var eventToDelete = await eventService.EventByIdAsync(id);
            
            if (eventToDelete == null)
            {
                return NotFound();
            }

            var model = new EventDeleteViewModel()
            {
                Id = eventToDelete.Id,
                Name = eventToDelete.Name,
                StartsOn = eventToDelete.StartsOn.ToString(EventStartDateFormat, CultureInfo.InvariantCulture),
            };

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(PetDeleteViewModel model)
        {
            if ((await eventService.ExistsAsync(model.Id) == false))
            {
                return NotFound();
            }

            if (await eventService.SameOrganiserAsync(model.Id, User.Id()) == false)
            {
                return Forbid();
            };

            if (model == null)
            {
                return NotFound();
            }

            await eventService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
