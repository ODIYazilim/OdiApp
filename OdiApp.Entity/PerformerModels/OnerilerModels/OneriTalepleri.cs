using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.OnerilerModels;

public class OneriTalepleri : StringBaseModel
{
    public string ProjeId { get; set; }
    public string MenajerId { get; set; }
    public string TalepGonderenId { get; set; }
    public DateTime TalepTarihi { get; set; }
}