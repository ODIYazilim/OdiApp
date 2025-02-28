using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikPaketiAboneOlmaDataServices
{
    public class AbonelikPaketiAboneOlmaDataService : IAbonelikPaketiAboneOlmaDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonelikPaketiAboneOlmaDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AbonelikPaketiAboneOlma> YeniAbonelikPaketiAboneOlma(AbonelikPaketiAboneOlma model)
        {
            await _dbContext.AbonelikPaketiAboneOlmalari.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<AbonelikPaketiAboneOlma> AbonelikPaketiAboneOlmaGuncelle(AbonelikPaketiAboneOlma model)
        {
            _dbContext.AbonelikPaketiAboneOlmalari.Update(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<AbonelikPaketiAboneOlma>> AbonelikPaketiAboneOlmaListesiGetir()
        {
            return await _dbContext.AbonelikPaketiAboneOlmalari.AsNoTracking().ToListAsync();
        }

        public async Task<List<AbonelikPaketiAboneOlma>> AbonelikPaketiAboneOlmaListesiGetirByKullaniciId(string kullaniciId)
        {
            return await _dbContext.AbonelikPaketiAboneOlmalari.AsNoTracking().Where(x => x.KullaniciId == kullaniciId).ToListAsync();
        }
        public async Task<AbonelikPaketiAboneOlma> AbonelikPaketiAboneOlmaGetirByAboneReferenceCode(string refCode)
        {
            return await _dbContext.AbonelikPaketiAboneOlmalari.AsNoTracking().FirstOrDefaultAsync(x => x.AbonelikReferanceCode == refCode);
        }
    }
}