using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels.ProjeMesajlasma;
using System.Data;

namespace OdiApp.DataAccessLayer.BildirimDataServices.ProjeMesajlasmaDataServices
{
    public class ProjeMesajlasmaDataService : IProjeMesajlasmaDataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProjeMesajlasmaDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<ProjeMesaj> YeniProjeMesaj(ProjeMesaj projeMesaj)
        {
            await _dbContext.ProjeMesaj.AddAsync(projeMesaj);
            await _dbContext.SaveChangesAsync();

            return projeMesaj;
        }

        public async Task<ProjeMesajDetay> YeniProjeMesajDetay(ProjeMesajDetay projeMesajDetay)
        {
            await _dbContext.ProjeMesajDetay.AddAsync(projeMesajDetay);
            await _dbContext.SaveChangesAsync();

            return projeMesajDetay;
        }

        public async Task<List<ProjeMesajDetay>> YeniProjeMesajDetay(List<ProjeMesajDetay> projeMesajDetay)
        {
            await _dbContext.ProjeMesajDetay.AddRangeAsync(projeMesajDetay);
            await _dbContext.SaveChangesAsync();

            return projeMesajDetay;
        }

        public async Task<ProjeMesajOutputDTO> ProjeMesajGetir(string projeMesajId)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { ProjeMesajId = projeMesajId };
            var result = await connection.QueryFirstOrDefaultAsync<ProjeMesajOutputDTO>("ProjeMesajGetir", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<ProjeMesajOutputDTO> ProjeMesajGetir(string kullanici1Id, string kullanici2Id)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { Kullanici1Id = kullanici1Id, Kullanici2Id = kullanici2Id };
            var result = await connection.QueryFirstOrDefaultAsync<ProjeMesajOutputDTO>("ProjeMesajGetir", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<ProjeMesajOutputDTO>> ProjeMesajListesiWithKullaniciId(string kullaniciId)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { KullaniciId = kullaniciId };
            var result = await connection.QueryAsync<ProjeMesajOutputDTO>("ProjeMesajGetir", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        //public async Task<List<ProjeMesajOutputDTO>> ProjeMesajListesiWithFirmaKodu(string firmaKodu)
        //{
        //    using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        //    var parameters = new { FirmaKodu = firmaKodu };
        //    var result = await connection.QueryAsync<ProjeMesajOutputDTO>("ProjeMesajGetir", parameters, commandType: CommandType.StoredProcedure);
        //    return result.ToList();
        //}

        public async Task<ProjeMesajDetayOutputDTO> ProjeMesajDetayGetir(string projeMesajDetayId)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { ProjeMesajDetayId = projeMesajDetayId };
            var result = await connection.QueryFirstOrDefaultAsync<ProjeMesajDetayOutputDTO>("ProjeMesajDetayGetirById", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<PagedData<ProjeMesajDetayOutputDTO>> ProjeMesajDetayListesi(string kullanici1Id, string kullanici2Id, int pageNo, int recordsPerPage)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { Kullanici1Id = kullanici1Id, Kullanici2Id = kullanici2Id, PageNo = pageNo, RecordsPerPage = recordsPerPage };

            var result = await connection.QueryMultipleAsync("ProjeMesajDetayGetirByUsers", parameters, commandType: CommandType.StoredProcedure);

            var data = result.Read<ProjeMesajDetayOutputDTO>().ToList();
            var pageInfo = result.Read<PagedDataInfo>().SingleOrDefault();

            return new PagedData<ProjeMesajDetayOutputDTO>
            {
                PageNo = pageInfo?.PageNo ?? 1,
                PageCount = pageInfo?.PageCount ?? 1,
                Records = pageInfo?.Records ?? 0,
                RecordsPerPage = pageInfo?.RecordsPerPage ?? 0,
                DataList = data
            };
        }

        public async Task<List<ProjeMesajDetayOutputDTO>> ProjeMesajDetayListesi(List<string> projeMesajDetayIdList)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new DynamicParameters();
            parameters.Add("@ProjeMesajDetayIdList", string.Join(",", projeMesajDetayIdList), DbType.String);
            var result = await connection.QueryAsync<ProjeMesajDetayOutputDTO>("ProjeMesajDetayGetirByIdList", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<bool> ProjeMesajGoruldu(string projeMesajDetayId)
        {
            ProjeMesajDetay projeMesajDetay = await _dbContext.ProjeMesajDetay.FirstOrDefaultAsync(x => x.Id == projeMesajDetayId);
            if (projeMesajDetay == null) return false;
            projeMesajDetay.Okundu = true;
            _dbContext.ProjeMesajDetay.Update(projeMesajDetay);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProjeMesajGoruldu(List<string> projeMesajDetayIdList)
        {
            List<ProjeMesajDetay> projeMesajDetayList = await _dbContext.ProjeMesajDetay.Where(x => projeMesajDetayIdList.Contains(x.Id)).ToListAsync();
            if (projeMesajDetayList?.Any() == false) return false;

            foreach (var item in projeMesajDetayList)
            {
                item.Okundu = true;
            }

            _dbContext.ProjeMesajDetay.UpdateRange(projeMesajDetayList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProjeMesajSilme(string projeMesajId)
        {
            ProjeMesaj projeMesaj = await _dbContext.ProjeMesaj.FirstOrDefaultAsync(x => x.Id == projeMesajId);
            if (projeMesaj == null) return false;
            _dbContext.ProjeMesaj.Remove(projeMesaj);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProjeMesajSilme(List<string> projeMesajIdList)
        {
            List<ProjeMesaj> projeMesajList = await _dbContext.ProjeMesaj.Where(x => projeMesajIdList.Contains(x.Id)).ToListAsync();
            if (projeMesajList?.Any() == false) return false;
            _dbContext.ProjeMesaj.RemoveRange(projeMesajList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProjeMesajDetaySilme(string projeMesajDetayId)
        {
            ProjeMesajDetay projeMesajDetay = await _dbContext.ProjeMesajDetay.FirstOrDefaultAsync(x => x.Id == projeMesajDetayId);
            if (projeMesajDetay == null) return false;
            _dbContext.ProjeMesajDetay.Remove(projeMesajDetay);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProjeMesajDetaySilme(List<string> projeMesajDetayIdList)
        {
            List<ProjeMesajDetay> projeMesajDetayList = await _dbContext.ProjeMesajDetay.Where(x => projeMesajDetayIdList.Contains(x.Id)).ToListAsync();
            if (projeMesajDetayList?.Any() == false) return false;
            _dbContext.ProjeMesajDetay.RemoveRange(projeMesajDetayList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}