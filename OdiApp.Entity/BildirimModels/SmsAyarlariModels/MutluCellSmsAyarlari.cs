using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels
{
    public class MutluCellSmsAyarlari : StringBaseModel
    {
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string OrganizasyonAdi { get; set; }
    }
}