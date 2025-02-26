using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.SesRengiModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SesRengiDataServices;

public class SesRengiDataService : ISesRengiDataService
{
    private readonly ApplicationDbContext _dbContext;

    public SesRengiDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SesRengi> YeniSesRengi(SesRengi model)
    {
        await _dbContext.SesRenkleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<SesRengi> SesRengiGuncelle(SesRengi model)
    {
        _dbContext.SesRenkleri.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<SesRengi>> SesRengiListesiGetir(int dilId)
    {
        IQueryable<SesRengi> query = _dbContext.SesRenkleri.AsNoTracking();

        if (dilId >= 0)
        {
            query = query.Where(x => x.DilId == dilId);
        }

        return await query.ToListAsync();
    }

    public async Task<SesRengi> SesRengiGetir(int id)
    {
        return await _dbContext.SesRenkleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}