using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class MenajerPerformerGuncellenenAlani : StringBaseModel
{
    public string? MenajerId { get; set; }
    public string PerformerId { get; set; }
    public string GuncellemeTipi { get; set; }
    public string GuncellenenAlan { get; set; }
    public bool? MenajerGordu { get; set; }
    public DateTime MenajerGorduTarihi { get; set; }
}