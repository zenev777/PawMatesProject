using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Data;
using PawMates.Data.Models;
using PawMates.Models;
using System.Globalization;
using System.Security.Claims;

namespace PawMates.Controllers
{
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
				Age = model.Age,
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

			return RedirectToAction("Home","Index");
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
