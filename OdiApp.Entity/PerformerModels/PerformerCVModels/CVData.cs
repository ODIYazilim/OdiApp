using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class CVData : StringBaseModel
{
    public string CVId { get; set; }
    public string AlanKodu { get; set; }
    public string? Deger { get; set; }
    public string? Deger2 { get; set; }
}