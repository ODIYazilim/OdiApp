using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.PerformerDTOs.SetCard;
using System.Data;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SetCard;

public class SetCardDataService : ISetCardDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public SetCardDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<SetCardKisiselBilgilerDTO> KisiselBilgilerGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { KullaniciId = kullaniciId };
        var result = await connection.QueryFirstOrDefaultAsync<SetCardKisiselBilgilerDTO>("SetCardKisiselBilgilerGetir", parameters, commandType: CommandType.StoredProcedure);
        return result;
    }

    public async Task<List<SetCardFizikselOzellikDTO>> FizikselOzellikleriGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { PerformerId = kullaniciId, DilId = dilId };
        var result = await connection.QueryAsync<SetCardFizikselOzellikDTO>("SetCardFizikselOzelliklerGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<SetCardKisiselOzellikDTO>> KisiselOzellikleriGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { PerformerId = kullaniciId, DilId = dilId };
        var result = await connection.QueryAsync<SetCardKisiselOzellikDTO>("SetCardKisiselOzelliklerGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<SetCardEgitimBilgileriDTO>> EgitimBilgileriGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { PerformerId = kullaniciId };
        var result = await connection.QueryAsync<SetCardEgitimBilgileriDTO>("SetCardEgitimBilgileriGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<SetCardYetenekBilgileriDTO>> YetenekBilgileriGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { PerformerId = kullaniciId, DilId = dilId };
        var result = await connection.QueryAsync<SetCardYetenekBilgileriDTO>("SetCardYetenekBilgileriGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<SetCardDeneyimBilgileriDTO>> DeneyimBilgileriGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { PerformerId = kullaniciId, DilId = dilId };
        var result = await connection.QueryAsync<SetCardDeneyimBilgileriDTO>("SetCardDeneyimBilgileriGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<SetCardAlbumVeFotografDTO>> AlbumVeFotograflariGetir(string kullaniciId, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var parameters = new { KullaniciId = kullaniciId, DilId = dilId };
        var result = await connection.QueryAsync<SetCardAlbumVeFotografDTO>("SetCardAlbumveFotoGetir", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }
}