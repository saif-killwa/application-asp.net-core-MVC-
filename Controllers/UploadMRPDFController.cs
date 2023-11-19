using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocomecApp.Models;

namespace SocomecApp.Controllers
{
    public class UploadMRPDFController : Controller
    {
        private readonly SocomecContext _context;
        public UploadMRPDFController(SocomecContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadCSVMRPDF(IFormFile file)
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
                    FileMRPDF MRPDF = new FileMRPDF();

                    string[] rows = sreader.ReadLine().Split(';');

                    MRPDF.Ordre = rows[0].ToString();
                    MRPDF.Article = rows[1].ToString();
                    MRPDF.Description = rows[2].ToString();
                    MRPDF.QuantOrdre = rows[3].ToString();
                    MRPDF.DateDebutPlanif = DateTime.Parse(rows[6].ToString());
                    MRPDF.DateFinPlanif = DateTime.Parse(rows[6].ToString());
                    MRPDF.Gamme = rows[8].ToString();
                    MRPDF.Mag = rows[9].ToString();
                    MRPDF.Planif = rows[9].ToString();
                    MRPDF.StatutOrdre = rows[9].ToString();
                    MRPDF.DateTrans = DateTime.Parse(rows[10].ToString());


                    _context.FileMRPDF.Add(MRPDF);
                }
                _context.SaveChangesAsync();
            }
            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherMRPDF()
        {
            return View(_context.FileMRPDF.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE FileMRPDF'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();

            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
        }
    }
}
