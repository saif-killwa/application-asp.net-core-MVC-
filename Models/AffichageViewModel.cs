namespace SocomecApp.Models
{
    public class AffichageViewModel
    {
        public List<FileFERME> FileFERME { get; set; }
        public List<FileMPS> FileMPS { get; set; }
        public List<FileFil> FileFil { get; set; }
        public List<FileMPSDF> FileMPSDF { get; set; }
        public List<FileMRP> FileMRP { get; set; }
        public List<FileMRPDF> FileMRPDF { get; set; }
        public List<OV> OV { get; set; }
        public List<OVFac> OVFac { get; set; }
        public List<POV> POV { get; set; }
        public List<PRS> PRS { get; set; }
        public List<ZRP> ZRP { get; set; }
        public List<Rendu> Rendu { get; set; }
    }
}
