using Microsoft.EntityFrameworkCore;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs.Interfaces;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;

public class CVYetenekDataService : ICVYetenekDataService
{
    ApplicationDbContext _dbContext;
    public CVYetenekDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //public async Task<CVYetenekBilgisi> CVYetenekGuncelle(CVYetenekBilgisi yetenek)
    //{
    //    _dbContext.CVYetenekBilgileri.Update(yetenek);
    //    await _dbContext.SaveChangesAsync();
    //    return yetenek;
    //}

    //public async Task<CVYetenekBilgisi> CVYetenekGetir(int cvYetenekId)
    //{
    //    CVYetenekBilgisi eg = await _dbContext.CVYetenekBilgileri.FirstOrDefaultAsync(x => x.Id == cvYetenekId);
    //    return eg;
    //}

    public async Task<List<CVYetenek>> CVYetenekListesi(string CVId)
    {
        return await _dbContext.CVYetenekleri.Where(x => x.CVId == CVId).ToListAsync();

    }

    //public async Task<bool> CVYetenekSil(int cvYetenekId)
    //{
    //    CVYetenekBilgisi eg = await this.CVYetenekGetir(cvYetenekId);
    //    _dbContext.Remove(eg);
    //    _dbContext.SaveChanges();
    //    return true;
    //}

    public async Task<CVYetenek> YeniCVYetenek(CVYetenek yetenek)
    {
        await _dbContext.CVYetenekleri.AddAsync(yetenek);
        await _dbContext.SaveChangesAsync();
        return yetenek;
    }
}
