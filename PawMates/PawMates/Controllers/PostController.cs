using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Services.EventService;

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
    }
}
