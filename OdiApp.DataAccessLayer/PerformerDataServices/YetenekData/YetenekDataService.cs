using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.YetenekData;

public class YetenekDataService : IYetenekDataService
{
    ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public YetenekDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<YetenekTipi> YeniYetenekTipi(YetenekTipi yetenekTipi)
    {
        await _dbContext.YetenekTipleri.AddAsync(yetenekTipi);
        await _dbContext.SaveChangesAsync();
        return yetenekTipi;
    }
    public async Task<Yetenek> YeniYetenek(Yetenek yetenek)
    {
        await _dbContext.Yetenekler.AddAsync(yetenek);
        await _dbContext.SaveChangesAsync();
        return yetenek;
    }

    public async Task<List<YetenekTipiDTO>> YetenekListesi(int dilId)
    {
        string query = "SELECT Tip AS YetenekTipiAdi, YetenekTipiKodu FROM YetenekTipleri WHERE DilId=@DilId";
        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<YetenekTipiDTO>(query, new { DilId = dilId });
        string query2 = "SELECT YetenekAdi, YetenekKodu FROM Yetenekler WHERE YetenekTipiKodu=@YetenekTipiKodu AND DilId=@DilId";

        foreach (var item in result)
        {
            var result2 = await connection.QueryAsync<YetenekOutputDTO>(query2, new { item.YetenekTipiKodu, DilId = dilId });
            item.Liste = result2.ToList();
        }

        return result.ToList();
    }
}