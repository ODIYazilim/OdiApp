using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.BildirimModels
{
    [Table("OneSignalUsers")]
    public class OneSignalUser : StringBaseModel
    {
        public string KullaniciId { get; set; }
        public string OneSignalExternalId { get; set; }
        public string OneSignalId { get; set; }
        public bool BildirimIzni { get; set; } = true;

        public List<OneSignalUserSubscription> Subscriptions { get; set; }
    }
}