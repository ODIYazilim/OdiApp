namespace OdiApp.DTOs.IslemlerDTOs.OdiListeler
{
    public class OdiListeOutputDTO
    {
        public string OdiListeId { get; set; }
        public string KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string ListeAdi { get; set; }
        public bool YetkililerlePaylasilsin { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }

        public List<OdiListeDetayOutputDTO>? Odiler { get; set; }
    }
}