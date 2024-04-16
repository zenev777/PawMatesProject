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
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Core.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;

        public EventService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreateEventAsync(EventFormViewModel model, string userId)
        {
            if (await repository.AlreadyExistAsync<Event>(e => e.Name == model.Name)) throw new ApplicationException("Event already exists");

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

            if (start < DateTime.Now)
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

        public async Task<int> EditEventAsync(int eventId, EventFormViewModel model)
        {
            var eventToEdit = await repository.GetByIdAsync<Event>(eventId);

            if (eventToEdit == null)
            {
                return -1;
            }

            DateTime start = DateTime.Now;

            if (!DateTime.TryParseExact(model.StartsOn,
            EventStartDateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out start))
            {
                return -1;
            }

            if (start < DateTime.Now)
            {
                return -1;
            }

            eventToEdit.Name = model.Name;
            eventToEdit.Location = model.Location;
            eventToEdit.StartsOn = start;
            eventToEdit.Description = model.Description;
            eventToEdit.Id = model.Id;

            await repository.SaveChangesAsync();

            return eventToEdit.Id;
        }

        public async Task<IEnumerable<EventInfoViewModel>> GetAllEventsAsync()
        {
            return await repository
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

        public async Task<Event> EventByIdAsync(int id)
        {
            return await repository.AllReadOnly<Event>()
                .Where(e => e.Id == id).FirstAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await repository.AllReadOnly<Event>().AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SameOrganiserAsync(int eventId, string currentUserId)
        {
            bool result = false;
            var Event = await repository.AllReadOnly<Event>()
                .Where(e => e.Id == eventId)
                .FirstOrDefaultAsync();

            if (Event?.OrganiserId == currentUserId)
            {
                result = true;
            }

            return result;
        }

        public async Task DeleteAsync(int eventId)
        {
            var eventToDelete = await repository.GetByIdAsync<Event>(eventId);
            var joinedUsers = repository.All<EventParticipant>(ep => ep.EventId == eventId);
            repository.DeleteRange(joinedUsers);
            repository.Delete(eventToDelete);

            await repository.SaveChangesAsync();
        }
    }
}
