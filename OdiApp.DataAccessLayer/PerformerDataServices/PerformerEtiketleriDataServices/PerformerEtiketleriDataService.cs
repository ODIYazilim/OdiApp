using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerEtiketleriDataServices;

public class PerformerEtiketleriDataService : IPerformerEtiketleriDataService
{
    private readonly ApplicationDbContext _dbContext;

    public PerformerEtiketleriDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Yetenek Temsilcisi Performer Etiket Tipi

    public async Task<YetenekTemsilcisiPerformerEtiketTipi> YeniYetenekTemsilcisiPerformerEtiketTipi(YetenekTemsilcisiPerformerEtiketTipi model)
    {
        await _dbContext.YetenekTemsilcisiPerformerEtiketTipleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<YetenekTemsilcisiPerformerEtiketTipi> YetenekTemsilcisiPerformerEtiketTipiGuncelle(YetenekTemsilcisiPerformerEtiketTipi model)
    {
        _dbContext.YetenekTemsilcisiPerformerEtiketTipleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<YetenekTemsilcisiPerformerEtiketTipi>> YetenekTemsilcisiPerformerEtiketTipiListesiGetir(int dilId, bool onlyAktif = true)
    {
        IQueryable<YetenekTemsilcisiPerformerEtiketTipi> query = _dbContext.YetenekTemsilcisiPerformerEtiketTipleri
            .AsNoTracking()
            .Where(x => x.DilId == dilId)
            .OrderBy(x => x.Sira);

        if (onlyAktif)
        {
            query = query.Where(x => x.Aktif == true);
        }

        List<YetenekTemsilcisiPerformerEtiketTipi> list = await query.ToListAsync();

        return list;
    }

    public async Task<YetenekTemsilcisiPerformerEtiketTipi> YetenekTemsilcisiPerformerEtiketTipiGetirById(string id)
    {
        return await _dbContext.YetenekTemsilcisiPerformerEtiketTipleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> YetenekTemsilcisiPerformerEtiketTipiSil(YetenekTemsilcisiPerformerEtiketTipi model)
    {
        _dbContext.YetenekTemsilcisiPerformerEtiketTipleri.Remove(model);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Performer Etiket

    public async Task<PerformerEtiket> YeniPerformerEtiket(PerformerEtiket model)
    {
        await _dbContext.PerformerEtiketleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerEtiket> PerformerEtiketGuncelle(PerformerEtiket model)
    {
        _dbContext.PerformerEtiketleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<PerformerEtiket>> PerformerEtiketListesiGetir(int dilId, bool onlyAktif = true)
    {
        IQueryable<PerformerEtiket> query = _dbContext.PerformerEtiketleri
            .AsNoTracking()
            .Where(x => x.DilId == dilId)
            .OrderBy(x => x.Sira);

        if (onlyAktif)
        {
            query = query.Where(x => x.Aktif == true);
        }

        List<PerformerEtiket> list = await query.ToListAsync();

        return list;
    }

    public async Task<PerformerEtiket> PerformerEtiketGetirById(string id)
    {
        return await _dbContext.PerformerEtiketleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> PerformerEtiketSil(PerformerEtiket model)
    {
        _dbContext.PerformerEtiketleri.Remove(model);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Yetenek Temsilcisi Performer Etiketleri

    public async Task<List<YetenekTemsilcisiPerformerEtiketi>> YetenekTemsilcisiPerformerEtiketiListesiGetir(string yetenekTemsilcisiId, string performerId)
    {
        return await _dbContext.YetenekTemsilcisiPerformerEtiketleri.AsNoTracking()
            .Where(x => x.YetenekTemsilcisiId == yetenekTemsilcisiId && x.PerformerId == performerId)
            .ToListAsync();
    }

    public async Task<YetenekTemsilcisiPerformerEtiketi> YetenekTemsilcisiPerformerEtiketiGuncelle(YetenekTemsilcisiPerformerEtiketi model)
    {
        _dbContext.YetenekTemsilcisiPerformerEtiketleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<YetenekTemsilcisiPerformerEtiketi> YetenekTemsilcisiPerformerEtiketiEkle(YetenekTemsilcisiPerformerEtiketi model)
    {
        await _dbContext.YetenekTemsilcisiPerformerEtiketleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<bool> YetenekTemsilcisiPerformerEtiketiTopluEkle(List<YetenekTemsilcisiPerformerEtiketi> model)
    {
        await _dbContext.YetenekTemsilcisiPerformerEtiketleri.AddRangeAsync(model);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> YetenekTemsilcisiPerformerEtiketiSil(string id)
    {
        var entity = await _dbContext.YetenekTemsilcisiPerformerEtiketleri.FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            _dbContext.YetenekTemsilcisiPerformerEtiketleri.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> YetenekTemsilcisiPerformerEtiketiTopluSil(List<YetenekTemsilcisiPerformerEtiketi> removeList)
    {
        _dbContext.YetenekTemsilcisiPerformerEtiketleri.RemoveRange(removeList);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion
}