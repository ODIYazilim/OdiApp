using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.EntityLayer.PerformerModels.SektorModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SektorDataServices;

public class SektorDataService : ISektorDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public SektorDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<List<Sektor>> SektorListesiGetir(int dilId)
    {
        return await _dbContext.Sektorler.AsNoTracking().Where(x => x.DilId == dilId).OrderBy(o => o.Sira).ToListAsync();
    }
}