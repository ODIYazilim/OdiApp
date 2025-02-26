using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.SektorModels;

public class Sektor : BaseModel
{
    public string SektorAdi { get; set; }
    public string SektorKodu { get; set; }
    public int Sira { get; set; }
    public int DilId { get; set; }
}