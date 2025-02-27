using OdiApp.EntityLayer.IslemlerModels.RolSendeModels;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.RolSendeDataServices
{
    public class RolSendeDataService : IRolSendeDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public RolSendeDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RolSende> YeniRolSende(RolSende rolSende)
        {
            await _dbContext.RolSende.AddAsync(rolSende);
            await _dbContext.SaveChangesAsync();

            return rolSende;
        }
    }
}