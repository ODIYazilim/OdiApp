using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.MenajerPerformerNotModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerNotDataServices;

public class MenajerPerformerNotDataService : IMenajerPerformerNotDataService
{
    private readonly ApplicationDbContext _dbContext;

    public MenajerPerformerNotDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MenajerPerformerNot> YeniMenajerPerformerNot(MenajerPerformerNot model)
    {
        await _dbContext.MenajerPerformerNotlari.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<MenajerPerformerNot> MenajerPerformerNotGuncelle(MenajerPerformerNot model)
    {
        _dbContext.MenajerPerformerNotlari.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<MenajerPerformerNot> MenajerPerformerNotGetir(string performerId, string menajerId)
    {
        return await _dbContext.MenajerPerformerNotlari.AsNoTracking().FirstOrDefaultAsync(x => x.PerformerId == performerId && x.MenajerId == menajerId);
    }

    public async Task<bool> MenajerPerformerNotKontrolEt(string performerId, string menajerId)
    {
        return await _dbContext.MenajerPerformerNotlari.AnyAsync(x => x.PerformerId == performerId && x.MenajerId == menajerId);
    }
}