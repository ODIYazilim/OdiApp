namespace OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolOzellikDTOs
{
    public class ProjeRolOzellikCreateDTO
    {
        public string ProjeRolId { get; set; }
        public string? Sehirler { get; set; }
        public string? Uyruk { get; set; }
        public int? MaxBoy { get; set; }
        public int? MinBoy { get; set; }
        public int? MaxKilo { get; set; }
        public int? MinKilo { get; set; }

        public List<RolOzellikFizikselDTO> FizikselOzellikler { get; set; }
        public List<RolOzellikDeneyimDTO> DeneyimKodlari { get; set; }
        public List<RolOzellikEgitimDTO> EgitimTipleri { get; set; }
        public List<RolOzellikYetenekDTO> YetenekTipleri { get; set; }
        public List<RolOzellikPerformerEtiketDTO> PerformerEtiketleri { get; set; }
    }
}