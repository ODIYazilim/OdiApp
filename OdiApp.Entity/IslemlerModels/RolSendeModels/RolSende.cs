using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.RolSendeModels
{
    public class RolSende : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string RolId { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }
        public string OnaylayanId { get; set; }
        public DateTime RolSendeTarihi { get; set; }
    }
}