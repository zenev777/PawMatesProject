using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Models.PostViewModels;
using PawMates.Core.Services.PetService;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System.Drawing;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Core.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;

        //private Dictionary<int,string> LikedByUserIds ;
   
        //private int likeCounter = 0;

        public PostService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreatePostAsync(PostFormViewModel model, string creatorId)
        {
            if (await repository.AlreadyExistAsync<Post>(
            p => p.CreatorId == creatorId
            && p.ImageUrl == model.ImageUrl))
            {
                return false;
            }

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

        public async Task<IEnumerable<PostViewInfoModel>> GetPostsForPageAsync(int skip, int pageSize)
        {
            var listOfPosts = await repository
                .AllReadOnly<Post>()
                .Select(p => new PostViewInfoModel()
                {
                  Id = p.Id,
                  Creator = p.Creator.UserName,
                  Description = p.Description,
                  ImageUrl = p.ImageUrl,
                })
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            if (listOfPosts.Count % pageSize == 0)
            {
                MaxPage = listOfPosts.Count / pageSize;
            }
            else
            {
                MaxPage = (listOfPosts.Count / pageSize) + 1;
            }

            return await repository
                .AllReadOnly<Post>()
                .OrderByDescending(p => p.Id)
                .Take(pageSize + skip) 
                .Select(p => new PostViewInfoModel()
                {
                    Id = p.Id,
                    Creator = p.Creator.UserName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Likes = p.LikePosts.Count()
                })
                .ToListAsync();

        }


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

        public async Task<Post> UpdateLikes(int id, string userId)
        {
            var post = await repository.GetByIdAsync<Post>(id);

            var like = new LikePost()
            {
                Post = post,
                PostId = post.Id,
                UserId = userId
            };


            var likeToAdd = await repository.AlreadyExistAsync<LikePost>(l=> l.PostId == id && l.UserId == userId);

            if (likeToAdd == false)
            {
                post.LikePosts.Add(like);
                await repository.AddAsync(like);
            }
            else
            {
                repository.Delete(like);
            }

            await repository.SaveChangesAsync();

            return post;
        }
    }
}
