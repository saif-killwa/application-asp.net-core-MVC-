namespace SocomecApp.Models
{
    public class FileMRPDF
    {
        public int Id { get; set; }
        public string? Ordre { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public string? QuantOrdre { get; set; }
        public DateTime DateDebutPlanif { get; set; }
        public DateTime DateFinPlanif { get; set; }
        public string? Gamme { get; set; }
        public string? Mag { get; set; }
        public string? Planif { get; set; }
        public string? StatutOrdre { get; set; }
        public DateTime DateTrans { get; set; }

     
            
            
    }
}
