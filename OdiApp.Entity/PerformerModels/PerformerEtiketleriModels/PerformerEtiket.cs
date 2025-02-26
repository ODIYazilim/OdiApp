using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

public class PerformerEtiket : StringBaseModel
{
    public string EtiketTipKodu { get; set; }
    public string EtiketAdi { get; set; }
    public string EtiketKodu { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
    public int DilId { get; set; }
}