namespace OdiApp.DTOs.SharedDTOs.BildirimDTOs.SMSBildirimDTOs
{
    public class SMSResultDTO
    {
        public bool Durum { get; set; }
        public string Hata { get; set; }
        public string HataKodu { get; set; }
        public string DonenKod { get; set; }
    }
}