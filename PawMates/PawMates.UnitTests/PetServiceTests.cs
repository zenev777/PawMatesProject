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
                .UseInMemoryDatabase("PawMatesDb")
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
                DateOfBirth= DateTime.Now,
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

            Assert.That(dbPet.Name,Is.EqualTo("Fluffy"));
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


            Assert.That(pets.Count(),Is.EqualTo(1));
        }


        //[Test]
        //public async Task AllAsync_WithFilters_ReturnsFilteredPets()
        //{

        //    var cat = new PetType { Name = "Cat" };
        //    var dog = new PetType { Name = "Dog" };
        //    var pets = new List<Pet>
        //    {
        //        new Pet { Name = "Fluffy", PetType = cat, Breed = "Persian" },
        //        new Pet { Name = "Bibby", PetType = dog, Breed = "Labrador" }
        //    }.AsQueryable();


        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(repo => repo.AllReadOnly<Pet>())
        //                  .Returns(pets);

        //    var result = await petService.AllAsync(petType: "Cat");


        //    //Assert.That(result.Pets.Count(), Is.EqualTo(1));
        //    Assert.That(result.Pets.CountName.Contains("Fluffy"));
        //}


        //[Test]
        //public async Task AllPetTypeNamesAsync_ReturnsDistinctPetTypeNames()
        //{
        //    var petTypes = new List<PetType>
        //    {
        //        new PetType { Name = "Cat" },
        //        new PetType { Name = "Dog" }
        //    }.AsQueryable();

        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(repo => repo.AllReadOnly<PetType>())
        //                  .Returns(petTypes);

        //    var result = await petService.AllPetTypeNamesAsync();

        //    string cat = "Cat";
        //    string dog = "Dog";

        //    Assert.That(result.Count(), Is.EqualTo(2));
        //    Assert.That(result.Contains(cat));
        //    Assert.That(result.Contains(dog));
        //}

        //[Test]
        //public async Task CreatePetAsync_ReturnsTrue()
        //{
        //    var model = new PetFormViewModel()
        //    {
        //        Name = "Fluffy",
        //        Breed = "Persian",
        //        DateOfBirth = "2019-01-01",
        //        Weight = 5,
        //        Gender = Gender.Male,
        //        MainColor = "White",
        //        SecondaryColor = "Brown",
        //        PetTypeId = 1,
        //        ImageUrl = "fluffy.jpg"
        //    };
        //    var userId = "user123";

        //    var result = await petService.CreatePetAsync(model, userId);

        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public async Task EditPetAsync_ValidModel_ReturnsId()
        //{
        //    var petId = 1;
        //    var model = new PetFormViewModel()
        //    {
        //        Name = "Fluffy",
        //        Breed = "Persian",
        //        DateOfBirth = "2019-01-01",
        //        Weight = 5,
        //        Gender = Gender.Male,
        //        MainColor = "White",
        //        SecondaryColor = "Brown",
        //        PetTypeId = 1,
        //        ImageUrl = "fluffy.jpg"
        //    };

        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(repo => repo.GetByIdAsync<Pet>(petId))
        //                  .ReturnsAsync(new Pet { Id = petId });

        //    var result = await petService.EditPetAsync(petId, model);

        //    Assert.That(result, Is.EqualTo(petId));
        //}

        //[Test]
        //public async Task GetPetDetailsAsync_ValidId_ReturnsPetInfoViewModel()
        //{
        //    var petId = 1;
        //    var pet = new Pet
        //    {
        //        Id = petId,
        //        Name = "Fluffy",
        //        DateOfBirth = new System.DateTime(2019, 01, 01),
        //        ImageUrl = "fluffy.jpg",
        //        PetType = new PetType { Name = "Cat" },
        //        Breed = "Persian",
        //        Gender = Gender.Male,
        //        MainColor = "White",
        //        SecondaryColor = "Brown",
        //        Weight = 5,
        //        OwnerId = "user123"
        //    };

        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(repo => repo.GetByIdAsync<Pet>(petId))
        //                  .ReturnsAsync(pet);

        //    var result = await petService.GetPetDetailsAsync(petId);

        //    Assert.NotNull(result);
        //    Assert.That(result.Name, Is.EqualTo("Fluffy"));        
        //    Assert.That(result.Breed, Is.EqualTo("Persian"));

        //}

        ////[Test]
        ////public async Task DeleteAsync_ValidPetId_DeletesPet()
        ////{
        ////    var petId = 1;

        ////    var mockRepository = new Mock<IRepository>();
        ////    mockRepository.Setup(repo => repo.GetByIdAsync<Pet>(petId))
        ////                  .ReturnsAsync(new Pet { Id = petId });

        ////    await petService.DeleteAsync(petId);

        ////    mockRepository.Verify(repo => repo.Delete(It.IsAny<Pet>()), Times.Once());
        ////}

        //[Test]
        //public async Task ExistsAsync_ExistingPetId_ReturnsTrue()
        //{
        //    var petId = 1;

        //    var mockRepository = new Mock<IRepository>();
        //    mockRepository.Setup(repo => repo.AllReadOnly<Pet>())
        //                  .Returns(new List<Pet> { new Pet { Id = petId } }.AsQueryable());

        //    var result = await petService.ExistsAsync(petId);

        //    Assert.IsTrue(result);
        //}

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
