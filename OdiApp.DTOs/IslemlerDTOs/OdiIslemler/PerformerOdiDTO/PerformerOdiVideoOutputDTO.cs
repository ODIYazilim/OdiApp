namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO
{
    public class PerformerOdiVideoOutputDTO
    {
        public string PerformerOdiVideoId { get; set; }
        public string PerformerOdiId { get; set; }
        public string Video { get; set; }
        public string VideoPresignedUrl { get; set; }
        public DateTime Tarih { get; set; }
    }
}
