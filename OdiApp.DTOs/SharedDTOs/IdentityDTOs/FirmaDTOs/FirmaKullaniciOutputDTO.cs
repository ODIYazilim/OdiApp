namespace OdiApp.DTOs.SharedDTOs.IdentityDTOs.FirmaDTOs
{
    public class FirmaKullaniciOutputDTO
    {
        public int FirmaId { get; set; }
        public string? FirmaAdi { get; set; }
        public string? FirmaTamAdi { get; set; }
        public string FirmaTipiKodu { get; set; }
        public string FirmaKodu { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdSoyad { get; set; }
        public string? FirmadakiPozisyonu { get; set; }
    }
}
