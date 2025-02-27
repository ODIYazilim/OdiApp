namespace OdiApp.DTOs.IslemlerDTOs.PerformerListeler
{
    public class PerformerListeCreateDTO
    {
        public string ListeAdi { get; set; }
        public string KullaniciId { get; set; }
        public List<PerformerListeDetayCreateDTO>? Performerlar { get; set; }
    }
}
