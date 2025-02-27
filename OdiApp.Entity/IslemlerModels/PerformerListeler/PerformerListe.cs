using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.PerformerListeler
{
    public class PerformerListe : StringBaseModel
    {
        public string ListeAdi { get; set; }
        public string KullaniciId { get; set; }
        public List<PerformerListeDetay> Performerlar { get; set; }
    }
}