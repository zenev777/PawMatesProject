using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Models.PetViewModels;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.DataConstants;
using PawMates.Infrastructure.Data.Models;
using System.Globalization;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Core.Services.PetService
{
    public class PetService : IPetService
    {
        private readonly IRepository repository;

        public PetService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<AllPetsQueryModel> AllAsync(
            string? petType = null, 
            int currentPage = 1, 
            int petsPerPage = 1)
        {
            var petsToShow = repository.AllReadOnly<Pet>();

            if (petType != null)
            {
                petsToShow = petsToShow
                    .Where(p => p.PetType.Name == petType);
            }        

            var pets = await petsToShow
                .Skip((currentPage - 1) * petsPerPage)
                .Take(petsPerPage)
                .ToListAsync();

            int totalPets = await petsToShow.CountAsync();

            return new AllPetsQueryModel()
            {
                Pets = pets,
                TotalPetsCount = totalPets
            };
        }

        public async Task<IEnumerable<string>> AllPetTypeNamesAsync()
        {
            return await repository.AllReadOnly<PetType>()
               .Select(c => c.Name)
               .Distinct()
               .ToListAsync();
        }

        public async Task<bool> CreatePetAsync(PetFormViewModel model, string userId)
        {
            if (await repository.AlreadyExistAsync<Pet>(p => p.Name == model.Name && p.OwnerId == userId))
            {
                return false;
            }

            DateTime birth = DateTime.Now;

            if (!DateTime.TryParseExact(model.DateOfBirth, DataConstants.DateOfBirthFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out birth))
            {
                return false;
            }

            //return pop up massage in future
            if (birth>DateTime.Now)
            {
                return false;
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
                OwnerId = userId,
            };

            await repository.AddAsync(entity);

            await repository.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int petId)
        {
            var petToDelete = await repository.GetByIdAsync<Pet>(petId);
            repository.Delete(petToDelete);

            await repository.SaveChangesAsync();
        }

        public async Task<int> EditPetAsync(int petId, PetFormViewModel model)
        {
            var petToEdit = await repository.GetByIdAsync<Pet>(petId);

            DateTime birth = DateTime.Now;

            if (!DateTime.TryParseExact(model.DateOfBirth,
            DateOfBirthFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out birth))
            {
                return -1;
            }

            //return pop up massage in future
            if (birth > DateTime.Now)
            {
                return -1;
            }

            petToEdit.Name = model.Name;
            petToEdit.Breed = model.Breed;
            petToEdit.DateOfBirth = birth;
            petToEdit.Weight = model.Weight;
            petToEdit.PetTypeId = model.PetTypeId;
            petToEdit.MainColor = model.MainColor;
            petToEdit.SecondaryColor = model.SecondaryColor;
            petToEdit.Gender = model.Gender;
            petToEdit.ImageUrl = model.ImageUrl;

            await repository.SaveChangesAsync();

            return petToEdit.Id;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await repository.AllReadOnly<Pet>().AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PetInfoViewModel>> GetMyPetsAsync()
        {
            return await repository
                .AllReadOnly<Pet>()
                .Select(p => new PetInfoViewModel()
                {
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Id = p.Id,
                    OwnerId = p.Owner.UserName,
                })
                .ToListAsync();
        }

        public async Task<PetInfoViewModel> GetPetDetailsAsync(int petId)
        {
            return await repository
                .AllReadOnly<Pet>()
                .Where(p=>p.Id == petId)
                .Select(p => new PetInfoViewModel()
                {
                    Name = p.Name,
                    DateOfBirth = p.DateOfBirth.ToString(DateOfBirthFormat),
                    ImageUrl = p.ImageUrl,
                    PetType = p.PetType.Name,
                    Breed = p.Breed,
                    Gender = p.Gender,
                    MainColor = p.MainColor,
                    SecondaryColor = p.SecondaryColor,
                    Weight = p.Weight,
                    OwnerId = p.OwnerId
                })
                .FirstAsync();
        }

        public async Task<Pet> PetByIdAsync(int id)
        {
            return await repository.AllReadOnly<Pet>()
                .Where(e => e.Id == id).FirstAsync();
        }

        public async Task<bool> SameOwnerAsync(int petId, string currentUserId)
        {
            bool result = false;
            var pet = await repository.AllReadOnly<Pet>()
                .Where(e => e.Id == petId)
                .FirstOrDefaultAsync();

            if (pet?.OwnerId == currentUserId)
            {
                result = true;
            }

            return result;
        }

        async Task<IEnumerable<PetTypesViewModel>> IPetService.GetPetTypes()
        {
            return await repository
                .AllReadOnly<PetType>()
                .OrderBy(c => c.Name)
                .Select(c => new PetTypesViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
