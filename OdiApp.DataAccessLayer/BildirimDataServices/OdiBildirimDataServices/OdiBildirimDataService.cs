using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.OdiBildirimDataServices
{
    public class OdiBildirimDataService : IOdiBildirimDataService
    {
        ApplicationDbContext _dbContext;
        public OdiBildirimDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<OdiBildirimHerkes>> OdiBildirimHerkesListesi(DateTime bildirimTarihi)
        {
            return await _dbContext.OdiBildirimHerkes.AsNoTracking().Where(x => x.EklenmeTarihi > bildirimTarihi).ToListAsync();
        }
        public async Task<OdiBildirimHerkes> OdiBildirimHerkesGetir(string odiBildirimHerkesId)
        {
            return await _dbContext.OdiBildirimHerkes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == odiBildirimHerkesId);
        }

        public async Task<List<OdiBildirim>> OdiBildirimListesi(string kullaniciId, DateTime bildirimTarihi)
        {
            return await _dbContext.OdiBildirim.AsNoTracking().Where(x => x.KullaniciId == kullaniciId && x.EklenmeTarihi > bildirimTarihi).ToListAsync();
        }

        public async Task<OdiBildirim> OdiBildirimGetir(string odiBildirmId)
        {
            return await _dbContext.OdiBildirim.AsNoTracking().FirstOrDefaultAsync(x => x.Id == odiBildirmId);
        }

        public async Task<OdiBildirim> YeniOdiBildirim(OdiBildirim bildirim)
        {
            await _dbContext.AddAsync(bildirim);
            await _dbContext.SaveChangesAsync();
            return bildirim;
        }
        public async Task<OdiBildirim> OdiBildirimGuncelle(OdiBildirim bildirim)
        {
            _dbContext.OdiBildirim.Update(bildirim);
            await _dbContext.SaveChangesAsync();
            return bildirim;
        }
        public async Task<OdiBildirimHerkes> YeniOdiBildirimHerkes(OdiBildirimHerkes bildirim)
        {
            await _dbContext.AddAsync(bildirim);
            await _dbContext.SaveChangesAsync();
            return bildirim;
        }

        public async Task<bool> OdiBildirimHepsiOkundu(string kullaniciId)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync($"Update OdiBildirim set Okundu=1 where KullaniciId=" + kullaniciId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> OdiBildirimSil(OdiBildirim bildirim)
        {
            _dbContext.OdiBildirim.Remove(bildirim);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}