using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikUrunuDataServices
{
    public class AbonelikUrunuDataService : IAbonelikUrunuDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonelikUrunuDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AbonelikUrunu> YeniAbonelikUrunu(AbonelikUrunu model)
        {
            await _dbContext.AbonelikUrunleri.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<AbonelikUrunu> AbonelikUrunuGuncelle(AbonelikUrunu model)
        {
            _dbContext.AbonelikUrunleri.Update(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<AbonelikUrunu>> AbonelikUrunListesiGetir()
        {
            return await _dbContext.AbonelikUrunleri.AsNoTracking().ToListAsync();
        }

        public async Task<AbonelikUrunu> AbonelikUrunuGetirByReferenceCode(string referenceCode)
        {
            return await _dbContext.AbonelikUrunleri.AsNoTracking().FirstOrDefaultAsync(x => x.ReferansCode == referenceCode);
        }
    }
}