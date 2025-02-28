using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.OdicikModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.OdicikIslemleriDataServices
{
    public class OdicikIslemleriDataService : IOdicikIslemleriDataService
    {
        ApplicationDbContext _dbContext;

        public OdicikIslemleriDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OdicikIslemleri> YeniOdicikIslemler(OdicikIslemleri model)
        {
            await _dbContext.OdicikIslemleri.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<OdicikIslemleri>> OdicikHareketleriGetir(string kullaniciId)
        {
            return await _dbContext.OdicikIslemleri.Where(x => x.KullaniciId == kullaniciId).OrderByDescending(x => x.EklenmeTarihi).ToListAsync();
        }

        public Task<int> OdicikBakiyeToplamiGetir(string kullaniciId)
        {
            return _dbContext.OdicikIslemleri.Where(x => x.KullaniciId == kullaniciId).SumAsync(x => x.OdicikMiktari);
        }
    }
}