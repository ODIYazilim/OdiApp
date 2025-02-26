using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.EntityLayer.PerformerModels.KisiselOzellikler;

namespace OdiApp.DataAccessLayer.PerformerDataServices.KisiselOzelliklerDataServices;

public class KisiselOzelliklerDataService : IKisiselOzelliklerDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public KisiselOzelliklerDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<KisiselOzellik> KisiselOzellikEkle(KisiselOzellik tip)
    {
        await _dbContext.AddAsync(tip);
        await _dbContext.SaveChangesAsync();
        return tip;
    }

    public async Task<KisiselOzellik> KisiselOzellikGuncelle(KisiselOzellik tip)
    {
        _dbContext.Update(tip);
        await _dbContext.SaveChangesAsync();
        return tip;
    }
    public Task<KisiselOzellik> KisiselOzellikGetir(int tipId)
    {
        return _dbContext.KisiselOzellikler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tipId);
    }
    public async Task<List<KisiselOzellik>> KisiselOzellikListe(int dilId)
    {
        return await _dbContext.KisiselOzellikler.AsNoTracking().Where(x => x.DilId == dilId).ToListAsync();
    }

    public async Task<bool> KisiselOzellikSil(KisiselOzellik e)
    {
        _dbContext.KisiselOzellikler.Remove(e);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}