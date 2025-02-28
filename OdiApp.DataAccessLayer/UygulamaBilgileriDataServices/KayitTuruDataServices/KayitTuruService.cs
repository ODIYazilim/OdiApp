using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.KayitTuruDataServices
{
    public class KayitTuruService : IKayitTuruService
    {
        ApplicationDbContext _dbContext;
        public KayitTuruService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Kayıt Grubu


        public async Task<List<KayitGrubu>> AktifKayitGrubuListesi(int dilId)
        {
            return await _dbContext.KayitGruplari.Where(x => x.DilId == dilId && x.Aktif).AsNoTracking().ToListAsync();
        }
        public async Task<List<KayitGrubu>> KayitGrubuListesi(int dilId)
        {
            return await _dbContext.KayitGruplari.Where(x => x.DilId == dilId).AsNoTracking().ToListAsync();
        }


        public async Task<KayitGrubu> KayitGrubuGetir(int id)
        {
            return await _dbContext.KayitGruplari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<KayitGrubu> YeniKayitGrubu(KayitGrubu kayitGrubu)
        {
            await _dbContext.KayitGruplari.AddAsync(kayitGrubu);
            await _dbContext.SaveChangesAsync();
            return kayitGrubu;
        }

        public async Task<KayitGrubu> KayitGrubuGuncelle(KayitGrubu kayitGrubu)
        {
            _dbContext.KayitGruplari.Update(kayitGrubu);
            await _dbContext.SaveChangesAsync();
            return kayitGrubu;
        }

        public async Task<bool> KayitGrubuSil(int kayitGrubuId)
        {
            KayitGrubu grb = await _dbContext.KayitGruplari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == kayitGrubuId);
            if (grb == null) return false;
            else _dbContext.KayitGruplari.Remove(grb);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Kayıt Türü

        public async Task<List<KayitTuru>> AktifKayitTuruListesi(int dilId, string grupKodu)
        {
            return await _dbContext.KayitTurleri.Where(x => x.DilId == dilId && x.Aktif && x.GrupKodu == grupKodu).AsNoTracking().ToListAsync();
        }

        public async Task<List<KayitTuru>> KayitTuruListesi(int dilId, string grupKodu)
        {
            return await _dbContext.KayitTurleri.Where(x => x.DilId == dilId && x.GrupKodu == grupKodu).AsNoTracking().ToListAsync();
        }

        public async Task<KayitTuru> KayitTuruGetir(int id)
        {
            return await _dbContext.KayitTurleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<KayitTuru> YeniKayitTuru(KayitTuru kayitTuru)
        {
            await _dbContext.KayitTurleri.AddAsync(kayitTuru);
            await _dbContext.SaveChangesAsync();
            return kayitTuru;
        }

        public async Task<KayitTuru> KayitTuruGuncelle(KayitTuru kayitTuru)
        {
            _dbContext.KayitTurleri.Update(kayitTuru);
            await _dbContext.SaveChangesAsync();
            return kayitTuru;
        }

        public async Task<bool> KayitTuruSil(int kayitTuruId)
        {
            KayitTuru kt = await KayitTuruGetir(kayitTuruId);
            if (kt == null) return false;
            _dbContext.KayitTurleri.Remove(kt);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
