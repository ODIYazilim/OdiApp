using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeYetkilileriDTOs;

namespace OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs
{
    public class ProjeListItemDTO
    {
        public string ProjeId { get; set; }
        public string ProjeTuru { get; set; }
        public string Platform { get; set; }
        public string YapimSirketi { get; set; }
        public string Yonetmen { get; set; }
        public string CekimTarihi { get; set; }
        public string CekimYeri { get; set; }
        public string SonOdilemeTarihi { get; set; }
        public string ProjeSorumlusu { get; set; }
        public bool Durum { get; set; }
        public bool Premium { get; set; }
        public bool Gizli { get; set; }
        public bool Acil { get; set; }
        public List<ProjeYetkiliOutputDTO> Yetkililer { get; set; }
    }
}
