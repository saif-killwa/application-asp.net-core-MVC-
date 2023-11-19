using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocomecApp.Models;

namespace SocomecApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SocomecContext _context;
        public HomeController(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Recaps.ToList());
        }
    }
}
