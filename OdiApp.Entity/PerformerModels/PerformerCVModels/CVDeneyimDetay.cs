using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class CVDeneyimDetay : StringBaseModel
{
    public string CVDeneyimId { get; set; }
    public string FormAlaniKodu { get; set; }
    public string Deger { get; set; }
}