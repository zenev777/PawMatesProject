using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Models.EventViewModels;
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
        private readonly IPetService petService;

        public PetController(IPetService _petService)
        {
			petService = _petService;
        }


		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new PetFormViewModel();

			model.PetTypes = await petService.GetPetTypes();

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
				model.PetTypes = await petService.GetPetTypes();

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
            var model = await petService.GetPetDetailsAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await petService.ExistsAsync(id) == false))
            {
                return RedirectToAction(nameof(All));
            }

            if (await petService.SameOwnerAsync(id, User.Id()) == false)
            {
                return RedirectToAction(nameof(All));
            };

            var petToDelete = await petService.PetByIdAsync(id);

            var model = new PetDeleteViewModel()
            {
                Id = petToDelete.Id,
                Name = petToDelete.Name,
                DateOfBirth = petToDelete.DateOfBirth.ToString(DateOfBirthFormat, CultureInfo.InvariantCulture),
            };

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(PetDeleteViewModel model)
        {
            if ((await petService.ExistsAsync(model.Id) == false))
            {
                return RedirectToAction(nameof(All));
            }

            if (await petService.SameOwnerAsync(model.Id, User.Id()) == false)
            {
                return RedirectToAction(nameof(All));
            };

            await petService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
            if ((await petService.ExistsAsync(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = User.Id();
            if (await petService.SameOwnerAsync(id, userId) == false)
            {
                return Unauthorized();
            };

            var pet = await petService.PetByIdAsync(id);

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

			model.PetTypes =await petService.GetPetTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PetFormViewModel model, int id)
		{
            if (id != model.Id)
            {
                return RedirectToAction(nameof(All));
            }

            if (await petService.ExistsAsync(model.Id) == false)
            {
                ModelState.AddModelError("", "Pet does not exist");

                return View(model);
            }

            if (await petService.SameOwnerAsync(model.Id, User.Id()) == false)
            {
                return RedirectToAction(nameof(All));
            };

            if (await petService.EditPetAsync(model.Id, model) == -1)
            {
                ModelState.AddModelError(nameof(model.DateOfBirth), $"Invalid Date! Format must be:{DateOfBirthFormat}");

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }


	}
}
