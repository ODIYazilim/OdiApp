using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;
using OdiApp.EntityLayer.PerformerModels.OnerilerModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.OnerilerDataServices;

public class OnerilerDataService : IOnerilerDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public OnerilerDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<OneriTalepleri> YeniOneriTalep(OneriTalepleri model)
    {
        await _dbContext.OneriTalepleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<MenajerPerformerOnerileri>> YeniMenajerPerformerOneri(List<MenajerPerformerOnerileri> model)
    {
        await _dbContext.MenajerPerformerOnerileri.AddRangeAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<PerformerDisplayInfoDTO>> MenajerPerformerOneriListesiGetir(string projeId, string menajerId)
    {
        string query = @"
                SELECT pdi.*
                FROM PerformerDisplayInfo pdi
                INNER JOIN (
                    SELECT DISTINCT pdi.KullaniciId
                    FROM PerformerDisplayInfo pdi
                    INNER JOIN MenajerPerformerOnerileri mpo ON pdi.KullaniciId = mpo.PerformerId
                    WHERE mpo.ProjeId = @ProjeId
                    AND mpo.MenajerId = @MenajerId
                ) AS sub ON pdi.KullaniciId = sub.KullaniciId;
            ";


        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<PerformerDisplayInfoDTO>(query, new { ProjeId = projeId, MenajerId = menajerId });
        return result.ToList();
    }
}