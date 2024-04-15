using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawMates.Core.Contracts.Admin;
using PawMates.Core.Services.Admin;
using PawMates.Core.Services.PostService;

namespace PawMates.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IRepository repository;
        private IUserService userService;
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
        public void CreateRole_ReturnsRoleWithCorrectProperties()
        {
            userService = new UserService();
            
            var roleName = "Admin";

            var role = userService.CreateRole(roleName);

            Assert.IsNotNull(role);
            Assert.That(role.Name, Is.EqualTo(roleName));
            Assert.That(role.NormalizedName, Is.EqualTo(roleName.ToUpper()));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
