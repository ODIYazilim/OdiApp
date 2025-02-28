using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    public class RolOzellikDeneyim : StringBaseModel
    {
        public string ProjeRolOzellikId { get; set; }
        public string DeneyimKodu { get; set; }
        public ProjeRolOzellik ProjeRolOzellik { get; set; }
    }
}