using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerYorumModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerYorumDataServices;

public class PerformerYorumDataService : IPerformerYorumDataService
{
    private readonly ApplicationDbContext _dbContext;

    public PerformerYorumDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PerformerYorum> YeniPerformerYorum(PerformerYorum model)
    {
        await _dbContext.PerformerYorumlari.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<PerformerYorum>> PerformerYorumListesiGetir(string performerId)
    {
        List<PerformerYorum> list = await _dbContext.PerformerYorumlari
            .AsNoTracking()
            .Where(x => x.PerformerId == performerId)
            .OrderByDescending(x => x.EklenmeTarihi)
            .ToListAsync();

        return list;
    }

    public async Task<PerformerYorum> PerformerYorumGetirById(string id)
    {
        return await _dbContext.PerformerYorumlari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> PerformerYorumSil(PerformerYorum model)
    {
        _dbContext.PerformerYorumlari.Remove(model);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}