using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerYorumModels;

public class PerformerYorum : StringBaseModel
{
    public string YorumYapanId { get; set; }
    public string PerformerId { get; set; }
    public string YorumMetni { get; set; }
}