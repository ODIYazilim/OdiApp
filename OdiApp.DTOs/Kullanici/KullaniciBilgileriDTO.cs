namespace OdiApp.DTOs.Kullanici
{
    public class KullaniciBilgileriDTO
    {
        public string Id { get; set; }
        public string AdSoyad { get; set; }
        public string? ProfilFotografiDosyaYolu { get; set; }
        public string? ProfilFotografi { get; set; }
        public string TelefonNumarasi { get; set; }
        public int UlkeTelefonKodu { get; set; }
        public string? Email { get; set; }
        public DateTime SonGirisTarihi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public List<string> KayitGrubuKoduListesi { get; set; }
        public List<string> KayitTuruKoduListesi { get; set; }
        public string MenajerId { get; set; }
        public string MenajerAdSoyad { get; set; }
        public bool CocukMu { get; set; }
        public string? VeliAdSoyad { get; set; }
        public string? VeliTelefon { get; set; }
        public bool Premium { get; set; }
        public bool OnayliKullanici { get; set; }
        public bool OnayBekliyor { get; set; }
        public bool Reddedildi { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Sehir { get; set; }
        public string FirmaKodu { get; set; }
        public string FirmaAdi { get; set; }
        //public PerformerPuanOutputDTO PerformerPuanBilgileri { get; set; }
    }
}
