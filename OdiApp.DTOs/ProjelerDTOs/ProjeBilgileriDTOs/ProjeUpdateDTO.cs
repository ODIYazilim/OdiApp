using OdiApp.DTOs.ProjelerDTOs.ProjeYetkilileri;

namespace OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs
{
    public class ProjeUpdateDTO
    {
        public string ProjeId { get; set; }
        public string Adi { get; set; }
        public string ProjeTurKodu { get; set; }
        public string? ProjeAciklama { get; set; }
        public DateTime? CekimBaslangicTarihi { get; set; }
        public DateTime? CekimBitisTarihi { get; set; }
        public int? Butce { get; set; }
        public DateTime? PrePpmTarihi { get; set; }
        public DateTime? PpmTarihi { get; set; }
        public string? Sehirler { get; set; }
        public string? YasakliKelimeler { get; set; }

        public string? YapimciFirmaKodu { get; set; }
        public string? YapimciFirmaAdi { get; set; }

        public List<ProjeYetkiliUpdateDTO>? Yetkililer { get; set; }
        public List<ProjeYetkiliCreateDTO>? YeniYetkililer { get; set; }
    }
}