namespace SocomecApp.Models
{
    public class FileMPSDF
    {
        public int Id { get; set; }
        public string? Ordre { get; set; }
        public string? ArticleMPS { get; set; }
        public string? Description { get; set; }
        public string? Gamme { get; set; }
        public string? QuantiteOrdre { get; set; }
        public string? MethodePlanif {get; set; }
        public DateTime DteDebPlanif { get; set; }
        public DateTime DteFinPlanif { get; set; }
        public string? Mag { set; get; }
        public string? Statut { get; set; }
        public DateTime DateTrans { get; set;}

    }
}
