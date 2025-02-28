using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSoru
{
    public class KayitliRolOdiSoru : StringBaseModel
    {
        public string KayitliRolOdiId { get; set; }
        public string Soru { get; set; }
        public bool CokluSecimSorusu { get; set; }
        public bool CokluCevapIzni { get; set; }
        public List<KayitliRolOdiSoruCevapSecenek> CevapSecenekleri { get; set; }
    }
}