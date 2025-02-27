namespace OdiApp.DTOs.IslemlerDTOs.OdiListeler
{
    public class OdiListeCreateDTO
    {
        public string KullaniciId { get; set; }
        public string ListeAdi { get; set; }

        public bool YetkililerlePaylasilsin { get; set; }
        public DateTime OluşturulmaTarihi { get; set; }
        public List<OdiListeDetayCreateDTO>? Odiler { get; set; }
    }
}
