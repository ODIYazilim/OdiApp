using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;

public class SeslendirmeDili : BaseModel
{
    public int DilId { get; set; }
    public string SeslendirmeDiliKodu { get; set; }
    public string SeslendirmeDiliAdi { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
}