using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikYukseltmeTalepDataServices
{
    public class AbonelikYukseltmeTalepDataService : IAbonelikYukseltmeTalepDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonelikYukseltmeTalepDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AbonelikYukseltmeTalep> YeniAbonelikYukseltmeTalep(AbonelikYukseltmeTalep model)
        {
            await _dbContext.AbonelikYukseltmeTalepleri.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGuncelle(AbonelikYukseltmeTalep model)
        {
            _dbContext.AbonelikYukseltmeTalepleri.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGetirById(string id)
        {
            return await _dbContext.AbonelikYukseltmeTalepleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGetirByKullaniciId(string kullaniciId)
        {
            return await _dbContext.AbonelikYukseltmeTalepleri.AsNoTracking().FirstOrDefaultAsync(x => x.KullaniciId == kullaniciId && x.Aktif);
        }

        public async Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGetirByKullaniciveAbonelikId(string kullaniciId, string abonelikId)
        {
            return await _dbContext.AbonelikYukseltmeTalepleri.AsNoTracking().FirstOrDefaultAsync(x => x.KullaniciId == kullaniciId && x.MevcutKullaniciAbonelikId == abonelikId && x.Aktif);
        }
    }
}