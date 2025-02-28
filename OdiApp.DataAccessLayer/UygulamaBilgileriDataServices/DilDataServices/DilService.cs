using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.DilDataServices
{
    public class DilService : IDilService
    {
        ApplicationDbContext _dbContext;
        public DilService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Dil>> AktifDilListesi()
        {
            return await _dbContext.Diller.Where(x => x.Aktif).ToListAsync();
        }
        public async Task<List<Dil>> DilListesi()
        {
            return await _dbContext.Diller.ToListAsync();
        }
        public async Task<Dil> DilGetir(int id)
        {
            return await _dbContext.Diller.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<Dil> YeniDil(Dil dil)
        {
            await _dbContext.Diller.AddAsync(dil);
            await _dbContext.SaveChangesAsync();

            return dil;
        }

        public async Task<Dil> DilGuncelle(Dil dil)
        {
            _dbContext.Diller.Update(dil);
            await _dbContext.SaveChangesAsync();

            return dil;
        }

        public async Task<bool> DilSil(Dil dil)
        {
            _dbContext.Diller.Remove(dil);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
