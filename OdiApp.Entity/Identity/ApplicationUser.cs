using Microsoft.AspNetCore.Identity;

namespace OdiApp.EntityLayer.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string AdSoyad { get; set; }
        public int UlkeTelefonKodu { get; set; }
        public string KayitTuruKodu { get; set; }
        public string KayitGrubuKodu { get; set; }
        public string ProfilFotografi { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime PreviusLogin { get; set; }
        public DateTime KayitTarihi { get; set; }
        public bool KVKK { get; set; }
        public bool KullaniciSozlesmesi { get; set; }
        public bool GizlilikSozlesmesi { get; set; }
        public int OdicikMiktari { get; set; }
        public bool CocukMu { get; set; }
        public string? VeliAdSoyad { get; set; }
        public string? VeliTelefon { get; set; }
        public string? Hakkimda { get; set; }
        public bool Premium { get; set; }
        public bool OnayliKullanici { get; set; }
        public bool OnayBekliyor { get; set; }
        public bool Reddedildi { get; set; }
        public string? OnaylayanId { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public bool Yasakli { get; set; }
        public DateTime? SonYasaklanmaTarihi { get; set; }
        public DateTime? SonYasakAcilmaTarihi { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? Sehir { get; set; }
    }
}
