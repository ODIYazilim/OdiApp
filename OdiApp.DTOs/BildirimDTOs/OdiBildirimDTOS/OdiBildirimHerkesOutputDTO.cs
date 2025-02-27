namespace OdiApp.DTOs.BildirimDTOs.OdiBildirimDTOS
{
    public class OdiBildirimHerkesOutputDTO
    {
        public string OdiBildirimHerkesId { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string DosyaYolu { get; set; }
        public int BildirimTipi { get; set; }
        public DateTime BildirimTarihi { get; set; }
    }
}
