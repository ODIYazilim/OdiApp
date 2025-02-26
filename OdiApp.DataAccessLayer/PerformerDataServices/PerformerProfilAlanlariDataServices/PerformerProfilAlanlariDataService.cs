using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;
using System.Data;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerProfilAlanlariDataServices;

public class PerformerProfilAlanlariDataService : IPerformerProfilAlanlariDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public PerformerProfilAlanlariDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<PerformerProfilAlanlari> YeniPerformerProfilAlanlari(PerformerProfilAlanlari model)
    {
        await _dbContext.PerformerProfilAlanlari.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerProfilAlanlari> PerformerProfilAlanlariGuncelle(PerformerProfilAlanlari model)
    {
        _dbContext.PerformerProfilAlanlari.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<PerformerProfilAlanlari>> PerformerProfilAlanlariListesiGetir(string? kayitTuru)
    {
        IQueryable<PerformerProfilAlanlari> query = _dbContext.PerformerProfilAlanlari.AsNoTracking();

        if (!string.IsNullOrEmpty(kayitTuru))
        {
            query = query.Where(x => x.PerfomerKayitTuru == kayitTuru);
        }

        return await query.ToListAsync();
    }

    public async Task<PerformerProfilAlanlari> PerformerProfilAlanlariGetir(int id)
    {
        return await _dbContext.PerformerProfilAlanlari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ProfilDolulukOraniOutputDTO> ProfilDolulukOrani(string kullaniciId)
    {
        using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PerformerId", kullaniciId, DbType.String);

            using (var multi = await dbConnection.QueryMultipleAsync(
                "ProfilDolulukOraniGetir",
                parameters,
                commandType: CommandType.StoredProcedure))
            {
                var profilDolulukOrani = await multi.ReadSingleAsync<int>();
                var eksikAlanlar = (await multi.ReadAsync<string>()).ToList();

                return new ProfilDolulukOraniOutputDTO
                {
                    ProfilDolulukOrani = profilDolulukOrani,
                    EksikAlanlar = eksikAlanlar
                };
            }
        }
    }
}