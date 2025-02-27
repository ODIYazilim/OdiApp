using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.OneSignalDataServices
{
    public interface IOneSignalUserDataService
    {
        Task<OneSignalUser> YeniOneSignalUser(OneSignalUser osUser);
        Task<OneSignalUser> OneSignalUserGetir(string userId);
        Task<OneSignalUserSubscription> YeniOneSignalUserSubscription(OneSignalUserSubscription osUserSubscription);


    }
}
