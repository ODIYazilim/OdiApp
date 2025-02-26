using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.FizikselOzelliklerDTOs;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

namespace OdiApp.DataAccessLayer.PerformerDataServices.FizikselOzellikler;

public class FizikselOzellikDataService : IFizikselOzellikDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public FizikselOzellikDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    public async Task<FizikselOzellikTipi> FizikselOzellikTipiEkle(FizikselOzellikTipi tip)
    {
        await _dbContext.AddAsync(tip);
        await _dbContext.SaveChangesAsync();
        return tip;
    }

    public async Task<FizikselOzellikTipi> FizikselOzellikTipiGuncelle(FizikselOzellikTipi tip)
    {
        _dbContext.Update(tip);
        await _dbContext.SaveChangesAsync();
        return tip;
    }
    public Task<FizikselOzellikTipi> FizikselOzellikTipiGetir(int tipId)
    {
        return _dbContext.FizikselOzellikTipleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tipId);
    }
    public async Task<List<FizikselOzellikTipi>> FizikselOzellikTipiListe(int dilId)
    {
        return await _dbContext.FizikselOzellikTipleri.AsNoTracking().Where(x => x.DilId == dilId).ToListAsync();
    }

    public async Task<List<FizikselOzellikTipiOutputDTO>> FizikselOzellikTipiListesi(int dilId)
    {
        const string query = @"
        SELECT 
            fot.FizikselOzellikTipKodu, 
            fot.FizikselOzellikTipAdi,
            fo.FizikselOzellikKodu, 
            fo.FizikselOzellikAdi
        FROM FizikselOzellikTipleri fot
        LEFT JOIN FizikselOzellikler fo 
            ON fot.FizikselOzellikTipKodu = fo.FizikselOzellikTipKodu
        WHERE fot.DilId = @DilId AND fo.DilId = @DilId";

        var dictionary = new Dictionary<string, FizikselOzellikTipiOutputDTO>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var result = await connection.QueryAsync<FizikselOzellikTipiOutputDTO, FizikselOzellikOutputDTO, FizikselOzellikTipiOutputDTO>(
                query,
                (tip, ozellik) =>
                {
                    if (!dictionary.TryGetValue(tip.FizikselOzellikTipKodu, out var entry))
                    {
                        entry = tip;
                        entry.Liste = new List<FizikselOzellikOutputDTO>();
                        dictionary[tip.FizikselOzellikTipKodu] = entry;
                    }

                    entry.Liste.Add(ozellik);
                    return entry;
                },
                new { DilId = dilId },
                splitOn: "FizikselOzellikKodu"
            );
        }

        return dictionary.Values.ToList();
    }

    public async Task<List<FizikselOzellikTipiOutputDTO>> FizikselOzellikTipiListesiByKayitTuruKodu(string kayitTuruKodlari, int dilId)
    {
        const string query = @"
                SELECT DISTINCT
                    cf.KayitTuruKodu,
                    fot.FizikselOzellikTipKodu,
                    fot.FizikselOzellikTipAdi,
                    fo.FizikselOzellikKodu,
                    fo.FizikselOzellikAdi
                FROM CVFormAlanlari cf
                INNER JOIN FizikselOzellikTipleri fot 
                    ON cf.AlanKodu = fot.FizikselOzellikTipKodu 
                       AND cf.AlanTipi = 'Fiziksel Özellikler' 
                       AND fot.DilId = @DilId
                INNER JOIN FizikselOzellikler fo 
                    ON fot.FizikselOzellikTipKodu = fo.FizikselOzellikTipKodu
                       AND fo.DilId = @DilId
                WHERE cf.KayitTuruKodu IN (SELECT Value FROM dbo.CustomStringSplit(@KayitTuruKodlari, ','));";

        var dictionary = new Dictionary<string, FizikselOzellikTipiOutputDTO>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var result = await connection.QueryAsync<FizikselOzellikTipiOutputDTO, FizikselOzellikOutputDTO, FizikselOzellikTipiOutputDTO>(
                query,
                (tip, ozellik) =>
                {
                    if (!dictionary.TryGetValue(tip.FizikselOzellikTipKodu, out var tipEntry))
                    {
                        tipEntry = tip;
                        tipEntry.Liste = new List<FizikselOzellikOutputDTO>();
                        dictionary[tip.FizikselOzellikTipKodu] = tipEntry;
                    }

                    // Benzersizlik kontrolü
                    if (!tipEntry.Liste.Any(o => o.FizikselOzellikKodu == ozellik.FizikselOzellikKodu))
                    {
                        tipEntry.Liste.Add(ozellik);
                    }

                    return tipEntry;
                },
                new { KayitTuruKodlari = kayitTuruKodlari, DilId = dilId },
                splitOn: "FizikselOzellikKodu"
            );
        }

        return dictionary.Values.ToList();
    }


    public async Task<bool> FizikselOzellikTipiSil(FizikselOzellikTipi tip)
    {
        _dbContext.FizikselOzellikTipleri.Remove(tip);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<FizikselOzellik>> FizikselOzellikListesi(string fizikselOzellikTipKodu)
    {
        return await _dbContext.FizikselOzellikler.Where(x => x.FizikselOzellikTipKodu == fizikselOzellikTipKodu).AsNoTracking().ToListAsync();
    }

    public async Task<List<FizikselOzellik>> FizikselOzellikListesiByDilId(string fizikselOzellikTipKodu, int dilId)
    {
        return await _dbContext.FizikselOzellikler.Where(x => x.FizikselOzellikTipKodu == fizikselOzellikTipKodu && x.DilId == dilId).AsNoTracking().ToListAsync();
    }

    public async Task<List<FizikselOzellik>> TumFizikselOzellikListesiByDilId(int dilId)
    {
        return await _dbContext.FizikselOzellikler.Where(x => x.DilId == dilId).AsNoTracking().ToListAsync();
    }
}