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
    public class UploadOVFac : Controller
    {
        private readonly SocomecContext _context;
        public UploadOVFac(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCSVOVFac(IFormFile file)
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
                    OVFac ovf = new OVFac();


                    string[] rows = sreader.ReadLine().Split(';');

                    ovf.Ov = rows[0].ToString();
                    ovf.Pos = rows[1].ToString();
                    ovf.Li = rows[2].ToString();
                    ovf.Zone = DateTime.Parse(rows[3].ToString());
                    ovf.Pays = rows[4].ToString();
                    ovf.Magasin = rows[5].ToString();
                    ovf.Statu = rows[6].ToString();
                    ovf.TypeOrd = rows[7].ToString();
                    ovf.Client = rows[8].ToString();
                    ovf.Article = rows[10].ToString();
                    ovf.Projet = RemplirColonneProjet(ovf.Article);
                    ovf.TypeArt = rows[11].ToString();
                    ovf.Qte = rows[12].ToString();
                    ovf.Livree = rows[13].ToString();
                    ovf.Reliquat = rows[14].ToString();
                    ovf.Unite = rows[15].ToString();
                    ovf.Montant = rows[16].ToString();
                    ovf.DateCreation = DateTime.Parse(rows[17].ToString());
                    ovf.HeureCreation = rows[18].ToString();
                    ovf.DateValC = DateTime.Parse(rows[19].ToString());
                    ovf.HeureValC = rows[20].ToString();
                    ovf.DateAR = DateTime.Parse(rows[21].ToString());
                    ovf.HeureAL = rows[22].ToString();
                    ovf.DateLivPlanif = DateTime.Parse(rows[23].ToString());
                    ovf.DateLivDemandee = DateTime.Parse(rows[24].ToString());
                    ovf.DateLivConfirmee = DateTime.Parse(rows[25].ToString());
                    ovf.DateLivRevisee = DateTime.Parse(rows[26].ToString());
                    ovf.DateFacture = DateTime.Parse(rows[38].ToString());
                    ovf.Annee = DateTime.Parse(rows[38].ToString()).Year.ToString();
                    ovf.Mois = DateTime.Parse(rows[38].ToString()).Month.ToString();
                    ovf.PRS = RemplirColonnePRS(ovf.Article);
                    ovf.PO = (float.Parse(ovf.PRS) * float.Parse(ovf.Qte)).ToString();

                    _context.OVFac.Add(ovf);
                }
                _context.SaveChangesAsync();

            }

            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherOVFac()
        {
            return View(_context.OVFac.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE OVFac'; " +
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
