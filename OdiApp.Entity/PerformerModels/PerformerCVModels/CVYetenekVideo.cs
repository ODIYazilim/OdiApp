using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class CVYetenekVideo : StringBaseModel
{
    public string CVYetenekId { get; set; }
    public string Video { get; set; }
    public string Tags { get; set; }
}