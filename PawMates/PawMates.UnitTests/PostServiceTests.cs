using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawMates.Core.Models.PostViewModels;
using PawMates.Core.Services.PostService;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Services.PetService;
using PawMates.Infrastructure.Data.Models;
using PawMates.Data.Migrations;
using PawMates.Infrastructure.Data.Enums;
using Moq;
using PawMates.Infrastructure.Data.IdentityModels;

namespace PawMates.UnitTests
{
    [TestFixture]
    public class PostServiceTests
    {
        private IRepository repository;
        private IPostService postService;
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
        public async Task CreatePostAsync_CreatesNewPost()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            var creatorId = "creator123";

            var result = await postService.CreatePostAsync((new PostFormViewModel()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                Id = 1 // assuming unique ID for testing
            }), creatorId);

            var dbPost = await repo.GetByIdAsync<Post>(1);

            Assert.IsTrue(result);
            Assert.That(dbPost.ImageUrl, Is.EqualTo("test.jpg"));

        }

        [Test]
        public async Task PostByIdAsync_ExistingId_ReturnsPost()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            var postId = 1;

            await repo.AddAsync(new Post()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                Id = 1
            });

            await repo.SaveChangesAsync();

            var result = await postService.PostByIdAsync(postId);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(postId));
            Assert.That(result.ImageUrl, Is.EqualTo("test.jpg"));
        }

        [Test]
        public async Task PostByIdAsync_NonExistingId()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            await repo.AddAsync(new Post()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                Id = 1
            });

            await repo.SaveChangesAsync();

            var posts = new List<Post>().AsQueryable();

            var result = await postService.PostByIdAsync(1);

            Assert.IsTrue(!posts.Contains(result));
        }

        //[Test]
        //public async Task CreatePostAsync_ThrowsException_WhenPostAlreadyExists()
        //{
        //    var repo = new Repository(context);

        //    postService = new PostService(repo);

        //    var creatorId = "creator123";

        //    var result = await postService.CreatePostAsync((new PostFormViewModel()
        //    {
        //        Description = "Test description",
        //        ImageUrl = "test.jpg",
        //        Id = 1 // assuming unique ID for testing
        //    }), creatorId);

        //    var post = await repo.GetByIdAsync<Post>(1);

        //    var model = new PostFormViewModel
        //    {
        //        Description = post.Description,
        //        ImageUrl = post.ImageUrl,
        //        Creator = post.CreatorId
        //    };

        //    Assert.ThrowsAsync<ApplicationException>(() => postService.CreatePostAsync(model, creatorId));
        //}

        [Test]
        public async Task TestDeletePostAsync()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            await repo.AddAsync(new Post()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                Id = 1 
            });
            await repo.AddAsync(new Post()
            {
                Description = "Test description second",
                ImageUrl = "testsecond.jpg",
                Id = 2 
            });

            await repo.SaveChangesAsync();

            var posts = repo.All<Post>();

            await postService.DeleteAsync(1);

            Assert.That(posts.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task SameCreatorAsync_PetWithSameCreator_ReturnsTrue()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            var currentCreatorId = "user123";
            
            await repo.AddAsync(new Post()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                CreatorId = currentCreatorId,
                Id = 1
            });

            var postId = 1;

            await repo.SaveChangesAsync();

            var result = await postService.SameCreatorAsync(postId, currentCreatorId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SameCreatorAsync_PetWithDiffCreator_ReturnsFalse()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            var currentCreatorId = "user123";

            await repo.AddAsync(new Post()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                CreatorId = "diffUser",
                Id = 1
            });

            var postId = 1;

            await repo.SaveChangesAsync();

            var result = await postService.SameCreatorAsync(postId, currentCreatorId);

            Assert.IsFalse(result);
        }


        [Test]
        public async Task ExistsAsync_ReturnsTrue()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            var currentCreatorId = "user123";

            await repo.AddAsync(new Post()
            {
                Description = "Test description",
                ImageUrl = "test.jpg",
                CreatorId = currentCreatorId,
                Id = 1
            });

            var postId = 1;

            await repo.SaveChangesAsync();

            var result = await postService.ExistsAsync(postId);

            Assert.IsTrue(result);
        }


        [Test]
        public async Task GetPostsForPageAsync_ReturnsPostsForSpecificPage()
        {
            var repo = new Repository(context);

            postService = new PostService(repo);

            var user = new ApplicationUser { Id = "user123", UserName = "testuser" };

            await repo.AddAsync(new Post()
            {
                Description = "Test 1",
                ImageUrl = "test1.jpg",
                Creator = user,
                Id = 1
            });
            await repo.AddAsync(new Post()
            {
                Description = "Test 2",
                ImageUrl = "test2.jpg",
                Creator = user,
                Id = 2
            });
            await repo.AddAsync(new Post()
            {
                Description = "Test 3",
                ImageUrl = "test3.jpg",
                Creator = user,
                Id = 3
            });

            await repo.SaveChangesAsync();

            var result = await postService.GetPostsForPageAsync(0, 2);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
