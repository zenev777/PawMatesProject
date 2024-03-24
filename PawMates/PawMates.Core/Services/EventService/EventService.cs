using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<EventInfoViewModel>> GetAllEventsAsync()
        {
            return  await repository
               .AllReadOnly<Event>()
               .Select(e => new EventInfoViewModel()
               {
                   Name = e.Name,
                   StartsOn = e.StartsOn.ToString(EventStartDateFormat),
                   Location = e.Location,
                   Description = e.Description,
                   OrganiserId = e.Organiser.UserName,
                   Id = e.Id,
               })
               .ToListAsync();
        }
    }
}
