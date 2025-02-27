namespace OdiApp.DTOs.BildirimDTOs.OdiBildirimDTOS
{
    public class OdiBildirimTumOutputDTO
    {
        public string OdiBildirimId { get; set; }
        public string Baslik { get; set; }
        public string KullaniciAdSoyad { get; set; } //
        public string GonderenKullaniciId { get; set; }//
        public string GonderenKullaniciAdSoyad { get; set; }// 
        public string AltBaslik { get; set; }//
        public string Mesaj { get; set; }
        public string DosyaYolu { get; set; }
        public int BildirimTipi { get; set; }
        public string KullaniciId { get; set; }
        public bool Okundu { get; set; }
        public bool Herkes { get; set; }
        public DateTime BildirimTarihi { get; set; }

    }
}
