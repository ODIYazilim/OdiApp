using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    public class ProjeRolPerformer : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string RolId { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }
    }
}