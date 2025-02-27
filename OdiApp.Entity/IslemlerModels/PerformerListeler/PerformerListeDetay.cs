using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.PerformerListeler
{
    public class PerformerListeDetay : StringBaseModel
    {
        public string PerformerListeId { get; set; }
        public string PerformerId { get; set; }
    }
}