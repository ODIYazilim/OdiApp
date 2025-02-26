using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs.Interfaces;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;

public class PerformerCVDataService : IPerformerCVDataService
{
    ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public PerformerCVDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<PerformerCV> PerformerCVGetirById(string performerCVId)
    {
        return await _dbContext.PerformerCV.FirstOrDefaultAsync(x => x.Id == performerCVId);
    }

    public async Task<PerformerCVOutputDTO> PerformerCVGetirByUserId(string userId)
    {
        string query = @"select *,cv.Id as PerformerCVId, kb.KullaniciAdSoyad as 'PerformerAdSoyad' from PerformerCV cv 
                                left join KullaniciBasic kb on kb.KullaniciId=cv.PerformerId
                                where cv.PerformerId=@PerformerId";
        var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        PerformerCVOutputDTO cv = await connection.QueryFirstOrDefaultAsync<PerformerCVOutputDTO>(query, new { PerformerId = userId });

        return cv;
    }


    public async Task<PerformerCV> PerformerCVGuncelle(PerformerCV cv)
    {
        _dbContext.PerformerCV.Update(cv);
        await _dbContext.SaveChangesAsync();
        return cv;
    }



    public async Task<PerformerCV> YeniPerformerCV(PerformerCV cv)
    {
        _dbContext.PerformerCV.AddAsync(cv);
        await _dbContext.SaveChangesAsync();
        return cv;
    }

    public async Task<bool> PerformerCVVarMi(string PerformerId)
    {
        return await _dbContext.PerformerCV.AnyAsync(p => p.PerformerId == PerformerId);
    }

    public Task<CVDeneyim> YeniCVDeneyim(CVDeneyim deneyim)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CVDeneyimSil(int CVDeneyimId)
    {
        throw new NotImplementedException();
    }
}