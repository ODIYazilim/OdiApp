namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class PerformerOdiSesOutputDTO
    {
        public string PerformerOdiSesId { get; set; }
        public string PerformerOdiId { get; set; }
        public string Video { get; set; }
        public string VideoPresignedUrl { get; set; }
        public DateTime Tarih { get; set; }
    }
}
