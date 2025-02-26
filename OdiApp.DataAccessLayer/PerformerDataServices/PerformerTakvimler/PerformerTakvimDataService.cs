using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerTakvimModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerTakvimler;

public class PerformerTakvimDataService : IPerformerTakvimDataService
{
    ApplicationDbContext _dbContext;

    public PerformerTakvimDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PerformerTakvim> YeniTarihAraligi(PerformerTakvim performerTakvim)
    {
        await _dbContext.PerformerTakvim.AddAsync(performerTakvim);
        await _dbContext.SaveChangesAsync();

        return performerTakvim;
    }

    public async Task<bool> MusaitlikKontrolu(string performerId, DateTime startDate, DateTime endDate)
    {
        // Check for overlapping date range
        var isAvailable = await _dbContext.PerformerTakvim
            .Where(pt => pt.PerformerId == performerId &&
                          !(endDate < pt.BaslangicTarihi || startDate > pt.BitisTarihi))
            .FirstOrDefaultAsync();

        // If isAvailable is null, there is no overlapping date range, so the performer is available
        return isAvailable == null;
    }

    public async Task<PerformerTakvim> TarihAraligiDuzenle(PerformerTakvim performerTakvim)
    {
        _dbContext.PerformerTakvim.Update(performerTakvim);
        await _dbContext.SaveChangesAsync();

        return performerTakvim;
    }

    public async Task<PerformerTakvim> TarihAraligiGetir(string performerTakvimId)
    {
        return await _dbContext.PerformerTakvim.AsNoTracking().FirstOrDefaultAsync(f => f.Id == performerTakvimId);
    }

    public async Task<bool> TarihAraligiSil(string performerListeId)
    {
        PerformerTakvim performerTakvim = await TarihAraligiGetir(performerListeId);

        if (performerTakvim == null) return false;

        _dbContext.PerformerTakvim.Remove(performerTakvim);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<PerformerTakvim>> ZamanAraligiSorgula(string performerId, DateTime baslangicTarihi, DateTime bitisTarihi)
    {
        List<PerformerTakvim> result = await _dbContext.PerformerTakvim
        .Where(p =>
            p.PerformerId == performerId &&
            (baslangicTarihi <= p.BitisTarihi && bitisTarihi >= p.BaslangicTarihi ||
            baslangicTarihi >= p.BaslangicTarihi && bitisTarihi <= p.BitisTarihi ||
            baslangicTarihi <= p.BitisTarihi && bitisTarihi >= p.BitisTarihi ||
            baslangicTarihi <= p.BaslangicTarihi && bitisTarihi >= p.BaslangicTarihi)
        )
        .ToListAsync();

        return result;
    }

    public async Task<List<PerformerTakvim>> AylikTakvimSorgula(string performerId, int ay, int yil)
    {
        List<PerformerTakvim> result = await _dbContext.PerformerTakvim
            .Where(p =>
                p.PerformerId == performerId &&
                (p.BaslangicTarihi.Month == ay && p.BaslangicTarihi.Year == yil ||
                p.BitisTarihi.Month == ay && p.BitisTarihi.Year == yil)
            )
            .ToListAsync();

        return result;
    }
}