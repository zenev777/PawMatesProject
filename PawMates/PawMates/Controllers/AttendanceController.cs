using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Extensions;

namespace PawMates.Controllers
{
    [Authorize]
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
                return StatusCode(404);
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
                return StatusCode(404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userId = User.Id();
            var events = await attendanceService.GetMyEventsAsync(userId);

            if (events == null)
            {
                StatusCode(404);
            }

            return View(events);
        }
    }
}
