using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi
{
    public class KayitliRolAnketSorusu : StringBaseModel
    {
        public string KayitliRolId { get; set; }
        public string? Soru { get; set; }
        public int Sira { get; set; }
    }
}