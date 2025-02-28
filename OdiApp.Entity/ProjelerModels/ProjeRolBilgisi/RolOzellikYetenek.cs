using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;

public class RolOzellikYetenek : StringBaseModel
{
    public string ProjeRolOzellikId { get; set; }
    public string YetenekTipiKodu { get; set; }
    public ProjeRolOzellik ProjeRolOzellik { get; set; }
}