using OdiApp.DTOs.BildirimDTOs.OneSignalDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.OneSignalLogicServices
{
    public interface IOneSignalLogicService
    {
        Task<OdiResponse<OneSignalUserOutputDTO>> YeniOneSignalUser(OneSignalUserCreateDTO oneSignalUser, OdiUser user);
        Task<OneSignalUser> OneSignalUserGetir(string kullaniciId);
        Task<OdiResponse<OneSignalUserOutputDTO>> YeniOneSignalUserSubscribe(OneSignalUserSubscriptionCreateDTO oneSignalUserSubscribe, OdiUser user);
    }
}
