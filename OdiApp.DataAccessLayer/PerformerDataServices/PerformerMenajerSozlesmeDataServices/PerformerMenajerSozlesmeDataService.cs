using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerMenajerModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerMenajerSozlesmeDataServices;

public class PerformerMenajerSozlesmeDataService : IPerformerMenajerSozlesmeDataService
{
    private readonly ApplicationDbContext _dbContext;

    public PerformerMenajerSozlesmeDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PerformerMenajerSozlesme> YeniPerformerMenajerSozlesme(PerformerMenajerSozlesme model)
    {
        await _dbContext.PerformerMenajerSozlesmeleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerMenajerSozlesme> PerformerMenajerSozlesmeGuncelle(PerformerMenajerSozlesme model)
    {
        _dbContext.PerformerMenajerSozlesmeleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<PerformerMenajerSozlesme>> PerformerMenajerSozlesmeListesiGetir(string menajerId)
    {
        return await _dbContext.PerformerMenajerSozlesmeleri.AsNoTracking().Where(x => x.MenajerId == menajerId).ToListAsync();
    }

    public async Task<PerformerMenajerSozlesme> PerformerMenajerSozlesmeGetirById(string id)
    {
        return await _dbContext.PerformerMenajerSozlesmeleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PerformerMenajerSozlesme> PerformerMenajerSozlesmeGetirByMenajerPerformerId(string performerId, string menajerId)
    {
        return await _dbContext.PerformerMenajerSozlesmeleri.AsNoTracking().FirstOrDefaultAsync(x => x.PerformerId == performerId && x.MenajerId == menajerId);
    }

    public async Task<List<PerformerMenajerSozlesme>> PerformerMenajerSozlesmeListesiGetirByMenajerPerformerId(string performerId, string menajerId)
    {
        return await _dbContext.PerformerMenajerSozlesmeleri.AsNoTracking().Where(x => x.PerformerId == performerId && x.MenajerId == menajerId).ToListAsync();
    }
}