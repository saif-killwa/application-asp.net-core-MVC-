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
    public class UploadPOV : Controller
    {
        private readonly SocomecContext _context;
        public UploadPOV(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCSVPOV(IFormFile file)
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
                    POV pov = new POV();


                    string[] rows = sreader.ReadLine().Split(';');

                    pov.Articleplan = rows[0].ToString();
                    pov.Date = DateTime.Parse(rows[1].ToString());
                    pov.Prev = rows[2].ToString();
                    pov.PExep = rows[3].ToString();
                    pov.Ov = rows[4].ToString();
                    pov.DemEcl = rows[5].ToString();
                    pov.LivVen = rows[6].ToString();
                    pov.LivInt = rows[7].ToString();
                    pov.StockPlanif = rows[8].ToString();
                    pov.Annee = DateTime.Parse(rows[1].ToString()).Year.ToString();
                    pov.Mois = DateTime.Parse(rows[1].ToString()).Month.ToString();
                    pov.PRS = RemplirColonnePRS(pov.Articleplan);
                    pov.PO = ( float.Parse(pov.PRS) * ( float.Parse(pov.Prev)-float.Parse(pov.Ov) ) ).ToString();
                    pov.Fil = RemplirColonneProjet(pov.Articleplan);

                    _context.POV.Add(pov);
                }
                _context.SaveChangesAsync();

            }

            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherPOV()
        {
            return View(_context.POV.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE POV'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();

            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
        }
        private string RemplirColonnePRS(string article)
        {
            string x = article.Replace(" ", string.Empty);

            PRS pRS = _context.PRS.FirstOrDefault(s => s.Article == x);
            if (pRS != null)
            {
                return pRS.PrixRevient.ToString();
            }
            else
                return "NA";
        }
        private string RemplirColonneProjet(string article)
        {
            string x = article.Replace(" ", string.Empty);

            FileFil fileFil = _context.FileFil.FirstOrDefault(s => s.ArticleMPS == x);
            if (fileFil != null)
            {
                return fileFil.Fil.ToString();
            }
            else
                return "NA";
        }
    }
}
