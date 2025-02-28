using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    public class Banka : BaseModel
    {
        public string BankaAdi { get; set; }
        public string Logo { get; set; }
        public int Sira { get; set; }
        public bool Aktif { get; set; }
    }
}