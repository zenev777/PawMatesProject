using Microsoft.AspNetCore.Mvc;

namespace PawMates.Controllers
{
    public class PetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
