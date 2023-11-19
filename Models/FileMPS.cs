using System.Data;

namespace SocomecApp.Models
{
    public class FileMPS
    {
        public int Id { get; set; }
        public string? Centre { get; set; }
        public DateTime DateFabrication { get; set; }
        public string? CapaciteRequise { get; set;}
        public string? OrdreType { get; set; }
        public string? Ordre { get; set; }
        public string? ArticleMPS { get; set; }
        public string? Ordre2 { get; set; }
        public string? QteOrdre { get; set; }
        public string? Mois { get; set; }
        public string? Annee { get; set; }
        public string? Fil { get; set; }
        public DateTime? DateFin { get; set; }


    }
}
