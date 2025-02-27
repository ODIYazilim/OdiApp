namespace OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma
{
    public class ProjeMesajDetayOutputDTO
    {
        public string ProjeMesajDetayId { get; set; }
        public string ProjeMesajId { get; set; }
        public string GonderenKullaniciId { get; set; }
        public string GonderenKullaniciAdSoyad { get; set; }
        public string GonderenKullaniciProfilResmi { get; set; } //presigned url olacak
        public string GonderenKullaniciProfilResmiDosyaYolu { get; set; }
        public string GonderilenKullaniciId { get; set; }
        public string GonderilenKullaniciAdSoyad { get; set; }
        public string GonderilenKullaniciProfilResmi { get; set; } //presigned url olacak
        public string GonderilenKullaniciProfilResmiDosyaYolu { get; set; }
        public DateTime MesajGonderimTarihi { get; set; }
        public string TextMesaj { get; set; } = "";
        public string DosyaMesajDosyaYolu { get; set; } = "";
        public string DosyaMesajPresignedUrl { get; set; } = ""; //presigned url olacak
        public string DosyaTipi { get; set; } = "";
        public bool MesajDosyami { get; set; } = false;
        public bool Okundu { get; set; } = false;
    }
}