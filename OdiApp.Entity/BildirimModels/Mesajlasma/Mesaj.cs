using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.BildirimModels.Mesajlasma
{
    public class Mesaj : StringBaseModel
    {
        public string Kullanici1Id { get; set; }
        public string Kullanici2Id { get; set; }
        public List<MesajDetay>? MesajDetaylar { get; set; }
    }
}