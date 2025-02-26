using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

public class YetenekTemsilcisiPerformerEtiketi : StringBaseModel
{
    public string PerformerId { get; set; }
    public string YetenekTemsilcisiId { get; set; }
    public string EtiketTipKodu { get; set; }
    public string EtiketKodu { get; set; }
}