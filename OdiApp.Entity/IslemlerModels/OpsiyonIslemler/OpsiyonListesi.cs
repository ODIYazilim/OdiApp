using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler
{
    public class OpsiyonListesi : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }
        public string ListeyeEkleyenKullaniciId { get; set; }
        public DateTime ListeyeEklenemeTarihi { get; set; }
        public Opsiyon? Opsiyon { get; set; }
    }
}