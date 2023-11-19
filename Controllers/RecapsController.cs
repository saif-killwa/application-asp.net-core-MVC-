using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using SocomecApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OfficeOpenXml.FormulaParsing;
using Microsoft.AspNetCore.Authorization;

namespace SocomecApp.Controllers
{
    [Authorize]
    public class RecapsController : Controller
    {
        private readonly SocomecContext _context;
        public RecapsController(SocomecContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Recaps.ToList());
        }
        [HttpGet("RemplirRecaps")]
        public async Task<IActionResult> RemplirRecaps()
        {
            // Recaps pour le table Ferme 
            
            List<Recaps> recaps = new List<Recaps>();
            // Récupérer les projets distincts de la table FileFerme
            List<string> projets1 = await _context.FileFERME.Select(f => f.Projet).Distinct().ToListAsync();
            List<string> moislist1 = await _context.FileFERME.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee1 = await _context.FileFERME.Select(f => f.Annee).Distinct().ToListAsync();

            // Calculer la somme des temps de fabrication pour chaque projet
            foreach(string annne in  annee1)
            {
                foreach (string mois in moislist1)
                {
                    foreach (string projet in projets1)
                    {

                        var sommeTempsFabrication = await _context.FileFERME.Where(f => f.Annee == annne && f.Mois == mois && f.Projet == projet).ToListAsync();
                        float x = 0;
                        string cc = null;
                        foreach (var item in sommeTempsFabrication)
                        {
                            cc = item.TempsFabrication.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "Ferme";
                        recaps1.Annee =  annne;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }

            }
            
            
            _context.SaveChanges();

            // Recaps pour le table MPS
            
            List<string> projets2 = await _context.FileMPS.Select(f => f.Fil).Distinct().ToListAsync();
            List<string> moislist2 = await _context.FileMPS.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee2 = await _context.FileMPS.Select(f => f.Annee).Distinct().ToListAsync();

            // Calculer la somme des temps de fabrication pour chaque projet            
            foreach (string annee in annee2) 
            {
                foreach (string mois in moislist2)
                {
                    foreach (string projet in projets2)
                    {
                        var sommecapaciteRequise = await _context.FileMPS.Where(f => f.Annee == annee && f.Mois == mois && f.Fil == projet).ToListAsync();
                        float x = 0;
                        string cc = null;

                        foreach (var item in sommecapaciteRequise)
                        {
                            cc = item.CapaciteRequise.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "MPS";
                        recaps1.Annee = annee;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }
            }
            
            _context.SaveChanges();

            // Recaps pour le table MRP

            
            List<string> projets3 = await _context.FileMRP.Select(f => f.Fil).Distinct().ToListAsync();
            List<string> moislist3 = await _context.FileMRP.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee3 = await _context.FileMRP.Select(f => f.Annee).Distinct().ToListAsync();

            // Calculer la somme des temps de fabrication pour chaque projet
            foreach (string annee in annee3) 
            {
                foreach (string mois in moislist3)
                {
                    foreach (string projet in projets3)
                    {
                        var sommecapaciteRequise = await _context.FileMRP.Where(f => f.Annee == annee && f.Mois == mois && f.Fil == projet).ToListAsync();
                        float x = 0;
                        string cc = null;

                        foreach (var item in sommecapaciteRequise)
                        {
                            cc = item.CapaciteRequise.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "MRP";
                        recaps1.Annee = annee;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }

            }
            
            _context.SaveChanges();

            //Recap Rendu 

            List<string> projets7 = await _context.Rendu.Select(f => f.Projet).Distinct().ToListAsync();
            List<string> moislist7 = await _context.Rendu.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee7 = await _context.Rendu.Select(f => f.Annee).Distinct().ToListAsync();

            // Calculer la somme des temps de fabrication pour chaque projet
            foreach (string annee in annee7)
            {
                foreach (string mois in moislist7)
                {
                    foreach (string projet in projets7)
                    {
                        var sommeHeuresMO = await _context.Rendu.Where(f => f.Annee == annee && f.Mois == mois && f.Projet == projet).ToListAsync();
                        float x = 0;
                        string cc = null;

                        foreach (var item in sommeHeuresMO)
                        {
                            cc = item.HeuresMO.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "Rendu";
                        recaps1.Annee = annee;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }

            }

            _context.SaveChanges();



            //Recaps PO
            // Recaps pour le table OV


            List<string> projets4 = await _context.OV.Select(f => f.Projet).Distinct().ToListAsync();
            List<string> moislist4 = await _context.OV.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee4 = await _context.OV.Select(f => f.Annee).Distinct().ToListAsync();

            // Calculer la somme des temps de fabrication pour chaque projet
            foreach (string annee in annee4)
            {
                foreach (string mois in moislist4)
                {
                    foreach (string projet in projets4)
                    {
                        var SommePO = await _context.OV.Where(f => f.Annee == annee && f.Mois == mois && f.Projet == projet).ToListAsync();
                        float x = 0;
                        string cc = null;

                        foreach (var item in SommePO)
                        {
                            cc = item.PO.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "OV";
                        recaps1.Annee = annee;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }
            }
            
            _context.SaveChanges();

            // Recaps pour le table OVFac

            
            List<string> projets5 = await _context.OVFac.Select(f => f.Projet).Distinct().ToListAsync();
            List<string> moislist5 = await _context.OVFac.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee5 = await _context.OVFac.Select(f => f.Annee).Distinct().ToListAsync();
            // Calculer la somme des temps de fabrication pour chaque projet
            foreach (string annee in annee5)
            {
                foreach (string mois in moislist5)
                {
                    foreach (string projet in projets5)
                    {
                        var SommePO = await _context.OVFac.Where(f => f.Annee == annee && f.Mois == mois && f.Projet == projet).ToListAsync();
                        float x = 0;
                        string cc = null;

                        foreach (var item in SommePO)
                        {
                            cc = item.PO.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "OVFac";
                        recaps1.Annee = annee;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }
            }
            
            _context.SaveChanges();

            // Recaps pour le table POV

            
            List<string> projets6 = await _context.POV.Select(f => f.Fil).Distinct().ToListAsync();
            List<string> moislist6 = await _context.POV.Select(f => f.Mois).Distinct().ToListAsync();
            List<string> annee6 = await _context.POV.Select(f => f.Annee).Distinct().ToListAsync();

            // Calculer la somme des temps de fabrication pour chaque projet
            foreach (string annee in annee6)
            {
                foreach (string mois in moislist6)
                {
                    foreach (string projet in projets6)
                    {
                        var SommePO = await _context.POV.Where( f => f.Annee == annee && f.Mois == mois && f.Fil == projet).ToListAsync();
                        float x = 0;
                        string cc = null;

                        foreach (var item in SommePO)
                        {
                            cc = item.PO.Replace(" ", string.Empty);
                            cc = cc.Replace(",", ".");
                            x = x + float.Parse(cc);
                        }

                        // Enregistrer les résultats dans la table "Recaps"
                        Recaps recaps1 = new Recaps();
                        recaps1.NomProject = projet;
                        recaps1.Value = x.ToString();
                        recaps1.Table = "POV";
                        recaps1.Annee = annee;
                        recaps1.Mois = mois;



                        _context.Recaps.AddRange(recaps1);
                    }
                }
            }
            
            _context.SaveChanges();
            _context.Recaps.OrderBy(r => r.Mois).ToList();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        

    }
}
