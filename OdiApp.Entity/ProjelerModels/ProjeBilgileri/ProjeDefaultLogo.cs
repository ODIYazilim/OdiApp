using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri
{
    public class ProjeDefaultLogo : StringBaseModel
    {
        public string ProjeTuruKodu { get; set; }
        public string Logo { get; set; }
    }
}