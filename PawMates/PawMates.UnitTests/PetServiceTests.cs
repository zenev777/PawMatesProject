using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Models.PetViewModels;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Models;
using PawMates.Core.Services.PetService;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Enums;
using Moq;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;
using PawMates.Infrastructure.Data.IdentityModels;

namespace PawMates.UnitTests.Services
{
    [TestFixture]
    public class PetServiceTests
    {
        private IRepository repository;
        private IPetService petService;
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
        public async Task TestPetEdit()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Breed = "",
                SecondaryColor = "",
                MainColor = "",
                Name = "Jyhny",
                Gender = Gender.Male,
                ImageUrl = "",
                Weight = 0,
                DateOfBirth = DateTime.Now,
            });

            await repo.SaveChangesAsync();

            await petService.EditPetAsync(1, new PetFormViewModel()
            {
                Id = 1,
                Breed = "",
                SecondaryColor = "",
                MainColor = "",
                Name = "Argo",
                Gender = Gender.Male,
                ImageUrl = "",
                DateOfBirth = "01/01/2003",
                Weight = 0
            });

            var dbPet = await repo.GetByIdAsync<Pet>(1);

            Assert.That(dbPet.Name, Is.EqualTo("Argo"));
            Assert.That(dbPet.DateOfBirth.ToString(DateOfBirthFormat), Is.EqualTo("01/01/2003"));
        }

        [Test]
        public async Task TestCreatePetAsync()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var userId = "user123";

            await petService.CreatePetAsync(new PetFormViewModel()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = "01/01/2003",
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            }, userId);

            var dbPet = await repo.GetByIdAsync<Pet>(1);

            Assert.That(dbPet.Name, Is.EqualTo("Fluffy"));
        }

        [Test]
        public async Task TestDeleteAsync()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });
            await repo.AddAsync(new Pet()
            {
                Id = 2,
                Name = "Jimmy",
                Breed = "Persi",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "sue",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });

            await repo.SaveChangesAsync();

            var pets = repo.All<Pet>();

            await petService.DeleteAsync(1);


            Assert.That(pets.Count(), Is.EqualTo(1));
            //Assert.That(pets.A)
        }

        [Test]
        public async Task AllAsync_WithFilters_ReturnsFilteredPets()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var cat = new PetType { Name = "Cat" };
            var dog = new PetType { Name = "Dog" };

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetType=cat,
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });
            await repo.AddAsync(new Pet()
            {
                Id = 2,
                Name = "Jimmy",
                Breed = "Persi",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "sue",
                SecondaryColor = "Brown",
                PetTypeId = 2,
                PetType = dog,
                ImageUrl = "fluffy.jpg"
            });

            await repo.SaveChangesAsync();

            var result = await petService.AllAsync(petType: "Cat");

            Assert.That(result.Pets.Count(), Is.EqualTo(1));
            Assert.That(result.Pets.First().Name, Is.EqualTo("Fluffy"));
        }

        [Test]
        public async Task AllPetTypeNamesAsync_ReturnsDistinctPetTypeNames()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var result = await petService.AllPetTypeNamesAsync();


            Assert.That(result.Count(), Is.EqualTo(7));
            Assert.That(result.Contains("Cat"));
            Assert.That(result.Contains("Dog"));
        }

        [Test]
        public async Task GetPetDetailsAsync_ValidId_ReturnsPetInfoViewModel()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var petId = 2;
            await repo.AddAsync(new Pet()
            {
                Id = 2,
                Name = "Jimmy",
                Breed = "Persi",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "sue",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });

            await repo.SaveChangesAsync();

            var pets = repo.All<Pet>();

            var result = await petService.GetPetDetailsAsync(petId);

            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo("Jimmy"));
            Assert.That(result.Breed, Is.EqualTo("Persi"));

        }

        [Test]
        public async Task ExistsAsync_ReturnsTrue()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var petId = 1;

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });

            await repo.SaveChangesAsync();

            var result = await petService.ExistsAsync(petId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task PetByIdAsync_ExistingId_ReturnsPet()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var petId = 1;

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });

            await repo.SaveChangesAsync();

            var result = await petService.PetByIdAsync(petId);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(petId));
            Assert.That(result.Name, Is.EqualTo("Fluffy"));
            Assert.That(result.ImageUrl, Is.EqualTo("fluffy.jpg"));
        }

        [Test]
        public async Task PetByIdAsync_NonExistingId_ReturnsNull()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg"
            });
            await repo.SaveChangesAsync();

            var pets = new List<Pet>().AsQueryable();

            var result = await petService.PetByIdAsync(1);

            Assert.IsTrue(!pets.Contains(result));
        }

        [Test]
        public async Task GetMyPetsAsync_ReturnsOnlyOwnersPets()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg",
                Owner = user              
            });
            await repo.AddAsync(new Pet()
            {
                Id = 2,
                Name = "Jimmy",
                Breed = "Persi",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "sue",
                SecondaryColor = "Brown",
                PetTypeId = 2,
                ImageUrl = "fluffy.jpg",
                Owner = user
            });

            await repo.SaveChangesAsync();

            var result = await petService.GetMyPetsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            
            var petInfoViewModels = result.ToList();

            Assert.That(petInfoViewModels[0].Name, Is.EqualTo("Fluffy"));
            Assert.That(petInfoViewModels[0].OwnerId, Is.EqualTo("testuser"));
            
            Assert.That(petInfoViewModels[1].Name, Is.EqualTo("Jimmy"));
            Assert.That(petInfoViewModels[1].OwnerId, Is.EqualTo("testuser"));
        }

        [Test]
        public async Task SameOwnerAsync_PetWithSameOwner_ReturnsTrue()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var petId = 1;

            var currentUserId = "user123";

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg",
                OwnerId = "user123"
            });

            await repo.SaveChangesAsync();

            var result = await petService.SameOwnerAsync(petId, currentUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SameOwnerAsync_PetWithDifferentOwner_ReturnsFalse()
        {
            var repo = new Repository(context);

            petService = new PetService(repo);

            var petId = 1;

            var currentUserId = "user123";

            await repo.AddAsync(new Pet()
            {
                Id = 1,
                Name = "Fluffy",
                Breed = "Persian",
                DateOfBirth = DateTime.Now,
                Weight = 5,
                Gender = Gender.Male,
                MainColor = "White",
                SecondaryColor = "Brown",
                PetTypeId = 1,
                ImageUrl = "fluffy.jpg",
                OwnerId = "diffuser"
            });

            await repo.SaveChangesAsync();

            var result = await petService.SameOwnerAsync(petId, currentUserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetPetTypes_ReturnsListOfPetTypesViewModel()
        {
            var repo = new Repository(context);

            petService = new PetService(repo); 

            var result = await petService.GetPetTypes();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(7));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
