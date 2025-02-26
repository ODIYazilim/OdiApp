using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.KisiselOzellikler;

public class KisiselOzellik : BaseModel
{
    public string KisiselOzellikAdi { get; set; }
    public string KisiselOzellikKodu { get; set; }
    public int Sira { get; set; }
    public int DilId { get; set; }
    public bool Active { get; set; }
}