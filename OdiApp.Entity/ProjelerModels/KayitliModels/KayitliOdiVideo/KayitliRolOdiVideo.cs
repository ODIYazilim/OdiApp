using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo
{
    public class KayitliRolOdiVideo : StringBaseModel
    {
        public string KayitliRolOdiId { get; set; }
        public string Baslik { get; set; }

        public List<KayitliRolOdiVideoDetay> DetayList { get; set; }
    }
}