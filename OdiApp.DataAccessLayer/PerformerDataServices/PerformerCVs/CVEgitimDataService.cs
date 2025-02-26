using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs.Interfaces;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;

public class CVEgitimDataService : ICVEgitimDataService
{
    ApplicationDbContext _dbContext;

    public CVEgitimDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //public async Task<CVEgitimBilgisi> YeniCVEgitim(CVEgitimBilgisi egitim)
    //{
    //    await _dbContext.CVEgitimBilgileri.AddAsync(egitim);
    //    await _dbContext.SaveChangesAsync();
    //    return egitim;
    //}
    //public async Task<CVEgitimBilgisi> CVEgitimGuncelle(CVEgitimBilgisi egtim)
    //{
    //    _dbContext.CVEgitimBilgileri.Update(egtim);
    //    await _dbContext.SaveChangesAsync();
    //    return egtim;
    //}

    //public async Task<List<CVEgitimBilgisi>> CVEgitimListesi(string CVId)
    //{

    //    return await _dbContext.CVEgitimBilgileri.Where(x=>x.CVId==CVId).ToListAsync();
    //}

    //public async Task<bool> CVEgitimSil(string cvEgitimId)
    //{
    //    CVEgitimBilgisi eg = await this.CVEgitimGetir(cvEgitimId);
    //    _dbContext.Remove(eg);
    //    _dbContext.SaveChanges();
    //    return true;
    //} 

    //public async Task<CVEgitimBilgisi> CVEgitimGetir(string cvEgitimId)
    //{
    //    CVEgitimBilgisi eg = await _dbContext.CVEgitimBilgileri.FirstOrDefaultAsync(x => x.Id == cvEgitimId);
    //    return eg;
    //}
}
