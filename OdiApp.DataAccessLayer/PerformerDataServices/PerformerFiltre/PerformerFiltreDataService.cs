using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerFiltre;

public class PerformerFiltreDataService : IPerformerFiltreDataService
{
    ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public PerformerFiltreDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<List<PerformerDisplayInfoDTO>> PerformerDisplayInfoList()
    {
        string query = @"SELECT * FROM PerformerDisplayInfo";
        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<PerformerDisplayInfoDTO>(query);
        return result.ToList();
    }

    public async Task<List<PerformerDisplayInfoDTO>> PerformerDetayListesi(List<string> idList)
    {
        string query = $"SELECT * FROM PerformerDisplayInfo WHERE KullaniciId IN ({string.Join(", ", idList.Select(id => $"'{id}'"))})";

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<PerformerDisplayInfoDTO>(query);
        return result.ToList();
    }
}