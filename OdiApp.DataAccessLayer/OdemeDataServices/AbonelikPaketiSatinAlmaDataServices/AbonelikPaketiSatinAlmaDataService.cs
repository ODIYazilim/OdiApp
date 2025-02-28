using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikPaketiSatinAlmaDataServices
{
    public class AbonelikPaketiSatinAlmaDataService : IAbonelikPaketiSatinAlmaDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonelikPaketiSatinAlmaDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AbonelikPaketiSatinAlma> YeniAbonelikPaketiSatinAlma(AbonelikPaketiSatinAlma model)
        {
            await _dbContext.AbonelikPaketiSatinAlmalari.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<AbonelikPaketiSatinAlma> AbonelikPaketiSatinAlmaGuncelle(AbonelikPaketiSatinAlma model)
        {
            _dbContext.AbonelikPaketiSatinAlmalari.Update(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<AbonelikPaketiSatinAlma>> AbonelikUrunListesiGetir()
        {
            return await _dbContext.AbonelikPaketiSatinAlmalari.AsNoTracking().ToListAsync();
        }

        public async Task<List<AbonelikPaketiSatinAlma>> AbonelikUrunListesiGetirByKullaniciId(string kullaniciId)
        {
            return await _dbContext.AbonelikPaketiSatinAlmalari.AsNoTracking().Where(x => x.KullaniciId == kullaniciId).ToListAsync();
        }
    }
}