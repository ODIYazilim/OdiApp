using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

public class PerformerEngellenen : StringBaseModel
{
    public string EngellenenPerformerId { get; set; }
    public string EngelleyenId { get; set; }
    public DateTime EngellenmeTarihi { get; set; }
}