using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PawMates.Core.Contracts.Admin;
using PawMates.Core.Models.Admin;
using PawMates.Core.Services.Admin;
using PawMates.Infrastructure.Data.IdentityModels;

namespace PawMates.Areas.AdministrativeDirector.Controllers
{
    public class HomeController : AdministrativeDirectorBaseController
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly IUserService userService;


        public HomeController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<ApplicationRole> _roleManager,
            IUserService _userService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            userService = _userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddRole()
        {
            var model = new AddRoleViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {

            if (await roleManager.RoleExistsAsync(model.RoleName) == false)
            {
                await roleManager.CreateAsync(userService.CreateRole(model.RoleName));
            };

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GiveRole()
        {
            var model = new UserToRoleViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GiveRole(UserToRoleViewModel model)
        {

            if (await roleManager.RoleExistsAsync(model.RoleName))
            {
                var user = await userManager.FindByNameAsync(model.UserName);



                if (await userManager.IsInRoleAsync(user, model.RoleName) == false
                    && user != null)
                {
                    await userManager.AddToRoleAsync(user, model.RoleName);
                }
            };

            return RedirectToAction("Index", "Home");
        }
    }
}
