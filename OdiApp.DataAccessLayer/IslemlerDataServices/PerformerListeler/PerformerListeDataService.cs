using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.IslemlerModels.PerformerListeler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.PerformerListeler
{
    public class PerformerListeDataService : IPerformerListeDataService
    {
        ApplicationDbContext _dbContext;

        public PerformerListeDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PerformerListe>> PerformerListeListesi(string kullaniciId)
        {
            return await _dbContext.PerformerListe.AsNoTracking().Where(x => x.KullaniciId == kullaniciId).ToListAsync();
        }

        public async Task<PerformerListe> PerformerListeGetirWithDetay(string performerListId)
        {
            return await _dbContext.PerformerListe.Include(x => x.Performerlar).AsNoTracking().FirstOrDefaultAsync(f => f.Id == performerListId);
        }

        public async Task<PerformerListe> PerformerListeGetir(string performerListId)
        {
            return await _dbContext.PerformerListe.AsNoTracking().FirstOrDefaultAsync(f => f.Id == performerListId);
        }

        public async Task<PerformerListe> YeniPerformerListe(PerformerListe performerListe)
        {
            await _dbContext.PerformerListe.AddAsync(performerListe);
            await _dbContext.SaveChangesAsync();

            return performerListe;
        }

        public async Task<PerformerListe> PerformerListeGuncelle(PerformerListe performerListe)
        {
            _dbContext.PerformerListe.Update(performerListe);
            await _dbContext.SaveChangesAsync();

            return performerListe;
        }

        public async Task<bool> PerformerListeSil(string performerListeId)
        {
            PerformerListe performerListe = await PerformerListeGetir(performerListeId);

            if (performerListe == null) return false;

            _dbContext.PerformerListe.Remove(performerListe);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<PerformerListeDetay>> YeniPerformerListeDetay(List<PerformerListeDetay> performerListeDetayList)
        {
            await _dbContext.PerformerListeDetay.AddRangeAsync(performerListeDetayList);
            await _dbContext.SaveChangesAsync();
            return performerListeDetayList;
        }

        public async Task<bool> PerformerListeDetaySil(List<PerformerListeDetay> performerListeDetayList)
        {
            _dbContext.PerformerListeDetay.RemoveRange(performerListeDetayList);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<PerformerListeDetay>> PerformerListeDetayGetir(List<string> performerListeDetayIdList)
        {
            return await _dbContext.PerformerListeDetay.AsNoTracking().Where(w => performerListeDetayIdList.Contains(w.Id)).ToListAsync();
        }
    }
}