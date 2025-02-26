using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikUrunuDataServices;

public class PerformerAbonelikUrunuDataService : IPerformerAbonelikUrunuDataService
{
    private readonly ApplicationDbContext _dbContext;

    public PerformerAbonelikUrunuDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PerformerAbonelikUrunu> YeniPerformerAbonelikUrunu(PerformerAbonelikUrunu model)
    {
        await _dbContext.PerformerAbonelikUrunleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerAbonelikUrunu> PerformerAbonelikUrunuGuncelle(PerformerAbonelikUrunu model)
    {
        _dbContext.PerformerAbonelikUrunleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerAbonelikUrunu> PerformerAbonelikUrunuGetir(string id)
    {
        return await _dbContext.PerformerAbonelikUrunleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PerformerAbonelikUrunu> PerformerAbonelikUrunuGetirByPeriod(int period)
    {
        return await _dbContext.PerformerAbonelikUrunleri.AsNoTracking().FirstOrDefaultAsync(x => x.OdemePeriodu == period);
    }

    public async Task<List<PerformerAbonelikUrunu>> PerformerAbonelikUrunListesiGetir(bool onlyAktif = true)
    {
        IQueryable<PerformerAbonelikUrunu> query = _dbContext.PerformerAbonelikUrunleri.AsNoTracking();

        if (onlyAktif)
        {
            query = query.Where(x => x.Aktif == true);
        }

        return await query.ToListAsync();
    }
}