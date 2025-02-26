using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerGuncellenenAlaniDataServices;

public class MenajerPerformerGuncellenenAlaniDataService : IMenajerPerformerGuncellenenAlaniDataService
{
    private readonly ApplicationDbContext _dbContext;

    public MenajerPerformerGuncellenenAlaniDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MenajerPerformerGuncellenenAlani> YeniMenajerPerformerGuncellenenAlani(MenajerPerformerGuncellenenAlani model)
    {
        await _dbContext.MenajerPerformerGuncellenenAlanlari.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<MenajerPerformerGuncellenenAlani>> YeniTopluMenajerPerformerGuncellenenAlani(List<MenajerPerformerGuncellenenAlani> model)
    {
        await _dbContext.MenajerPerformerGuncellenenAlanlari.AddRangeAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<MenajerPerformerGuncellenenAlani> MenajerPerformerGuncellenenAlaniGuncelle(MenajerPerformerGuncellenenAlani model)
    {
        _dbContext.MenajerPerformerGuncellenenAlanlari.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<MenajerPerformerGuncellenenAlani> MenajerPerformerGuncellenenAlaniGetir(string id)
    {
        return await _dbContext.MenajerPerformerGuncellenenAlanlari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<MenajerPerformerGuncellenenAlani>> MenajerPerformerGuncellenenAlaniListesiSon1AyGetir(string performerId, string menajerId)
    {
        return await _dbContext.MenajerPerformerGuncellenenAlanlari.AsNoTracking().Where(x => x.PerformerId == performerId && x.MenajerId == menajerId && x.GuncellenmeTarihi > DateTime.Now.AddMonths(-1)).ToListAsync();
    }

    //public async Task<List<string>> MenajerGuncellenenPerformerlarIdListesi(string yetenekTemsilcisiId, bool gorulduOlanlariDahilEt, DateTime? eklenmeTarihindenItibaren)
    //{
    //    IQueryable<MenajerPerformerGuncellenenAlani> menajerPerformerGuncellenenAlaniQuery = _dbContext.MenajerPerformerGuncellenenAlanlari.AsNoTracking().Where(x => x.MenajerId == yetenekTemsilcisiId);

    //    if (!gorulduOlanlariDahilEt)
    //    {
    //        menajerPerformerGuncellenenAlaniQuery = menajerPerformerGuncellenenAlaniQuery.Where(x => x.MenajerGordu == false);
    //    }

    //    if (eklenmeTarihindenItibaren != null)
    //    {
    //        menajerPerformerGuncellenenAlaniQuery = menajerPerformerGuncellenenAlaniQuery.Where(x => eklenmeTarihindenItibaren < x.EklenmeTarihi);
    //    }

    //    return await menajerPerformerGuncellenenAlaniQuery.Select(s => s.PerformerId).ToListAsync();
    //}
}