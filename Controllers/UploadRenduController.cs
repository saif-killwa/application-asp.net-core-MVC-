using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocomecApp.Models;

namespace SocomecApp.Controllers
{
    public class UploadRenduController : Controller
    {
        private readonly SocomecContext _context;
        public UploadRenduController(SocomecContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadCSVRendu(IFormFile file)
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
                    Rendu R = new Rendu();

                    string[] rows = sreader.ReadLine().Split(';');

                    R.Magasin = rows[0].ToString();
                    R.Ordre = rows[1].ToString();
                    R.DateHeure = DateTime.Parse(rows[2].ToString());
                    R.Centre = rows[3].ToString();
                    R.Operation = rows[4].ToString();
                    R.HeuresMO = rows[5].ToString();
                    R.CodeArticle = rows[6].ToString();
                    R.Projet = RemplirColonneProjet(R.CodeArticle);
                    R.QteOrdre = rows[8].ToString();
                    R.QteLiv = rows[9].ToString();
                    R.Mois = DateTime.Parse(rows[2].ToString()).Month.ToString();
                    R.Annee = DateTime.Parse(rows[2].ToString()).Year.ToString();


                    _context.Rendu.Add(R);
                }
                _context.SaveChangesAsync();
            }
            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherRendu()
        {
            return View(_context.Rendu.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE Rendu'; " +
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
    }
}
