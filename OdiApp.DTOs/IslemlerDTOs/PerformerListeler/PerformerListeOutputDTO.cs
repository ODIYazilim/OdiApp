namespace OdiApp.DTOs.IslemlerDTOs.PerformerListeler
{
    public class PerformerListeOutputDTO
    {
        public string PerformerListeId { get; set; }
        public string ListeAdi { get; set; }
        public string KullaniciId { get; set; }
        public List<PerformerListeDetayOutputDTO> Performerlar { get; set; }
    }
}