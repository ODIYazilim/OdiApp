namespace OdiApp.DTOs.BildirimDTOs.OneSignalDTOs
{
    public class OneSignalUserOutputDTO
    {
        public string OneSignalUserId { get; set; }
        public string KullaniciId { get; set; }
        public string OneSignalExternalId { get; set; }
        public string OneSignalId { get; set; }
        public bool BildirimIzni { get; set; } = true;

        public List<OneSignalUserSubscriptionCreateDTO> Subscriptions { get; set; }
    }
}
