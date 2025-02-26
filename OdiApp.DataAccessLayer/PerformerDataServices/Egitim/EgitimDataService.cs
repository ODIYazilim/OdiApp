using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.Egitim;

namespace OdiApp.DataAccessLayer.PerformerDataServices.Egitim;

public class EgitimDataService : IEgitimDataService
{
    ApplicationDbContext _dbContext;

    public EgitimDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<EgitimTipi> YeniEgitimTipi(EgitimTipi tip)
    {
        await _dbContext.EgitimTipleri.AddAsync(tip);
        await _dbContext.SaveChangesAsync();
        return tip;
    }

    public async Task<Okul> YeniOkul(Okul okul)
    {
        await _dbContext.Okullar.AddAsync(okul);
        await _dbContext.SaveChangesAsync();
        return okul;
    }

    public async Task<OkulBolum> YeniOkulBolum(OkulBolum okulBolum)
    {
        await _dbContext.OkulBolumler.AddAsync(okulBolum);
        await _dbContext.SaveChangesAsync();
        return okulBolum;
    }

    public async Task<List<EgitimTipi>> EgitimListesi()
    {
        return await _dbContext.EgitimTipleri.Include(x => x.Okullar).ThenInclude(x => x.Bolumler).ToListAsync();
    }
    public async Task<List<EgitimTipi>> EgitimTipiListesi()
    {
        return await _dbContext.EgitimTipleri.ToListAsync();
    }
}