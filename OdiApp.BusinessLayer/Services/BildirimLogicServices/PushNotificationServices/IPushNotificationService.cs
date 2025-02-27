using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.PushNotificationServices
{
    public interface IPushNotificationService
    {
        Task<bool> SendPushAll(OdiBildirim bildirim);
        Task<bool> SendPushClient(OdiBildirim bildirim, List<string> externalIds);
    }
}