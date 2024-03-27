using Microsoft.AspNetCore.Mvc;

namespace PawMates.Controllers
{
	public class PetStatusController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
