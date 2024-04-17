using PawMates.Core.Models.EventViewModels;
using PawMates.Infrastructure.Data.Models;

namespace PawMates.Core.Contracts.EventInterface
{
    public interface IEventService
    {
        Task<bool> CreateEventAsync(EventFormViewModel model, string userId);

        Task<IEnumerable<EventInfoViewModel>> GetAllEventsAsync();

        Task<int> EditEventAsync(int eventId, EventFormViewModel model);

        Task<Event> EventByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> SameOrganiserAsync(int eventId, string currentUserId);

        Task DeleteAsync(int eventId);
    }
}
