using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo
{
    public class KayitliRolOdiVideoDetay : StringBaseModel
    {
        public string KayitliRolOdiVideoId { get; set; }
        public string SesDosyasi { get; set; }
        public string Replik { get; set; }
        public int Sure { get; set; }
        public int Sira { get; set; }
        public bool Dosyami { get; set; }
    }
}