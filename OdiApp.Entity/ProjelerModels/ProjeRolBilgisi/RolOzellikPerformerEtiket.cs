using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;

public class RolOzellikPerformerEtiket : StringBaseModel
{
    public string ProjeRolOzellikId { get; set; }
    public string EtiketTipKodu { get; set; }
    public ProjeRolOzellik ProjeRolOzellik { get; set; }
}