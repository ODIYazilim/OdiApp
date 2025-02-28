using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SehirDataServices
{
    public class SehirService : ISehirService
    {
        ApplicationDbContext _dbContext;

        public SehirService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Sehir>> SehirListesi()
        {
            return await _dbContext.Sehirler.Include(x => x.Ilceler).ToListAsync();
        }
    }
}
