using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.TelefonKoduDataServices
{
    public class TelefonKoduService : ITelefonKoduService
    {
        ApplicationDbContext _dbContext;
        public TelefonKoduService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TelefonKodu>> AktifTelefonKodlariListesi()
        {
            return await _dbContext.TelefonKodlari.Where(x => x.Aktif == true).ToListAsync();
        }
        public async Task<List<TelefonKodu>> TelefonKoduListesi()
        {
            return await _dbContext.TelefonKodlari.AsNoTracking().ToListAsync();
        }

        public async Task<TelefonKodu> TelefonKoduGetir(int id)
        {
            return await _dbContext.TelefonKodlari.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<TelefonKodu> YeniTelefonKodu(TelefonKodu telefonKodu)
        {
            await _dbContext.TelefonKodlari.AddAsync(telefonKodu);
            await _dbContext.SaveChangesAsync();

            return telefonKodu;
        }

        public async Task<TelefonKodu> TelefonKoduGuncelle(TelefonKodu telefonKodu)
        {
            _dbContext.TelefonKodlari.Update(telefonKodu);
            await _dbContext.SaveChangesAsync();

            return telefonKodu;
        }

        public async Task<bool> TelefonKoduSil(TelefonKodu telefonKodu)
        {
            _dbContext.TelefonKodlari.Remove(telefonKodu);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
