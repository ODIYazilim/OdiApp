namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class PerformerOdiFotografOutputDTO
    {
        public string PerformerOdiFotografId { get; set; }
        public string PerformerOdiId { get; set; }
        public string Fotograf { get; set; }
        public string FotografPresignedURL { get; set; }
        public string FotografAciklamasi { get; set; }
        public DateTime Tarih { get; set; }
    }
}
