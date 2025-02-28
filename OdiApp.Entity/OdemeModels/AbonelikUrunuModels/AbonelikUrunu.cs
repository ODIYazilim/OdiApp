using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels
{
    public class AbonelikUrunu : StringBaseModel
    {
        public string KayitGrubu { get; set; }
        public string KayitTuru { get; set; }
        public string OdemeYonetimiUrunAdi { get; set; }
        public string? Status { get; set; }
        public string? ReferansCode { get; set; }
        public bool Aktif { get; set; } = false;
    }
}