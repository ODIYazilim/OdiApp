using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSes
{
    public class KayitliRolOdiSes : StringBaseModel
    {
        public string KayitliRolOdiId { get; set; }
        public string Replik { get; set; }
        public int Sure { get; set; }
        public int Sira { get; set; }
    }
}