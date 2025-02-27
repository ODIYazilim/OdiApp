using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.MutluCellSmsDataServices
{
    public class MutluCellSmsDataService : IMutluCellSmsDataService
    {
        ApplicationDbContext _dbContext;

        public MutluCellSmsDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MutluCellSmsAyarlari> AyarlariGetir()
        {
            return await _dbContext.MutluCellSmsAyarlari.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<MutluCellSmsAyarlari> AyarlariGuncelle(MutluCellSmsAyarlari model)
        {
            _dbContext.MutluCellSmsAyarlari.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}