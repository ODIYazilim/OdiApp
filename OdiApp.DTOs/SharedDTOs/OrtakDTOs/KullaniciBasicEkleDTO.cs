namespace OdiApp.DTOs.SharedDTOs.OrtakDTOs
{
    public class KullaniciBasicEkleDTO
    {
        public string KullaniciId { get; set; }
        public string KullaniciAdSoyad { get; set; }
        public string KullaniciProfilResmi { get; set; }
        public string KayitGrubuKodu { get; set; }
        public string KayitTuruKodu { get; set; }
        public string KullaniciEmail { get; set; }
        public string KullaniciTelefon { get; set; }
        public int UlkeTelefonKodu { get; set; }
        public bool CocukMu { get; set; }
        public string? VeliAdSoyad { get; set; }
        public string? VeliTelefon { get; set; }
    }
}