using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocomecApp.Models;
using System.Linq;



namespace SocomecApp.Controllers
{
    [Authorize]
    public class AffichageController : Controller
    {
        private readonly SocomecContext _context;
        public AffichageController(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Afficher()
        {
            var fermeData = _context.FileFERME.ToList();
            var mpsData = _context.FileMPS.ToList();
            var mrpData = _context.FileMRP.ToList();
            var mpsdfData = _context.FileMPSDF.ToList();
            var mrpdfData = _context.FileMRPDF.ToList();
            var renduData = _context.Rendu.ToList();
            var filData = _context.FileFil.ToList();
            var ovData = _context.OV.ToList();
            var ovFacData = _context.OVFac.ToList();
            var povData = _context.POV.ToList();
            var prsData = _context.PRS.ToList();
            var zrpData = _context.ZRP.ToList();

            var viewModel = new AffichageViewModel {
                FileFERME = fermeData,
                FileFil = filData,
                FileMPS = mpsData,
                FileMRP = mrpData,
                FileMPSDF = mpsdfData,
                FileMRPDF = mrpdfData,
                Rendu = renduData,
                OV = ovData,
                OVFac = ovFacData,
                POV = povData,
                PRS = prsData,
                ZRP = zrpData,
            };
            return View(viewModel);
        }
    }
}
