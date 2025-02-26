using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerTakvimModels;

public class PerformerTakvim : StringBaseModel
{
    public string PerformerId { get; set; }
    public DateTime BaslangicTarihi { get; set; }
    public DateTime BitisTarihi { get; set; }
    public string DolulukAciklamasi { get; set; }
    public int DolulukTuru { get; set; }
}