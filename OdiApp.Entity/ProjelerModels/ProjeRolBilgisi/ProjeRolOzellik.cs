using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    public class ProjeRolOzellik : StringBaseModel
    {
        public string ProjeRolId { get; set; }
        public string? Sehirler { get; set; }
        public string? Uyruk { get; set; }
        public int? MaxBoy { get; set; }
        public int? MinBoy { get; set; }
        public int? MaxKilo { get; set; }
        public int? MinKilo { get; set; }

        public List<RolOzellikFiziksel> FizikselOzellikler { get; set; }
        public List<RolOzellikDeneyim> DeneyimKodlari { get; set; }
        public List<RolOzellikEgitim> EgitimTipleri { get; set; }
        public List<RolOzellikYetenek> YetenekTipleri { get; set; }
        public List<RolOzellikPerformerEtiket> PerformerEtiketleri { get; set; }
    }
}