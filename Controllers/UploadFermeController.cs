using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using SocomecApp.Models;
using System.Globalization;
using CsvHelper.FastDynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Text.RegularExpressions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace SocomecApp.Controllers
{
    public class UploadFermeController : Controller
    {
        private readonly SocomecContext _context;
        public UploadFermeController(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCSVFERME(IFormFile file)
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
                    FileFERME Ferme = new FileFERME();

                    string[] rows = sreader.ReadLine().Split(';');


                    Ferme.Ordre = rows[0].ToString();
                    Ferme.Article = rows[1].ToString();
                    Ferme.Projet = RemplirColonneProjet(Ferme.Article);
                    Ferme.Magasin = rows[3].ToString();
                    Ferme.QteOrdre = rows[4].ToString();
                    Ferme.QteLiv = rows[5].ToString();
                    Ferme.Centre = rows[6].ToString();
                    Ferme.TempsFabrication = rows[7].ToString();
                    Ferme.DateDebutRest = DateTime.Parse(rows[8].ToString());
                    Ferme.Annee = DateTime.Parse(rows[8].ToString()).Year.ToString();
                    Ferme.Mois = DateTime.Parse(rows[8].ToString()).Month.ToString();
                    _context.FileFERME.Add(Ferme);
                   
                }
                _context.SaveChangesAsync();
            }
            return Redirect("~/Upload/Index");
        }

        //affichage
        public IActionResult AfficherFerme()
        {
            return View(_context.FileFERME.ToList());
        }

        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE FileFERME'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();

            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
        }

        private string RemplirColonneProjet(string article)
        {
            string x = article.Replace(" ", string.Empty);
            
            FileFil fileFil = _context.FileFil.FirstOrDefault(s => s.ArticleMPS==x);
            if (fileFil != null)
            {
                return fileFil.Fil.ToString();
            }
            else
                return "NA";
        }

        

    }
}