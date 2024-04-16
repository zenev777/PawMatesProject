using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PawMates.Areas.AdministrativeDirector.Controllers
{
    [Area("AdministrativeDirector")]
    [Authorize("AdministrativeDirector")]
    public class AdministrativeDirectorBaseController : Controller
    { 
    }
}
