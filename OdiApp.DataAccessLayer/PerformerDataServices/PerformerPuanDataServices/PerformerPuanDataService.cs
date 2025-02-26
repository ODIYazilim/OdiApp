using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerPuanModels;
using System.Data;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerPuanDataServices;

public class PerformerPuanDataService : IPerformerPuanDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public PerformerPuanDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<PerformerPuan> YeniPerformerPuan(PerformerPuan model)
    {
        await _dbContext.PerformerPuanlari.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerPuan> PerformerPuanGuncelle(PerformerPuan model)
    {
        _dbContext.PerformerPuanlari.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<PerformerPuan> PerformerPuanGetir(string id)
    {
        return await _dbContext.PerformerPuanlari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<PerformerPuan>> PerformerPuanListesiGetirByOyVeren(string oyverenId)
    {
        return await _dbContext.PerformerPuanlari.AsNoTracking().Where(x => x.OyVerenId == oyverenId).ToListAsync();
    }

    public async Task<List<PerformerPuan>> PerformerPuanListesiGetirByPerformer(string performerId, string? oyverenKayitGrubu, string? oyverenKayitTuru)
    {
        IQueryable<PerformerPuan> query = _dbContext.PerformerPuanlari.AsNoTracking().Where(x => x.PerformerId == performerId);

        if (!string.IsNullOrEmpty(oyverenKayitGrubu))
        {
            query = query.Where(x => x.OyVerenKayitGrubu == oyverenKayitGrubu);
        }

        if (!string.IsNullOrEmpty(oyverenKayitTuru))
        {
            query = query.Where(x => x.OyVerenKayitTuru == oyverenKayitTuru);
        }
        return await query.ToListAsync();
    }

    public async Task<PerformerPuanOutputDTO> PerformerPuanGetirByPerformerId(string performerId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new DynamicParameters();
        parameters.Add("@PerformerIds", performerId, DbType.String);

        var result = await connection.QueryFirstOrDefaultAsync<PerformerPuanOutputDTO>("PerformerPuanOrtalamalariniGetir", parameters, commandType: CommandType.StoredProcedure);
        return result;
    }

    // Liste halinde PerformerId'ler için puan getir
    public async Task<List<PerformerPuanOutputDTO>> PerformerListesiPuanGetir(List<string> performerIdList)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new DynamicParameters();
        parameters.Add("@PerformerIds", string.Join(",", performerIdList), DbType.String);

        var result = await connection.QueryAsync<PerformerPuanOutputDTO>("PerformerPuanOrtalamalariniGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }
}