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

        Task<int> EditPetAsync(int eventId, PetFormViewModel model);

        Task<Event> PetByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> SameOrganiserAsync(int eventId, string currentUserId);

        Task DeleteAsync(int eventId);
    }
}
