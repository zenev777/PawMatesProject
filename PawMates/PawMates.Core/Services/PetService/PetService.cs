using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PetViewModels;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Services.PetService
{
    public class PetService : IPetService
    {
        private readonly IRepository repository;

        public PetService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreatePetAsync(PetFormViewModel model, string userId)
        {
            if (await repository.AlreadyExistAsync<Pet>(e => e.Name == model.Name)) throw new ApplicationException("Event already exists");

            DateTime birth = DateTime.Now;

            if (!DateTime.TryParseExact(model.DateOfBirth, DataConstants.DateOfBirthFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out birth))
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

        public Task DeleteAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditPetAsync(int eventId, PetFormViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
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
                })
                .ToListAsync();
        }

        public Task<Event> PetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SameOrganiserAsync(int eventId, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
