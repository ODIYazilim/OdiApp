using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;

public class PerformerAbonelikSureUzatma : StringBaseModel
{
    public string PerformerAbonelikId { get; set; }
    public DateTime BitisTarihi { get; set; }
    public DateTime UzatilmisBitisTarihi { get; set; }
    public string UzatmaSebebi { get; set; }
}