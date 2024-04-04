using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PostViewModels;
using PawMates.Core.Services.EventService;
using PawMates.Extensions;

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
        public async Task<IActionResult> All()
        {
            var model = await postService.GetAllPostsAsync();

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
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }
    }
}
