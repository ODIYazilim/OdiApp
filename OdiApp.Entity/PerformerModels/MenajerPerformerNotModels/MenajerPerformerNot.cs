using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.MenajerPerformerNotModels;

public class MenajerPerformerNot : StringBaseModel
{
    public string PerformerId { get; set; }
    public string MenajerId { get; set; }
    public string Not { get; set; }
    public DateTime NotKayitTarihi { get; set; }
    public int Derece { get; set; }
}