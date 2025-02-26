using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class CV : StringBaseModel
{
    public string PerformerId { get; set; }
    public bool MenajerGordu { get; set; }
    public DateTime? MenajerGorduTarih { get; set; }
    public string? MenajerId { get; set; }
    public List<CVData>? DataList { get; set; }
}