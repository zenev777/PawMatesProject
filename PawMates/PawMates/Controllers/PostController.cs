using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Models.PetViewModels;
using PawMates.Core.Models.PostViewModels;
using PawMates.Extensions;

namespace PawMates.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
        {
            // Define the number of posts per page
            int pageSize = 5; // Adjust this according to your needs

            // Calculate the offset based on the page number
            int skip = (page - 1) * pageSize;

            //Try to extend this varaebles
            var model = await postService.GetPostsForPageAsync(skip, pageSize);

            if (model == null)
            {
                return StatusCode(404); 
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PostFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormViewModel model)
        {
            var creatorId = User.Id();

            var result = await postService.CreatePostAsync(model, creatorId);

            if (!ModelState.IsValid)
            {
                return StatusCode(500);
            }

            if (result == false)
            {
                return StatusCode(500);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await postService.ExistsAsync(id) == false))
            {
                return StatusCode(500);
            }

            if (await postService.SameCreatorAsync(id, User.Id()) == false)
            {
                return StatusCode(403);
            };

            var postToDelete = await postService.PostByIdAsync(id);

            var model = new PostDeleteViewModel()
            {
                Id = postToDelete.Id,
                ImageUrl = postToDelete.ImageUrl,
            };

            if (postToDelete == null)
            {
                return StatusCode(404);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(PetDeleteViewModel model)
        {
            if ((await postService.ExistsAsync(model.Id) == false))
            {
                return StatusCode(500);
            }

            if (await postService.SameCreatorAsync(model.Id, User.Id()) == false)
            {
                return StatusCode(403);
            };

            await postService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
