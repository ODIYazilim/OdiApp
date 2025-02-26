using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

public class YetenekTemsilcisiPerformerEtiketTipi : StringBaseModel
{
    public string EtiketTipAdi { get; set; }
    public string EtiketTipKodu { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
    public int DilId { get; set; }
}