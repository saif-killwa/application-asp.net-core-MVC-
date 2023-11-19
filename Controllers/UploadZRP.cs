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
    public class UploadZRP : Controller
    {
        private readonly SocomecContext _context;
        public UploadZRP(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCSVZRP(IFormFile file)
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
                    ZRP zrp = new ZRP();


                    string[] rows = sreader.ReadLine().Split(';');

                    zrp.DtFabrication = DateTime.Parse(rows[0].ToString());
                    zrp.Pourcentage = rows[1].ToString();
                    zrp.Mach = rows[2].ToString();
                    zrp.De = rows[3].ToString();
                    zrp.A = rows[4].ToString();
                    zrp.Ordre = rows[5].ToString();
                    zrp.Pr = rows[6].ToString();
                    zrp.CodeArticle = rows[7].ToString();
                    zrp.DetailType = rows[8].ToString();
                    zrp.Mq = rows[9].ToString();
                    zrp.Op = rows[10].ToString();
                    zrp.Quantite = rows[11].ToString();
                    zrp.Temps = rows[12].ToString();
                    zrp.Pourc = rows[13].ToString();
                    zrp.Date = rows[14].ToString();
                    zrp.Multiplier = rows[15].ToString();
                    zrp.Couv = rows[16].ToString();
                    zrp.Couv2 = rows[17].ToString();
                    zrp.Varia = rows[18].ToString();
                    zrp.Syst = rows[19].ToString();
                    zrp.Planif = rows[20].ToString();
                    zrp.Statut = rows[21].ToString();
                    zrp.StatutOF = rows[22].ToString();
                    zrp.SCType = rows[23].ToString();

                    _context.ZRP.Add(zrp);
                }
                _context.SaveChangesAsync();

            }

            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherZRP()
        {
            return View(_context.ZRP.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE ZRP'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();

            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
        }
    }
}
