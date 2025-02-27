using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.OneSignalDataServices
{
    public class OneSignalUserDataService : IOneSignalUserDataService
    {
        ApplicationDbContext _dbContext;
        public OneSignalUserDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OneSignalUser> OneSignalUserGetir(string UserId)
        {
            return await _dbContext.OneSignalUsers.Include(x => x.Subscriptions).Where(x => x.KullaniciId == UserId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<OneSignalUser> YeniOneSignalUser(OneSignalUser osUser)
        {
            await _dbContext.AddAsync(osUser);
            await _dbContext.SaveChangesAsync();
            return osUser;
        }

        public async Task<OneSignalUserSubscription> YeniOneSignalUserSubscription(OneSignalUserSubscription osUserSubscription)
        {
            await _dbContext.AddAsync(osUserSubscription);
            await _dbContext.SaveChangesAsync();
            return osUserSubscription;
        }
    }
}
