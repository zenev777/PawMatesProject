using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Core.Services.AttendanceService;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.IdentityModels;
using PawMates.Infrastructure.Data.Models;

namespace PawMates.UnitTests
{
    [TestFixture]
    public class AttedanceServiceTests
    {
        private IRepository repository;
        private IAttendanceService attendanceService;
        private ApplicationDbContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDataBase")
                .Options;

            context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task JoinEventAsync_UserNotJoined_ReturnsTrue()
        {
            var repo = new Repository(context);
          
            attendanceService = new AttendanceService(repo);
          
            var eventId = 1;

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Event()
            {
                Id = 1,
                Description = "Test",
                Location = "Test Loc",
                Name = "Test",
                StartsOn = DateTime.Now,
                Organiser = user
            });

            var eventToAttend = repo.GetByIdAsync<Event>(1);

            await repo.SaveChangesAsync();

            var result = await attendanceService.JoinEventAsync(eventId, user.Id);

            Assert.IsTrue(result);   
        }

        [Test]
        public async Task JoinEventAsync_UserAlreadyJoined_ReturnsFalse()
        {
            var repo = new Repository(context);

            attendanceService = new AttendanceService(repo);

            var eventId = 1;

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Event()
            {
                Id = 1,
                Description = "Test",
                Location = "Test Loc",
                Name = "Test",
                StartsOn = DateTime.Now,
                Organiser = user
            });

            var eventToAttend = repo.GetByIdAsync<Event>(1);

            await repo.AddAsync(new EventParticipant()
            {
                Helper = user,
                EventId = 1
            });

            await repo.SaveChangesAsync();

            var result = await attendanceService.JoinEventAsync(eventId, user.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task LeaveEventAsync_UserLeavesEvent_ReturnsTrue()
        {
            var repo = new Repository(context);

            attendanceService = new AttendanceService(repo);

            var eventId = 1;

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Event()
            {
                Id = 1,
                Description = "Test",
                Location = "Test Loc",
                Name = "Test",
                StartsOn = DateTime.Now,
                Organiser = user
            });

            var eventToAttend = repo.GetByIdAsync<Event>(1);

            await repo.AddAsync(new EventParticipant()
            {
                Helper = user,
                EventId = 1
            });

            await repo.SaveChangesAsync();

            var result = await attendanceService.LeaveEventAsync(eventId, user.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task LeaveEventAsync_UserNotJoinedEvent_ReturnsFalse()
        {
            var repo = new Repository(context);

            attendanceService = new AttendanceService(repo);

            var eventId = 1;

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Event()
            {
                Id = 1,
                Description = "Test",
                Location = "Test Loc",
                Name = "Test",
                StartsOn = DateTime.Now,
                Organiser = user
            });

            var eventToAttend = repo.GetByIdAsync<Event>(1);

            await repo.SaveChangesAsync();

            var result = await attendanceService.LeaveEventAsync(eventId, user.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetMyEventsAsync_ReturnsEventsForUser()
        {
            var repo = new Repository(context);

            attendanceService = new AttendanceService(repo);

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Event()
            {
                Id = 1,
                Description = "Test",
                Location = "Test Loc",
                Name = "Test",
                StartsOn = DateTime.Now,
                Organiser = user
            });
            await repo.AddAsync(new Event()
            {
                Id = 2,
                Description = "Test 2",
                Location = "Test Loc 222",
                Name = "Test 222",
                StartsOn = DateTime.Now,
                Organiser = user
            });

            await repo.AddAsync(new EventParticipant()
            {
                Helper = user,
                EventId = 1
            });
            await repo.AddAsync(new EventParticipant()
            {
                Helper = user,
                EventId = 2
            });

            await repo.SaveChangesAsync();
            
            var result = await attendanceService.GetMyEventsAsync(user.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));

            var eventInfoViewModels = result.ToList();
            Assert.That(eventInfoViewModels[0].Name, Is.EqualTo("Test"));
            Assert.That(eventInfoViewModels[0].Location, Is.EqualTo("Test Loc"));

            Assert.That(eventInfoViewModels[1].Name, Is.EqualTo("Test 222"));
            Assert.That(eventInfoViewModels[1].Location, Is.EqualTo("Test Loc 222"));
        }
    }
}
