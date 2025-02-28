using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    public class SabitMetin : BaseModel
    {
        public string KayitGrubu { get; set; }
        public int DilId { get; set; }
        public int MetinTipi { get; set; }
        public string Metin { get; set; }
    }
}