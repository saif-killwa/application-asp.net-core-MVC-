using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocomecApp.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
