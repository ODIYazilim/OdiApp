using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler
{
    public class PerformerOdiDataService : IPerformerOdiDataService
    {
        ApplicationDbContext _dbContext;

        public PerformerOdiDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PerformerOdi> YeniPerformerOdi(PerformerOdi odi)
        {
            await _dbContext.PerformerOdi.AddAsync(odi);
            await _dbContext.SaveChangesAsync();
            return odi;
        }
        public async Task<List<PerformerOdiSoru>> YeniPerformerOdiSoru(List<PerformerOdiSoru> sorular)
        {
            await _dbContext.PerformerOdiSorulari.AddRangeAsync(sorular);
            await _dbContext.SaveChangesAsync();
            return sorular;
        }

        public async Task<List<PerformerOdiFotograf>> YeniPerformerOdiFotograf(List<PerformerOdiFotograf> fotograflar)
        {
            await _dbContext.PerformerOdiFotograflar.AddRangeAsync(fotograflar);
            await _dbContext.SaveChangesAsync();
            return fotograflar;
        }

        public async Task<PerformerOdiSes> YeniPerformerOdiSes(PerformerOdiSes ses)
        {
            await _dbContext.PerformerOdiSesler.AddAsync(ses);
            await _dbContext.SaveChangesAsync();
            return ses;
        }

        public async Task<PerformerOdiVideo> YeniPerformerOdiVideo(PerformerOdiVideo video)
        {
            await _dbContext.PerformerOdiVideolar.AddAsync(video);
            await _dbContext.SaveChangesAsync();
            return video;
        }
        /// //
        public async Task<PerformerOdi> PerformerOdiGuncelle(PerformerOdi odi)
        {
            _dbContext.PerformerOdi.Update(odi);
            await _dbContext.SaveChangesAsync();
            return odi;
        }
        public async Task<PerformerOdiSoru> PerformerOdiSoruGuncelle(PerformerOdiSoru soru)
        {
            _dbContext.PerformerOdiSorulari.Update(soru);
            await _dbContext.SaveChangesAsync();
            return soru;
        }

        public async Task<PerformerOdiFotograf> PerformerOdiFotografGuncelle(PerformerOdiFotograf fotograf)
        {
            _dbContext.PerformerOdiFotograflar.Update(fotograf);
            await _dbContext.SaveChangesAsync();
            return fotograf;
        }

        public async Task<PerformerOdiSes> PerformerOdiSesGuncelle(PerformerOdiSes ses)
        {
            _dbContext.PerformerOdiSesler.UpdateRange(ses);
            await _dbContext.SaveChangesAsync();
            return ses;
        }

        public async Task<PerformerOdiVideo> PerformerOdiVideoGuncelle(PerformerOdiVideo video)
        {
            _dbContext.PerformerOdiVideolar.UpdateRange(video);
            await _dbContext.SaveChangesAsync();
            return video;
        }

        public async Task<PerformerOdi> PerformerOdiGetir(string odiTalepId)
        {
            return await _dbContext.PerformerOdi.Include(x => x.PerformerOdiSorular).Include(x => x.PerformerOdiFotograflar).Include(x => x.PerformerOdiVideo).Include(x => x.PerformerOdiSes).Include(x => x.TekrarCekOneriListesi).AsNoTracking().FirstOrDefaultAsync(x => x.OdiTalepId == odiTalepId);
        }
        public async Task<PerformerOdi> PerformerOdiGetirbyId(string performerOdiId)
        {
            return await _dbContext.PerformerOdi.Include(x => x.PerformerOdiSorular).Include(x => x.PerformerOdiFotograflar).Include(x => x.PerformerOdiVideo).Include(x => x.PerformerOdiSes).Include(x => x.TekrarCekOneriListesi).AsNoTracking().FirstOrDefaultAsync(x => x.Id == performerOdiId);
        }
        public async Task<List<PerformerOdiSoru>> PerformerOdiSoruListesi(string performerOdiId)
        {
            return await _dbContext.PerformerOdiSorulari.Where(x => x.PerformerOdiId == performerOdiId).AsNoTracking().ToListAsync();
        }
        public async Task<List<PerformerOdiFotograf>> PerformerOdiFotografListesi(string performerOdiId)
        {
            return await _dbContext.PerformerOdiFotograflar.Where(x => x.PerformerOdiId == performerOdiId).AsNoTracking().ToListAsync();
        }

        public async Task<PerformerOdiSes> PerformerOdiSesGetir(string performerOdiId)
        {
            return await _dbContext.PerformerOdiSesler.AsNoTracking().FirstOrDefaultAsync(x => x.PerformerOdiId == performerOdiId);
        }

        public async Task<PerformerOdiVideo> PerformerOdiVideoGetir(string performerOdiId)
        {
            return await _dbContext.PerformerOdiVideolar.AsNoTracking().FirstOrDefaultAsync(x => x.PerformerOdiId == performerOdiId);
        }

        public async Task<PerformerOdiTekrarCekOneri> YeniPerformerOdiTekrarCekOnerisi(PerformerOdiTekrarCekOneri oneri)
        {
            await _dbContext.PerformerOdiTekrarCekOnerileri.AddAsync(oneri);
            await _dbContext.SaveChangesAsync();
            return oneri;
        }
        public async Task<List<PerformerOdiTekrarCekOneri>> PerformerOdiTekrarCekOneriListesi(string performerOdiId)
        {
            return await _dbContext.PerformerOdiTekrarCekOnerileri.Where(x => x.PerformerOdiId == performerOdiId).ToListAsync();
        }
    }
}
