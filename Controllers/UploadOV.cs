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
    public class UploadOV : Controller
    {
        private readonly SocomecContext _context;
        public UploadOV(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCSVOV(IFormFile file)
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
                    OV ov = new OV();


                    string[] rows = sreader.ReadLine().Split(';');

                    ov.Ov = rows[0].ToString();
                    ov.Pos = rows[1].ToString();
                    ov.Li = rows[2].ToString();
                    ov.Zone = DateTime.Parse(rows[3].ToString());
                    ov.Pays = rows[4].ToString();
                    ov.Magasin = rows[5].ToString();
                    ov.Statu = rows[6].ToString();
                    ov.TypeOrd = rows[7].ToString();
                    ov.Client = rows[8].ToString();
                    ov.Article = rows[10].ToString();
                    ov.Projet = RemplirColonneProjet(ov.Article);
                    ov.TypeArt = rows[11].ToString();
                    ov.Qte = rows[12].ToString();
                    ov.Livree = rows[13].ToString();
                    ov.Reliquat = rows[14].ToString();
                    ov.Unite = rows[15].ToString();
                    ov.Montant = rows[16].ToString();
                    ov.DateCreation = DateTime.Parse(rows[17].ToString());
                    ov.HeureCreation = rows[18].ToString();
                    ov.DateValC = DateTime.Parse(rows[19].ToString());
                    ov.HeureValC = rows[20].ToString();
                    ov.DateAR = DateTime.Parse(rows[21].ToString());
                    ov.HeureAL = rows[22].ToString();
                    ov.DateLivPlanif = DateTime.Parse(rows[23].ToString());
                    ov.DateLivDemandee = DateTime.Parse(rows[24].ToString());
                    ov.DateLivConfirmee = DateTime.Parse(rows[25].ToString());
                    ov.DateLivRevisee = DateTime.Parse(rows[26].ToString());
                    ov.Annee = DateTime.Parse(rows[23].ToString()).Year.ToString();
                    ov.Mois = DateTime.Parse(rows[23].ToString()).Month.ToString();
                    ov.PRS = RemplirColonnePRS(ov.Article);
                    ov.PO = (float.Parse(ov.PRS) * float.Parse(ov.Qte)).ToString();

                    _context.OV.Add(ov);
                }
                _context.SaveChangesAsync();

            }

            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherOV()
        {
            return View(_context.OV.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE OV'; " +
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

    }
}
