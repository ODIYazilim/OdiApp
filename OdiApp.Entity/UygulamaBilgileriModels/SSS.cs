using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    public class SSS : BaseModel
    {
        public int DilId { get; set; }
        public string KayitGrubu { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public int Sira { get; set; }
        public bool Aktif { get; set; }
    }
}