using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using static PawMates.Infrastructure.Data.DataConstants;

namespace PawMates.Core.Services.AttendanceService
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IRepository repository;

        public AttendanceService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> JoinEventAsync(int eventId, string userId)
        {

            if (await repository.AlreadyExistAsync<EventParticipant>(e => e.EventId == eventId && e.HelperId == userId))
            {
                return false;
            }

            var eventParticipant = new EventParticipant { EventId = eventId, HelperId = userId };

            await repository.AddAsync(eventParticipant);

            var eventToAddParticipant = await repository.GetByIdAsync<Event>(eventId);
            eventToAddParticipant.EventParticipants.Add(eventParticipant);

            await repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LeaveEventAsync(int eventId, string userId)
        {
            var attendance = await repository.FirstOrDefaultAsync<EventParticipant>(e => e.EventId == eventId && e.HelperId == userId);
            if (attendance == null)
            {
                return false;
            }


            repository.Delete(attendance);
            await repository.SaveChangesAsync();
            return true;
        }

        public async Task<List<EventInfoViewModel>> GetMyEventsAsync(string userId)
        {
            return await repository.AllReadOnly<EventParticipant>()
                .Where(a => a.HelperId == userId)
                .Include(a => a.Event)
                .Select(ep => new EventInfoViewModel
                {
                    Description = ep.Event.Description,
                    Id = ep.Event.Id,
                    Location = ep.Event.Location,
                    Name = ep.Event.Name,
                    StartsOn = ep.Event.StartsOn.ToString(EventStartDateFormat),
                })
                .ToListAsync();
        }
    }
}
