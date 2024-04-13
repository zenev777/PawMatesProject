using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PetViewModels;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Contracts.PetInterface
{
    public interface IPetService
    {
        Task<bool> CreatePetAsync(PetFormViewModel model, string userId);

        Task<IEnumerable<PetInfoViewModel>> GetMyPetsAsync();

        Task<int> EditPetAsync(int petId, PetFormViewModel model);

        Task<Pet> PetByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> SameOwnerAsync(int petId, string currentUserId);

        Task DeleteAsync(int petId);

        Task<PetInfoViewModel> GetPetDetailsAsync(int petId);

        Task<IEnumerable<PetTypesViewModel>> GetPetTypes();

        Task<AllPetsQueryModel> AllAsync(
            string? petType = null,
            int currentPage = 1,
            int petsPerPage = 1);

        Task<IEnumerable<string>> AllPetTypeNamesAsync();
    }
}
