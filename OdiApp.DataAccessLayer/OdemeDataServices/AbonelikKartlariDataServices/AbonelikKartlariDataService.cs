using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikKartlariDataServices
{
    public class AbonelikKartlariDataService : IAbonelikKartlariDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonelikKartlariDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AbonelikKartlari> YeniAbonelikKartlari(AbonelikKartlari model)
        {
            await _dbContext.AbonelikKartlari.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<AbonelikKartlari> AbonelikKartlariGuncelle(AbonelikKartlari model)
        {
            _dbContext.AbonelikKartlari.Update(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<AbonelikKartlari>> AbonelikKartListesiGetir()
        {
            return await _dbContext.AbonelikKartlari.AsNoTracking().ToListAsync();
        }

        public async Task<AbonelikKartlari> AbonelikKartiGetir(string id)
        {
            return await _dbContext.AbonelikKartlari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}