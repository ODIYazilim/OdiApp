namespace OdiApp.DTOs.IslemlerDTOs.PerformerListeler
{
    public class PerformerListeWithDetayOutputDTO
    {
        public string ListeId { get; set; }
        public string ListeAdi { get; set; }
        public List<PerformerListeDetayDisplayInfoDTO> Performerlar { get; set; }
    }
}