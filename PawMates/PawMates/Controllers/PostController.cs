using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PetViewModels;
using PawMates.Core.Models.PostViewModels;
using PawMates.Core.Services.EventService;
using PawMates.Core.Services.PostService;
using PawMates.Extensions;
using System.Globalization;

namespace PawMates.Controllers
{
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


            var model = await postService.GetPostsForPageAsync(skip, pageSize);

            if (page==1)
            {
                return View(model);
            }
            else
            {
                return View("~/Views/Post/PostCards.cshtml",model);
            }

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
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await postService.ExistsAsync(id) == false))
            {
                return RedirectToAction(nameof(All));
            }

            if (await postService.SameCreatorAsync(id, User.Id()) == false)
            {
                return RedirectToAction(nameof(All));
            };

            var postToDelete = await postService.PostByIdAsync(id);

            var model = new PostDeleteViewModel()
            {
                Id = postToDelete.Id,
                ImageUrl = postToDelete.ImageUrl,
            };

            if (postToDelete == null)
            {
                return BadRequest();
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(PetDeleteViewModel model)
        {
            if ((await postService.ExistsAsync(model.Id) == false))
            {
                return RedirectToAction(nameof(All));
            }

            if (await postService.SameCreatorAsync(model.Id, User.Id()) == false)
            {
                return RedirectToAction(nameof(All));
            };

            await postService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
