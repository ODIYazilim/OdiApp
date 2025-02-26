using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.OnerilerModels;

public class MenajerPerformerOnerileri : StringBaseModel
{
    public string ProjeId { get; set; }
    public string RolId { get; set; }
    public string MenajerId { get; set; }
    public string PerformerId { get; set; }
    public DateTime OneriTarihi { get; set; }
}