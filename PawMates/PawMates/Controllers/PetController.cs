using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Models;
using PawMates.Models;
using System.Globalization;
using System.Security.Claims;
using static PawMates.Infrastructure.Data.DataConstants;

namespace PawMates.Controllers
{
	[Authorize]
    public class PetController : Controller
    {
		private readonly ApplicationDbContext data;

        public PetController(ApplicationDbContext context)
        {
			data = context;
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
			DateTime birth = DateTime.Now;
			

			if (!DateTime.TryParseExact(model.DateOfBirth, DataConstants.DateOfBirthFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out birth))
			{
				ModelState.AddModelError(nameof(model.DateOfBirth), $"Invalid date! Format must be: {DataConstants.DateOfBirthFormat}");
			}

			

			if (!ModelState.IsValid)
			{
				model.PetTypes = await GetPetTypes();

				return View(model);
			}


			var entity = new Pet()
			{
				Name = model.Name,
				ImageUrl = model.ImageUrl,
				Breed = model.Breed,
				MainColor = model.MainColor,
				SecondaryColor = model.SecondaryColor,
				Weight = model.Weight,
				Gender = model.Gender,
				DateOfBirth = birth,
				PetTypeId = model.PetTypeId,
				OwnerId = GetUserId(),
			};

			//Add error massage for similar pet

			await data.Pets.AddAsync(entity);

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
        public async Task<IActionResult> All()
        {
            var pets = await data.Pets
                .AsNoTracking()
				.Where(p=>p.OwnerId == GetUserId())
                .Select(p => new PetInfoViewModel()
				{
					Name= p.Name,
					ImageUrl= p.ImageUrl,
					Id = p.Id,
				})
                .ToListAsync();

            return View(pets);
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
