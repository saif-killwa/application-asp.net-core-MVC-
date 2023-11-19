using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocomecApp.Models;

namespace SocomecApp.Controllers
{
    public class UploadMPSDFController : Controller
    {
        private readonly SocomecContext _context;
        public UploadMPSDFController(SocomecContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadCSVMPSDF(IFormFile file)
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

                // Read the lines until you find the line with data
                while (!sreader.EndOfStream)
                {
                    
                        FileMPSDF MPSDF = new FileMPSDF();

                        string[] rows = sreader.ReadLine().Split(';');

                        MPSDF.Ordre = rows[0].ToString();
                        MPSDF.ArticleMPS = rows[1].ToString();
                        MPSDF.Description = rows[2].ToString();
                        MPSDF.Gamme = rows[3].ToString();
                        MPSDF.QuantiteOrdre = rows[4].ToString();
                        MPSDF.MethodePlanif = rows[5].ToString();
                        MPSDF.DteDebPlanif = DateTime.Parse(rows[6].ToString());
                        MPSDF.DteFinPlanif = DateTime.Parse(rows[7].ToString());
                        MPSDF.Mag = rows[8].ToString();
                        MPSDF.Statut = rows[9].ToString();
                        MPSDF.DateTrans = DateTime.Parse(rows[10].ToString());


                        _context.FileMPSDF.Add(MPSDF);
                    
                }
                _context.SaveChangesAsync();

            }
            return Redirect("~/Upload/Index");
        }
        //affichage
        public IActionResult AfficherMPSDF()
        {
            return View(_context.FileMPSDF.ToList());
        }
        public void TruncateDatabase()
        {
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; " +
                                  "EXEC sp_MSforeachtable 'TRUNCATE TABLE FileMPSDF'; " +
                                  "EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all';";

            command.ExecuteNonQuery();

            connection.Close();

            // Réinitialiser l'ID de la table à 1
            //ResetTableId();
        }

    }
}
