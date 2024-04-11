using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PostViewModels;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;

        public PostService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreatePostAsync(PostFormViewModel model, string creatorId)
        {
            if (await repository.AlreadyExistAsync<Post>(
            p => p.Creator.UserName == model.Creator
            && p.Description == model.Description
            && p.ImageUrl == model.ImageUrl))
                throw new ApplicationException("Event already exists");

            var entity = new Post()
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CreatorId = creatorId,
                Id = model.Id,
            };

            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int postId)
        {
            var postToDelete = await repository.GetByIdAsync<Post>(postId);
            repository.Delete(postToDelete);

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await repository.AllReadOnly<Post>().AnyAsync(p => p.Id == id);
        }

        //public async Task<IEnumerable<PostViewInfoModel>> GetAllPostsAsync(int skip, int take)
        //{
        //    //return await repository
        //    //  .AllReadOnly<Post>()
        //    //  .Select(p => new PostViewInfoModel()
        //    //  {
        //    //    Id = p.Id,
        //    //    Creator = p.Creator.UserName,
        //    //    Description = p.Description,
        //    //    ImageUrl = p.ImageUrl,
        //    //  })
        //    //  .OrderByDescending(p => p.Id)
        //    //  .ToListAsync();

        //    return await repository
        //        .AllReadOnly<Post>()
        //        .OrderByDescending(p => p.Id)
        //        .Skip(skip) // Skip the specified number of posts
        //        .Take(take) // Take the specified number of posts for the current page
        //        .Select(p => new PostViewInfoModel()
        //        {
        //            Id = p.Id,
        //            Creator = p.Creator.UserName,
        //            Description = p.Description,
        //            ImageUrl = p.ImageUrl,
        //        })
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<PostViewInfoModel>> GetPostsForPageAsync(int skip, int pageSize)
        {
            return await repository
                .AllReadOnly<Post>()
                .OrderByDescending(p => p.Id)
                .Skip(skip) // Skip the specified number of posts
                .Take(pageSize) // Take the specified number of posts for the current page
                .Select(p => new PostViewInfoModel()
                {
                    Id = p.Id,
                    Creator = p.Creator.UserName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                })
                .ToListAsync();
        }

        //return await repository
        //      .AllReadOnly<Post>()
        //      .Select(p => new PostViewInfoModel()
        //      {
        //        Id = p.Id,
        //        Creator = p.Creator.UserName,
        //        Description = p.Description,
        //        ImageUrl = p.ImageUrl,
        //      })
        //      .OrderByDescending(p => p.Id)
        //      .ToListAsync();

        public async Task<Post> PostByIdAsync(int id)
        {
            return await repository.AllReadOnly<Post>()
               .Where(p => p.Id == id).FirstAsync();
        }

        public async Task<bool> SameCreatorAsync(int postId, string currentCreatorId)
        {
            bool result = false;
            var post = await repository.AllReadOnly<Post>()
                .Where(p => p.Id == postId)
                .FirstOrDefaultAsync();

            if (post?.CreatorId == currentCreatorId)
            {
                result = true;
            }

            return result;
        }
    }
}
