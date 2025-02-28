using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    public class RolOzellikFiziksel : StringBaseModel
    {
        public string ProjeRolOzellikId { get; set; }
        public string FizikselOzellikTipiKodu { get; set; }
        public string FizikselOzellikAdiKodu { get; set; }
        public ProjeRolOzellik ProjeRolOzellik { get; set; }
    }
}