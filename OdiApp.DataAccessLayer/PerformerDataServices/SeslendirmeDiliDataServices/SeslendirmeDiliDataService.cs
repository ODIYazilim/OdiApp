using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SeslendirmeDiliDataServices;

public class SeslendirmeDiliDataService : ISeslendirmeDiliDataService
{
    private readonly ApplicationDbContext _dbContext;

    public SeslendirmeDiliDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SeslendirmeDili> YeniSeslendirmeDili(SeslendirmeDili model)
    {
        await _dbContext.SeslendirmeDilleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<SeslendirmeDili> SeslendirmeDiliGuncelle(SeslendirmeDili model)
    {
        _dbContext.SeslendirmeDilleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<SeslendirmeDili>> SeslendirmeDiliListesiGetir(int dilId)
    {
        IQueryable<SeslendirmeDili> query = _dbContext.SeslendirmeDilleri.AsNoTracking();

        if (dilId >= 0)
        {
            query = query.Where(x => x.DilId == dilId);
        }

        return await query.ToListAsync();
    }

    public async Task<SeslendirmeDili> SeslendirmeDiliGetir(int id)
    {
        return await _dbContext.SeslendirmeDilleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}