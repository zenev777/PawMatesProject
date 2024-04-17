using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PostViewModels;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Contracts.PostInterface
{
    public interface IPostService
    {
        public Task<IEnumerable<PostViewInfoModel>> GetPostsForPageAsync(int page, int pageSize);

        Task<bool> CreatePostAsync(PostFormViewModel model, string id);

        Task DeleteAsync(int postId);

        Task<Post> PostByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> SameCreatorAsync(int postId, string currentCreatorId);

        Task<Post> UpdateLikes(int id, string userId);
    }
}
