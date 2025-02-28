using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.ProjeBilgileri
{
    public class ProjeDataService : IProjeDataService
    {
        ApplicationDbContext _dbContext;
        public ProjeDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Proje> ProjeGetir(string projeId)
        {
            Proje proje = await _dbContext.Projeler.Include(x => x.Yetkililer).AsNoTracking().FirstOrDefaultAsync(x => x.Id == projeId);
            if (proje != null) proje.ProjeTuru = await _dbContext.ProjeTurleri.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeTurKodu == proje.ProjeTurKodu && x.DilId == proje.DilId);
            return proje;
        }

        public async Task<Proje> ProjeGuncelle(Proje proje)
        {
            _dbContext.Projeler.Update(proje);
            await _dbContext.SaveChangesAsync();
            return proje;
        }

        public async Task<bool> ProjeSil(string projeId)
        {
            Proje proje = await ProjeGetir(projeId);
            if (proje == null) return false;
            _dbContext.Remove(proje);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Proje> YeniProje(Proje proje)
        {
            await _dbContext.Projeler.AddAsync(proje);
            await _dbContext.SaveChangesAsync();
            return proje;
        }

        public async Task<List<string>> ProjeFotografiArama(string fotografAdi)
        {
            List<string> liste = await _dbContext.Projeler
                        .Where(p => p.FotografAdi.Contains(fotografAdi))
                        .Select(p => p.Fotograf)
                        .Distinct()
                        .ToListAsync();

            return liste;
        }

        #region Proje Yetkili Listesi
        public async Task<List<ProjeYetkili>> ProjeYetkiliListesiGuncelle(List<ProjeYetkili> list)
        {
            _dbContext.ProjeYetkilileri.UpdateRange(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }
        public async Task<ProjeYetkili> ProjeYetkiliGuncelle(ProjeYetkili py)
        {
            _dbContext.ProjeYetkilileri.Update(py);
            await _dbContext.SaveChangesAsync();
            return py;
        }
        public async Task ProjeYetkiliListesiSil(List<ProjeYetkili> list)
        {
            _dbContext.ProjeYetkilileri.RemoveRange(list);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProjeYetkili>> ProjeYeniYetkiliListesi(List<ProjeYetkili> list)
        {
            await _dbContext.ProjeYetkilileri.AddRangeAsync(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }
        public async Task<ProjeYetkili> ProjeYetkiliGetir(string projeYetkiliId)
        {

            return await _dbContext.ProjeYetkilileri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == projeYetkiliId);
        }
        public async Task<List<ProjeYetkili>> ProjeYetkiliListesi(string projeId)
        {
            List<ProjeYetkili> yetkiliiler = await _dbContext.ProjeYetkilileri.Where(x => x.ProjeId == projeId).ToListAsync();
            return yetkiliiler;
        }

        #endregion

        #region PROJE TURLERİ
        public async Task<List<ProjeTuru>> ProjeTuruListe(int dilId)
        {
            return await _dbContext.ProjeTurleri.Where(x => x.DilId == dilId).ToListAsync();
        }
        #endregion

        #region PROJE LİSTELERİ

        public async Task<List<Proje>> TumProjeler(int dilId)
        {
            List<Proje> projeler = await _dbContext.Projeler
                .Include(x => x.Yetkililer)
                .AsNoTracking()
                .ToListAsync();

            List<ProjeTuru> projeTurleri = await _dbContext.ProjeTurleri
                .Where(x => x.DilId == dilId)
                .AsNoTracking()
                .ToListAsync();

            Dictionary<string, ProjeTuru> projeTurleriDict = projeTurleri.ToDictionary(x => x.ProjeTurKodu);

            foreach (var proje in projeler)
            {
                if (proje.ProjeTurKodu != null && projeTurleriDict.TryGetValue(proje.ProjeTurKodu, out var projeTuru))
                {
                    proje.ProjeTuru = projeTuru;
                }
            }

            return projeler;
        }

        public async Task<List<Proje>> PerformerProjeListesi(List<string> projeIdList, int dilId)
        {
            List<Proje> projeler = await _dbContext.Projeler
                .Include(x => x.Yetkililer)
                .Where(x => projeIdList.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();

            var projeTurleri = await _dbContext.ProjeTurleri
                .Where(x => x.DilId == dilId)
                .AsNoTracking()
                .ToListAsync();

            Dictionary<string, ProjeTuru> projeTurleriDict = projeTurleri.ToDictionary(x => x.ProjeTurKodu);

            foreach (var proje in projeler)
            {
                if (proje.ProjeTurKodu != null && projeTurleriDict.TryGetValue(proje.ProjeTurKodu, out var projeTuru))
                {
                    proje.ProjeTuru = projeTuru;
                }
            }

            return projeler;
        }


        #endregion

        #region Proje Default Logo

        public async Task<List<ProjeDefaultLogo>> ProjeDefaultLogoListe()
        {
            return await _dbContext.ProjeDefaultLogolari.ToListAsync();
        }

        #endregion

        #region Proje Katılım Bölgesi

        public async Task<List<ProjeKatilimBolgesi>> ProjeKatilimBolgesiListe()
        {
            return await _dbContext.ProjeKatilimBolgeleri.ToListAsync();
        }

        #endregion
    }
}
