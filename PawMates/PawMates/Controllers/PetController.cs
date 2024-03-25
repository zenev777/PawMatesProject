using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Models.PetViewModels;
using PawMates.Core.Services.EventService;
using PawMates.Extensions;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Models;
using System.Globalization;
using System.Security.Claims;
using static PawMates.Infrastructure.Data.DataConstants;

namespace PawMates.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
		private readonly ApplicationDbContext data;
        private readonly IPetService petService;

        public PetController(ApplicationDbContext context, IPetService _petService)
        {
			data = context;
			petService = _petService;
        }


		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new PetFormViewModel();

			model.PetTypes = await GetPetTypes();

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Add(PetFormViewModel model)
        {
            var userId = User.Id();

			var result = await petService.CreatePetAsync(model, userId);

			if (result == false)
			{
				ModelState.AddModelError(nameof(model.DateOfBirth), $"Invalid date! Format must be: {DataConstants.DateOfBirthFormat}");
			}

            if (!ModelState.IsValid)
			{
				model.PetTypes = await GetPetTypes();

				return View(model);
			}

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await petService.GetMyPetsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Pets
                .Where(p => p.Id == id)
                .AsNoTracking()
                .Select(p => new PetInfoViewModel()
                {
                    Name = p.Name,
                    DateOfBirth = p.DateOfBirth.ToString(DateOfBirthFormat),
                    ImageUrl = p.ImageUrl,
                    PetType = p.PetType.Name,
                    Breed = p.Breed,
                    Gender = p.Gender,
					MainColor = p.MainColor,
					SecondaryColor=p.SecondaryColor,
					Weight = p.Weight,
                    OwnerId = p.OwnerId
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            var model = await data.Pets
                .Where(p => p.OwnerId == userId)
                .Where(p => p.Id == id)
                .AsNoTracking()
                .Select(s => new PetDeleteViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    DateOfBirth = s.DateOfBirth,
                })
                .FirstOrDefaultAsync();


            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(PetDeleteViewModel model)
        {
            var pet = await data.Pets
                .Where(p => p.Id == model.Id)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                return BadRequest();
            }

            if (pet.OwnerId != GetUserId())
            {
                return BadRequest();
            }

            data.Pets.Remove(pet);

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var pet = await data.Pets
				.FindAsync(id);

			if (pet == null)
			{
				return BadRequest();
			}

			if (pet.OwnerId != GetUserId())
			{
				return Unauthorized();
			}

			var model = new PetFormViewModel()
			{
				Name = pet.Name,
				Breed = pet.Breed,
				DateOfBirth=pet.DateOfBirth.ToString(DateOfBirthFormat),
				MainColor = pet.MainColor,
				SecondaryColor = pet.SecondaryColor,
				Gender = pet.Gender,
				PetTypeId = pet.PetTypeId,
				ImageUrl = pet.ImageUrl,
				Weight = pet.Weight,
			};

			model.PetTypes = await GetPetTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PetFormViewModel model, int id)
		{
			var pet = await data.Pets
				.FindAsync(id);

			if (pet == null)
			{
				return BadRequest();
			}

			if (pet.OwnerId != GetUserId())
			{
				return Unauthorized();
			}

			DateTime birth = DateTime.Now;

			if (!DateTime.TryParseExact(
				model.DateOfBirth,
				DateOfBirthFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out birth))
			{
				ModelState
					.AddModelError(nameof(model.DateOfBirth), $"Invalid date! Format must be: {DateOfBirthFormat}");
			}

			if (!ModelState.IsValid)
			{
				model.PetTypes = await GetPetTypes();

				return View(model);
			}

			pet.Name = model.Name;
			pet.Breed = model.Breed;
			pet.DateOfBirth = birth;
			pet.Weight = model.Weight;
			pet.PetTypeId = model.PetTypeId;
			pet.MainColor = model.MainColor;
			pet.SecondaryColor = model.SecondaryColor;
			pet.Gender = model.Gender;
			pet.ImageUrl = model.ImageUrl;

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}

		private async Task<IEnumerable<PetTypesViewModel>> GetPetTypes()
		{
			return await data.PetTypes
				.AsNoTracking()
				.Select(t => new PetTypesViewModel
				{
					Id = t.Id,
					Name = t.Name
				})
				.ToListAsync();
		}

	}
}
