using PawMates.Core.Models.EventViewModels;

namespace PawMates.Core.Contracts.AttendanceInterface
{
    public interface IAttendanceService
    {
        Task<bool> JoinEventAsync(int eventId, string userId);
        
        Task<bool> LeaveEventAsync(int eventId, string userId);
        
        Task<List<EventInfoViewModel>> GetMyEventsAsync(string userId);
    }
}
