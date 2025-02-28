using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo
{
    public class KayitliRolOdiVideoSenaryo : StringBaseModel
    {
        public string KayitliRolOdiId { get; set; }
        public string SenaryoAdi { get; set; }
        public string Senaryo { get; set; } //html formatında
        public bool Dosyami { get; set; }
    }
}