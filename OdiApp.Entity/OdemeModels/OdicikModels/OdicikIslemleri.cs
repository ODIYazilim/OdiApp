using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.OdicikModels
{
    public class OdicikIslemleri : StringBaseModel
    {
        public string KullaniciId { get; set; }
        public int OdicikMiktari { get; set; }
        public string IslemTipi { get; set; } //örn; giden, gelen
        public string IslemKodu { get; set; } //örn; proje, callback
        public string Aciklama { get; set; }
    }
}