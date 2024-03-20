using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Data;
using PawMates.Data.Models;
using PawMates.Models;
using System.Globalization;
using System.Security.Claims;
using static PawMates.Data.DataConstants;

namespace PawMates.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext data;

        public EventController(ApplicationDbContext context)
        {
            data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await data.Events
                .AsNoTracking()
                .Select(e => new EventInfoViewModel()
                {
                    Name = e.Name,
                    StartsOn = e.StartsOn.ToString(EventStartDateFormat),
                    Location = e.Location,
                    Description = e.Description,
                    OrganiserId = e.Organiser.UserName,
                    Id = e.Id,
                })
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
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
                StartsOn = start,
                Description = model.Description,
                Location = model.Location,
                Name = model.Name,
                OrganiserId = GetUserId(),
            };

            await data.Events.AddAsync(entity);

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await data.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!e.EventParticipants.Any(p => p.HelperId == userId))
            {
                e.EventParticipants.Add(new EventParticipant()
                {
                    EventId = e.Id,
                    HelperId = userId
                });

                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await data.EventParticipants
                .Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(ep => new EventInfoViewModel()
                {
                    Id = ep.EventId,
                    Name = ep.Event.Name,
                    StartsOn = ep.Event.StartsOn.ToString(EventStartDateFormat),
                    Location = ep.Event.Location,
                    OrganiserId =  ep.Event.Organiser.UserName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await data.Events
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new EventFormViewModel()
            {
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                StartsOn = e.StartsOn.ToString(EventStartDateFormat),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            var e = await data.Events
                .FindAsync(id);
            
            DateTime start = DateTime.Now;

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

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

            e.StartsOn = start;
            e.Name = model.Name;
            e.Description = model.Description;
            e.Location = model.Location;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            var model = await data.Events
                .Where(e => e.OrganiserId == userId)
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDeleteViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartsOn = e.StartsOn,
                })
                .FirstOrDefaultAsync();


            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
