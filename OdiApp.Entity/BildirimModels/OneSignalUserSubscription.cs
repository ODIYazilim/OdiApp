using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.BildirimModels
{
    [Table("OneSignalUserSubscriptions")]
    public class OneSignalUserSubscription : StringBaseModel
    {
        public string OneSignalUserId { get; set; }
        public string Device { get; set; }
        public int DeviceType { get; set; }//android =1 , ios =2, web=3
        public string OneSignalSubscribeId { get; set; }
    }
}