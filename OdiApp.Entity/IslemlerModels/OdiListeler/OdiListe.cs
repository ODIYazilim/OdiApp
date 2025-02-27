using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.OdiListeler
{
    public class OdiListe : StringBaseModel
    {
        public string KullaniciId { get; set; }
        public string ListeAdi { get; set; }
        public bool YetkililerlePaylasilsin { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }

        public List<OdiListeDetay>? Odiler { get; set; }
        public bool Begendiklerim { get; set; }
        public bool Belki { get; set; }
    }
}