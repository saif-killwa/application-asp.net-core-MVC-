using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using SocomecApp.Models;
using System.Globalization;
using CsvHelper.FastDynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace SocomecApp.Controllers
{
    public class UploadPRS : Controller
    {
        private readonly SocomecContext _context;
        public UploadPRS(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCSVPRS(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                    ModelState.AddModelError("", "Uploaded file is empty or null.");
                if (!file.FileName.EndsWith(".csv"))
                    ModelState.AddModelError(nameof(uploadApp.file), "Veuillez sélectionner un fichier CSV à importer");

            }
            TruncateDatabase();
            using (var sreader = new StreamReader(file.OpenReadStream()))
            {

                //First line is header. If header is not passed in csv then we can neglect the below line.
                string[] headers = sreader.ReadLine().Split(';');
                //Loop through the records


                while (!sreader.EndOfStream)
                {
                    PRS prs = new PRS();


                    string[] rows = sreader.ReadLine().Split(';');


                    prs.Article = rows[0].ToString();
                    prs.Description = rows[1].ToString();
                    prs.Consommation = rows[2].ToString();
                    prs.CoutsMatieres = rows[3].ToString();
                    prs.CoutsOperatoires = rows[4].ToString();
                    prs.PrixRevient = rows[5].ToString();

                    _context.PRS.Add(prs);
                }
                _context.SaveChangesAsync();

            }

            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherPRS()
        {
            return View(_context.PRS.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE PRS'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();

            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
        }
    }
}
