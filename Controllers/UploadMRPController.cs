using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocomecApp.Models;

namespace SocomecApp.Controllers
{
    public class UploadMRPController : Controller
    {
        private readonly SocomecContext _context;
        public UploadMRPController(SocomecContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadCSVMRP(IFormFile file)
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
                    FileMRP MRP = new FileMRP();

                    string[] rows = sreader.ReadLine().Split(';');

                    MRP.Centre = rows[0].ToString();
                    MRP.DteFabrication = DateTime.Parse(rows[1].ToString());
                    MRP.CapaciteRequise = rows[2].ToString();
                    MRP.TypeOrdre = rows[3].ToString();
                    MRP.Ordre = rows[4].ToString();
                    MRP.CodeArticle = rows[5].ToString();
                    MRP.Ordre2 = rows[6].ToString();
                    MRP.Annee = DateTime.Parse(rows[1].ToString()).Year.ToString();
                    MRP.Mois = DateTime.Parse(rows[1].ToString()).Month.ToString();
                    MRP.Fil = RemplirColonneProjet(MRP.CodeArticle);
                    MRP.DateFin = RemplirColonneDateFin(MRP.DteFabrication);
                    _context.FileMRP.Add(MRP);
                }
                _context.SaveChangesAsync();
            }
            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherMRP()
        {
            return View(_context.FileMRP.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE FileMRP'; " +
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
            FileMRPDF fileMRPDFItem = _context.FileMRPDF.FirstOrDefault(df => df.DateDebutPlanif == datefabrication);
            if (fileMRPDFItem != null)
            {
                // Mettre à jour la colonne DateFin dans la table FileMPS
                return fileMRPDFItem.DateFinPlanif;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}
