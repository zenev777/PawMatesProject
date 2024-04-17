using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Services.EventService;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.IdentityModels;
using PawMates.Infrastructure.Data.Models;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.UnitTests
{
    [TestFixture]
    public class EventServiceTests
    {
        private IEventService eventService;
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
        public async Task CreateEventAsync()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            var creatorId = "creator123";

            var result = await eventService.CreateEventAsync((new EventFormViewModel()
            {
                Name = "Test",
                Description = "Test",
                Location = "Test Location",
                StartsOn = "01/01/2025 15:00",
                Id = 1
            }), creatorId);

            var dbEvent = await repo.GetByIdAsync<Event>(1);

            Assert.IsTrue(result);
            Assert.That(dbEvent.Location, Is.EqualTo("Test Location"));
            Assert.That(dbEvent.Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task EventByIdAsync_ExistingId_ReturnsEvent()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            var eventId = 1;

            await repo.AddAsync(new Event()
            {
                Name = "Test",
                Description = "Test",
                Location = "Test Location",
                StartsOn = DateTime.Now,
                Id = 1
            });

            await repo.SaveChangesAsync();

            var result = await eventService.EventByIdAsync(eventId);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(eventId));
            Assert.That(result.Location, Is.EqualTo("Test Location"));
        }

        [Test]
        public async Task EventByIdAsync_NonExistingId()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            await repo.AddAsync(new Event()
            {
                Name = "Test",
                Description = "Test",
                Location = "Test Location",
                StartsOn = DateTime.Now,
                Id = 1
            });

            await repo.SaveChangesAsync();

            var events = new List<Event>().AsQueryable();

            var result = await eventService.EventByIdAsync(1);

            Assert.IsTrue(!events.Contains(result));
        }

        [Test]
        public async Task TestDeleteEventAsync()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            await repo.AddAsync(new Event()
            {
                Name = "Test",
                Description = "Test",
                Location = "Test Location",
                StartsOn = DateTime.Now,
                Id = 1
            });
            await repo.AddAsync(new Event()
            {
                Name = "Test Second",
                Description = "Test Second",
                Location = "Test Location Second",
                StartsOn = DateTime.Now,
                Id = 2
            });

            await repo.SaveChangesAsync();

            var events = repo.All<Event>();

            await eventService.DeleteAsync(1);

            Assert.That(events.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task SameOrganiserAsync_EventWithSameOrganiser_ReturnsTrue()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            var currentOrganiserId = "user123";

            await repo.AddAsync(new Event()
            {
                Name = "Test Second",
                Description = "Test Second",
                Location = "Test Location Second",
                StartsOn = DateTime.Now,
                OrganiserId = "user123",
                Id = 2
            });

            var eventId = 2;

            await repo.SaveChangesAsync();

            var result = await eventService.SameOrganiserAsync(eventId, currentOrganiserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SameOrganiserAsync_EventWithDiffOrganiser_ReturnsFalse()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            var currentOrganiserId = "user123";

            await repo.AddAsync(new Event()
            {
                Name = "Test Second",
                Description = "Test Second",
                Location = "Test Location Second",
                StartsOn = DateTime.Now,
                OrganiserId = "diffuser123",
                Id = 2
            });

            var eventId = 1;

            await repo.SaveChangesAsync();

            var result = await eventService.SameOrganiserAsync(eventId, currentOrganiserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ExistsAsync_ReturnsTrue()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            await repo.AddAsync(new Event()
            {
                Name = "Test Second",
                Description = "Test Second",
                Location = "Test Location Second",
                StartsOn = DateTime.Now,
                OrganiserId = "user123",
                Id = 2
            });

            var eventId = 2;

            await repo.SaveChangesAsync();

            var result = await eventService.ExistsAsync(eventId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllEventsTest()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Event()
            {
                Name = "Test",
                Description = "Test",
                StartsOn = DateTime.Now,
                Location = "Test Location",
                Organiser = user,
                Id = 1
            });
            await repo.AddAsync(new Event()
            {
                Name = "Test Second",
                Description = "Test Second",
                StartsOn = DateTime.Now,
                Location = "Test Location Second",
                Organiser = user,
                Id = 2
            });
            await repo.AddAsync(new Event()
            {
                Name = "Test Third",
                Description = "Test Third",
                StartsOn = DateTime.Now,
                Location = "Test Location Third",
                Organiser = user,
                Id = 3
            });

            await repo.SaveChangesAsync();

            var result = await eventService.GetAllEventsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task EditEventASyncTestMethod()
        {
            var repo = new Repository(context);

            eventService = new EventService(repo);

            await repo.AddAsync(new Event()
            {
                Name = "Test",
                Description = "Test",
                Location = "Test Location",
                StartsOn = DateTime.Parse("01/01/2020 15:00"),
                Id = 1
            });

            await repo.SaveChangesAsync();

            await eventService.EditEventAsync(1, new EventFormViewModel()
            {
                Name = "Argos walks",
                Description = "",
                Location = "Test Location Park Edit",
                StartsOn = "01/01/2023 15:00",
                Id=1
            });

            var dbEvent = await repo.GetByIdAsync<Event>(1);

            Assert.That(dbEvent.Name, Is.EqualTo("Argos walks"));
            Assert.That(dbEvent.StartsOn.ToString(EventStartDateFormat), Is.EqualTo("01/01/2023 15:00"));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
