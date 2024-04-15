using Microsoft.EntityFrameworkCore;
using Moq;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Services.AttendanceService;
using PawMates.Core.Services.EventService;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //[Test]
        //public async Task JoinEventAsync_UserNotJoined_ReturnsTrue()
        //{
        //    var repo = new Repository(context);
            
        //    attendanceService = new AttendanceService(repo);
            
            
        //    var eventId = 1;
        //    var userId = "user123";

        //    await repo.AddAsync(new Event()
        //    {
        //        Id = 1,
        //        Description = "Test",
        //        Location = "Test Loc",
        //        Name = "Test",
        //        StartsOn = DateTime.Now,
        //        OrganiserId = userId
        //    });

        //    await repo.AddAsync(new EventParticipant()
        //    {
        //        HelperId = userId,
        //        EventId = eventId,
        //    });

        //    await repo.SaveChangesAsync();

        //    // Act
        //    var result = await attendanceService.JoinEventAsync(eventId, userId);

        //    // Assert
        //    Assert.IsTrue(result);

            
        //}

        //[Test]
        //public async Task JoinEventAsync_UserAlreadyJoined_ReturnsFalse()
        //{
        //    // Arrange
        //    var eventId = 1;
        //    var userId = "user123";

        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(repo => repo.AlreadyExistAsync<EventParticipant>(e => e.EventId == eventId && e.HelperId == userId))
        //                  .ReturnsAsync(true);

        //    // Act
        //    var result = await attendanceService.JoinEventAsync(eventId, userId);

        //    // Assert
        //    Assert.IsFalse(result);

        //    mockRepository.Verify(repo => repo.AddAsync(It.IsAny<EventParticipant>()), Times.Never);
        //    mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        //}
    }
}
