using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikUrunuOdemePlaniDataServices
{
    public class AbonelikUrunuOdemePlaniDataService : IAbonelikUrunuOdemePlaniDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public AbonelikUrunuOdemePlaniDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AbonelikUrunuOdemePlani> YeniAbonelikUrunuOdemePlani(AbonelikUrunuOdemePlani model)
        {
            await _dbContext.AbonelikUrunuOdemePlanlari.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<AbonelikUrunuOdemePlani> AbonelikUrunuOdemePlaniGuncelle(AbonelikUrunuOdemePlani model)
        {
            _dbContext.AbonelikUrunuOdemePlanlari.Update(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<AbonelikUrunuOdemePlani>> AbonelikUrunuOdemePlaniListesiGetir()
        {
            return await _dbContext.AbonelikUrunuOdemePlanlari.AsNoTracking().ToListAsync();
        }

        public async Task<AbonelikUrunuOdemePlani> AbonelikUrunuOdemePlaniGetirByReferenceCode(string referenceCode)
        {
            return await _dbContext.AbonelikUrunuOdemePlanlari.AsNoTracking().FirstOrDefaultAsync(x => x.ReferenceCode == referenceCode);
        }
    }
}