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
    public class UploadMPSController : Controller
    {
        private readonly SocomecContext _context;
        public UploadMPSController(SocomecContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadCSVMPS(IFormFile file) {
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
                    FileMPS MPS = new FileMPS();

                    string[] rows = sreader.ReadLine().Split(';');

                    MPS.Centre = rows[0].ToString();
                    MPS.DateFabrication = DateTime.Parse(rows[1].ToString());
                    MPS.CapaciteRequise = rows[2].ToString();
                    MPS.OrdreType = rows[3].ToString();
                    MPS.Ordre = rows[4].ToString();
                    MPS.ArticleMPS = rows[5].ToString();
                    MPS.Ordre2 = rows[6].ToString();
                    MPS.QteOrdre = rows[7].ToString();
                    MPS.Annee = DateTime.Parse(rows[1].ToString()).Year.ToString();
                    MPS.Mois = DateTime.Parse(rows[1].ToString()).Month.ToString();
                    MPS.Fil= RemplirColonneProjet(MPS.ArticleMPS);
                    MPS.DateFin = RemplirColonneDateFin(MPS.DateFabrication);
                    _context.FileMPS.Add(MPS);
                    
                }
                _context.SaveChangesAsync();
            }
            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherMPS()
        {
            return View(_context.FileMPS.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE FileMPS'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();
            
            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
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
        private DateTime RemplirColonneDateFin(DateTime datefabrication)
        {
            FileMPSDF fileMPSDFItem = _context.FileMPSDF.FirstOrDefault(df => df.DteDebPlanif == datefabrication);
                if (fileMPSDFItem != null)
                {
                    // Mettre à jour la colonne DateFin dans la table FileMPS
                    return fileMPSDFItem.DteFinPlanif;
                }
                else return DateTime.MinValue;
                    
        }
    }
}
