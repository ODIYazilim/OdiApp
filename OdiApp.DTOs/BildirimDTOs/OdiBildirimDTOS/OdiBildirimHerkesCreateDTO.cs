namespace OdiApp.DTOs.BildirimDTOs.OdiBildirimDTOS
{
    public class OdiBildirimHerkesCreateDTO
    {
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string DosyaYolu { get; set; }
        public int BildirimTipi { get; set; }
        public DateTime BildirimTarihi { get; set; }
    }
}
