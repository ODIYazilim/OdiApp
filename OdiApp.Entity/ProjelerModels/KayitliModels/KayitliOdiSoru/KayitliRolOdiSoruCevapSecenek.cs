using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSoru
{
    public class KayitliRolOdiSoruCevapSecenek : StringBaseModel
    {
        public string KayitliRolOdiSoruId { get; set; }
        public string Cevap { get; set; }
        public bool DigerMi { get; set; }
        public string DigerAciklama { get; set; }
        public int Sira { get; set; }
    }
}