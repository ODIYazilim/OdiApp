namespace OdiApp.DTOs.BildirimDTOs.OneSignalDTOs
{
    public class OneSignalUserCreateDTO
    {
        public string KullaniciId { get; set; }
        public string OneSignalExternalId { get; set; }
        public string OneSignalId { get; set; }
        public bool BildirimIzni { get; set; } = true;

        public List<OneSignalUserSubscriptionCreateDTO> Subscriptions { get; set; }
    }
}
