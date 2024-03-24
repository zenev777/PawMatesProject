using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawMates.Infrastructure.Data.DataConstants;

namespace PawMates.Core.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;

        public EventService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreateAsync(EventFormViewModel model, string userId)
        {
            if (await repository.AlreadyExistAsync<Event>(e=>e.Name == model.Name)) throw new ApplicationException("Event already exists");

            DateTime start = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.StartsOn,
                EventStartDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                return false;
            } 

            var entity = new Event()
            {
                StartsOn = start,
                Description = model.Description,
                Location = model.Location,
                Name = model.Name,
                OrganiserId = userId,
            };

            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();

            return true;

        }

        public Task<bool> CreateEventAsync(EventFormViewModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventInfoViewModel>> GetAllEventsAsync()
        {
            return  await repository
               .AllReadOnly<Event>()
               .Select(e => new EventInfoViewModel()
               {
                   Name = e.Name,
                   StartsOn = e.StartsOn.ToString(EventStartDateFormat, CultureInfo.InvariantCulture),
                   Location = e.Location,
                   Description = e.Description,
                   OrganiserId = e.Organiser.UserName,
                   Id = e.Id,
               })
               .ToListAsync();
        }


    }
}
