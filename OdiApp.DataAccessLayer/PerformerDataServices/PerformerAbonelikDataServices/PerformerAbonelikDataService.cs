using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikDataServices;

public class PerformerAbonelikDataService : IPerformerAbonelikDataService
{
    private readonly ApplicationDbContext _dbContext;

    public PerformerAbonelikDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PerformerAbonelik> YeniPerformerAbonelik(PerformerAbonelik model)
    {
        await _dbContext.PerformerAbonelikleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerAbonelik> PerformerAbonelikGuncelle(PerformerAbonelik model)
    {
        _dbContext.PerformerAbonelikleri.Update(model);
        await _dbContext.SaveChangesAsync();


        return model;
    }

    public async Task<PerformerAbonelik> PerformerAbonelikGetirById(string id)
    {
        return await _dbContext.PerformerAbonelikleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PerformerAbonelik> PerformerAbonelikGetirByPerformerId(string performerId)
    {
        return await _dbContext.PerformerAbonelikleri.AsNoTracking().FirstOrDefaultAsync(x => x.PerformerId == performerId && x.Aktif);
    }

    public async Task<PerformerAbonelikSureUzatma> YeniPerformerAbonelikSureUzatma(PerformerAbonelikSureUzatma model)
    {
        await _dbContext.PerformerAbonelikSureUzatmalari.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<bool> PerformerAbonelikKayitKontrolu(string performerAbonelikUrunuId, string performerId)
    {
        return !await _dbContext.PerformerAbonelikleri.AnyAsync(x => x.PerformerId == performerId && x.AbonelikUrunId == performerAbonelikUrunuId && x.Aktif);
    }
}