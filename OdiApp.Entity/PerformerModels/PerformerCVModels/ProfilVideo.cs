using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class ProfilVideo : StringBaseModel
{
    public string PerformerId { get; set; }
    public string VideoTipiKodu { get; set; }
    public string? Dil { get; set; }
    public string VideoURL { get; set; }
    public string? VideoTags { get; set; }
}