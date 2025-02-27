using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.KullaniciBasicDataServices
{
    public class KullaniciBasicDataService : IKullaniciBasicDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public KullaniciBasicDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<KullaniciBasic> KullaniciEkle(KullaniciBasic kullaniciBasic)
        {
            await _dbContext.KullaniciBasic.AddAsync(kullaniciBasic);
            await _dbContext.SaveChangesAsync();

            return kullaniciBasic;
        }

        public async Task<KullaniciBasic> KullaniciGetir(string kullaniciId)
        {
            return await _dbContext.KullaniciBasic.AsNoTracking().FirstOrDefaultAsync(f => f.KullaniciId == kullaniciId);
        }

        public async Task<KullaniciBasic> KullaniciGuncelle(KullaniciBasic kullaniciBasic)
        {
            _dbContext.KullaniciBasic.Update(kullaniciBasic);
            await _dbContext.SaveChangesAsync();

            return kullaniciBasic;
        }
    }
}