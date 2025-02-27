using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.OdiListeler
{
    public class OdiListeDetay : StringBaseModel
    {
        public string OdiListeId { get; set; }
        public string ProjeId { get; set; }
        public string OdiTalepId { get; set; }
        public DateTime ListeyeEklenmeTarihi { get; set; }
    }
}