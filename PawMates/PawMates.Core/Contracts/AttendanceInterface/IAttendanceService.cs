using PawMates.Core.Models.EventViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Contracts.AttendanceInterface
{
    public interface IAttendanceService
    {
        Task<bool> JoinEventAsync(int eventId, string userId);
        
        Task<bool> LeaveEventAsync(int eventId, string userId);
        
        Task<List<EventInfoViewModel>> GetMyEventsAsync(string userId);
    }
}
