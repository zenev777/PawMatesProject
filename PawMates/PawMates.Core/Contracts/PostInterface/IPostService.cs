using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Contracts.PostInterface
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewInfoModel>> GetAllPostsAsync();

        Task<bool> CreatePostAsync(PostFormViewModel model, string id);
    }
}
