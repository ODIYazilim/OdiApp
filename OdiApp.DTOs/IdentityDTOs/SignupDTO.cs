using System.ComponentModel.DataAnnotations;

namespace OdiApp.DTOs.IdentityDTOs
{
    public class SignupDTO
    {
        [Required(ErrorMessage = "Kullanıcı Tam Adı alanı boş bırakılamaz")]
        public string TamAdi { get; set; }

        [Required(ErrorMessage = "Kayıt Türü alanı boş bırakılamaz")]
        public string KayitTuruKodu { get; set; }

        [Required(ErrorMessage = "Kayıt Türü alanı boş bırakılamaz")]
        public string KayitGrubuKodu { get; set; }

        [Required(ErrorMessage = "Telefon Numarası alanı boş bırakılamaz")]
        public string TelefonNumarasi { get; set; }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifre alanı boş bırakılamaz")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "KVKK alanı boş bırakılamaz")]
        public bool KVKK { get; set; }

        [Required(ErrorMessage = "KullaniciSozlesmesi alanı boş bırakılamaz")]
        public bool KullaniciSozlesmesi { get; set; }

        [Required(ErrorMessage = "GizlilikSozlesmesi alanı boş bırakılamaz")]
        public bool GizlilikSozlesmesi { get; set; }

        [Required(ErrorMessage = "UlkeTelefonKodu alanı boş bırakılamaz")]
        public int UlkeTelefonKodu { get; set; }
        public string? YetenekTemsilcisiId { get; set; }
        public bool CocukMu { get; set; }
        public string? VeliAdSoyad { get; set; }
        public string? VeliTelefon { get; set; }
        public string? FirmaKodu { get; set; }
        public string? OnerilenFirmaAdi { get; set; }
    }
}
