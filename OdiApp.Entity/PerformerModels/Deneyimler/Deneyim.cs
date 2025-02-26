using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.Deneyimler;

public class Deneyim : BaseModel
{
    public string DeneyimAdi { get; set; }
    public string DeneyimKodu { get; set; }
    public int DilId { get; set; }
    public bool Aktif { get; set; }
    public int Sira { get; set; }
}