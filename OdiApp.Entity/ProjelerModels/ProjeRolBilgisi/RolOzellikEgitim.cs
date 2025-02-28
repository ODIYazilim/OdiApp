using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;

public class RolOzellikEgitim : StringBaseModel
{
    public string ProjeRolOzellikId { get; set; }
    public int EgitimTipiId { get; set; }
    public ProjeRolOzellik ProjeRolOzellik { get; set; }
}