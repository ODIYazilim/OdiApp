using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    public class PerformerOdiTekrarCekOneri : StringBaseModel
    {
        public string PerformerOdiId { get; set; }
        public string MenajerOnerisi { get; set; }
        public DateTime OneriTarihi { get; set; }
    }
}