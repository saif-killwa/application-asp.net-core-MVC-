namespace SocomecApp.Models
{
    public class FileFERME
    {   
        public int Id { get; set; }
        public string? Ordre { get; set; }
        public string? Article { get; set; }
        public string? Projet { get; set; }
        public string? Magasin { get; set; }
        public string? QteOrdre { get; set; }
        public string? QteLiv { get; set; }
        public string? Centre { get; set; }
        public string?  TempsFabrication { get; set; }
        public DateTime DateDebutRest { get; set; }
        public string? Mois { get; set; }
        public string? Annee { get; set;}

    }
}
