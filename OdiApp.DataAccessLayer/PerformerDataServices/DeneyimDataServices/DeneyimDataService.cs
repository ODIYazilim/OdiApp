using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.DeneyimDTOs;
using OdiApp.EntityLayer.PerformerModels.Deneyimler;

namespace OdiApp.DataAccessLayer.PerformerDataServices.DeneyimDataServices;

public class DeneyimDataService : IDeneyimDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public DeneyimDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<List<Deneyim>> DeneyimTipiListesi(int dilId)
    {
        return await _dbContext.Deneyimler.Where(x => x.DilId == dilId && x.Aktif).ToListAsync();
    }

    public async Task<List<DeneyimDTO>> DeneyimListesi(int dilId)
    {
        List<DeneyimDTO> Deneyimler = new List<DeneyimDTO>();
        string query = @" select DeneyimAdi,DeneyimKodu from Deneyimler where DilId=@DilId and Aktif=1 order by Sira ";
        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);

        var result = await connection.QueryAsync<DeneyimDTO>(query, new { DilId = dilId });
        Deneyimler = result.ToList();

        string query2 = @"select dd.FormAlaniKodu, dfa.AlanAdi as FormAlaniAdi, dfa.DataType ,dfa.KarakterSiniri from DeneyimDetaylari dd   left join
                            DeneyimFormAlanlari dfa on dfa.AlanKodu=dd.FormAlaniKodu 
                            where DeneyimKodu=@DeneyimKodu and dfa.DilId=@dilId  and dd.Aktif=1
                            order by dd.Sira";


        foreach (var item in Deneyimler)
        {
            var result2 = await connection.QueryAsync<DeneyimFormAlanlariDTO>(query2, new { item.DeneyimKodu, DilId = dilId });
            item.Alanlar = result2.ToList();
        }
        return Deneyimler;
    }

    public async Task<List<CVDeneyimOutputDTO>> CVDeneyimListesi(string cvId, int dilId)
    {
        List<CVDeneyimOutputDTO> list = new List<CVDeneyimOutputDTO>();

        string query = @"select cvd.Id as 'CVDeneyimId', cvd.DeneyimKodu,d.DeneyimAdi  From CVDeneyimler cvd 
                            left join Deneyimler d on d.Deneyimkodu=cvd.DeneyimKodu
                            where cvd.CVId=@CVId and d.DilId=@DilId  order by d.Sira";

        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        var result = await connection.QueryAsync<CVDeneyimOutputDTO>(query, new { CVId = cvId, DilId = dilId });
        list = result.ToList();

        string query2 = @"Select cvdd.FormAlaniKodu as AlanKodu,dfa.AlanAdi,cvdd.Deger from CVDeneyimDetaylari cvdd 
							left join DeneyimFormAlanlari dfa on dfa.AlanKodu=cvdd.FormAlaniKodu
							where cvdd.CVDeneyimId=@CVDeneyimId and dfa.DilId=@DilId";

        foreach (var item in list)
        {
            var result2 = await connection.QueryAsync<CVDeneyimDetayOutputDTO>(query2, new { item.CVDeneyimId, DilId = dilId });
            item.Detaylar = result2.ToList();
        }
        return list;
    }
}
