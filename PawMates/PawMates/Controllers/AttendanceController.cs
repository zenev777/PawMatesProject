using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Extensions;

namespace PawMates.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService _attendanceService)
        {
            attendanceService = _attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var userId = User.Id();
            var success = await attendanceService.JoinEventAsync(id, userId);
            if (success)
            {
                return RedirectToAction(nameof(Joined));
            }
            else
            {
                return BadRequest("Already joined event");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userId = User.Id();
            var success = await attendanceService.LeaveEventAsync(id, userId);
            if (success)
            {
                return RedirectToAction(nameof(Joined));
            }
            else
            {
                return BadRequest("Not joined event");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userId = User.Id();
            var events = await attendanceService.GetMyEventsAsync(userId);
            return View(events);
        }
    }
}
