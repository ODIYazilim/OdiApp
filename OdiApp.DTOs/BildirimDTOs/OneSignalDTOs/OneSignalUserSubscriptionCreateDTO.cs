namespace OdiApp.DTOs.BildirimDTOs.OneSignalDTOs
{
    public class OneSignalUserSubscriptionCreateDTO
    {
        public string? OneSignalUserId { get; set; }
        public string? KullaniciId { get; set; }
        public string Device { get; set; }
        public int DeviceType { get; set; }
        public string OneSignalSubscribeId { get; set; }
    }
}
