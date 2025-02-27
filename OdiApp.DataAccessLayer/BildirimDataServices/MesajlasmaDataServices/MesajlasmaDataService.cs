using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.BildirimDTOs.Mesajlasma;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels.Mesajlasma;
using System.Data;

namespace OdiApp.DataAccessLayer.BildirimDataServices.MesajlasmaDataServices
{
    public class MesajlasmaDataService : IMesajlasmaDataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public MesajlasmaDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Mesaj> YeniMesaj(Mesaj mesaj)
        {
            await _dbContext.Mesaj.AddAsync(mesaj);
            await _dbContext.SaveChangesAsync();

            return mesaj;
        }

        public async Task<MesajDetay> YeniMesajDetay(MesajDetay mesajDetay)
        {
            await _dbContext.MesajDetay.AddAsync(mesajDetay);
            await _dbContext.SaveChangesAsync();

            return mesajDetay;
        }

        public async Task<List<MesajDetay>> YeniMesajDetay(List<MesajDetay> mesajDetay)
        {
            await _dbContext.MesajDetay.AddRangeAsync(mesajDetay);
            await _dbContext.SaveChangesAsync();

            return mesajDetay;
        }

        public async Task<MesajOutputDTO> MesajGetir(string mesajId)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { MesajId = mesajId };
            var result = await connection.QueryFirstOrDefaultAsync<MesajOutputDTO>("MesajGetir", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<MesajOutputDTO> MesajGetir(string kullanici1Id, string kullanici2Id)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { Kullanici1Id = kullanici1Id, Kullanici2Id = kullanici2Id };
            var result = await connection.QueryFirstOrDefaultAsync<MesajOutputDTO>("MesajGetir", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<MesajOutputDTO>> MesajListesi(string kullaniciId)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { KullaniciId = kullaniciId };
            var result = await connection.QueryAsync<MesajOutputDTO>("MesajGetir", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<MesajDetayOutputDTO> MesajDetayGetir(string mesajDetayId)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { MesajDetayId = mesajDetayId };
            var result = await connection.QueryFirstOrDefaultAsync<MesajDetayOutputDTO>("MesajDetayGetirById", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<PagedData<MesajDetayOutputDTO>> MesajDetayListesi(string kullanici1Id, string kullanici2Id, int pageNo, int recordsPerPage)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new { Kullanici1Id = kullanici1Id, Kullanici2Id = kullanici2Id, PageNo = pageNo, RecordsPerPage = recordsPerPage };

            var result = await connection.QueryMultipleAsync("MesajDetayGetirByUsers", parameters, commandType: CommandType.StoredProcedure);

            var data = result.Read<MesajDetayOutputDTO>().ToList();
            var pageInfo = result.Read<PagedDataInfo>().SingleOrDefault();

            return new PagedData<MesajDetayOutputDTO>
            {
                PageNo = pageInfo?.PageNo ?? 1,
                PageCount = pageInfo?.PageCount ?? 1,
                Records = pageInfo?.Records ?? 0,
                RecordsPerPage = pageInfo?.RecordsPerPage ?? 0,
                DataList = data
            };
        }

        public async Task<List<MesajDetayOutputDTO>> MesajDetayListesi(List<string> mesajDetayIdList)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var parameters = new DynamicParameters();
            parameters.Add("@MesajDetayIdList", string.Join(",", mesajDetayIdList), DbType.String);
            var result = await connection.QueryAsync<MesajDetayOutputDTO>("MesajDetayGetirByIdList", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<bool> MesajGoruldu(string mesajDetayId)
        {
            MesajDetay mesajDetay = await _dbContext.MesajDetay.FirstOrDefaultAsync(x => x.Id == mesajDetayId);
            if (mesajDetay == null) return false;
            mesajDetay.Okundu = true;
            _dbContext.MesajDetay.Update(mesajDetay);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MesajGoruldu(List<string> mesajDetayIdList)
        {
            List<MesajDetay> mesajDetayList = await _dbContext.MesajDetay.Where(x => mesajDetayIdList.Contains(x.Id)).ToListAsync();
            if (mesajDetayList?.Any() == false) return false;

            foreach (var item in mesajDetayList)
            {
                item.Okundu = true;
            }

            _dbContext.MesajDetay.UpdateRange(mesajDetayList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MesajSilme(string mesajId)
        {
            Mesaj mesaj = await _dbContext.Mesaj.FirstOrDefaultAsync(x => x.Id == mesajId);
            if (mesaj == null) return false;
            _dbContext.Mesaj.Remove(mesaj);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MesajSilme(List<string> mesajIdList)
        {
            List<Mesaj> mesajList = await _dbContext.Mesaj.Where(x => mesajIdList.Contains(x.Id)).ToListAsync();
            if (mesajList?.Any() == false) return false;
            _dbContext.Mesaj.RemoveRange(mesajList);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MesajDetaySilme(string mesajDetayId)
        {
            MesajDetay mesajDetay = await _dbContext.MesajDetay.FirstOrDefaultAsync(x => x.Id == mesajDetayId);
            if (mesajDetay == null) return false;
            _dbContext.MesajDetay.Remove(mesajDetay);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MesajDetaySilme(List<string> mesajDetayIdList)
        {
            List<MesajDetay> mesajDetayList = await _dbContext.MesajDetay.Where(x => mesajDetayIdList.Contains(x.Id)).ToListAsync();
            if (mesajDetayList?.Any() == false) return false;
            _dbContext.MesajDetay.RemoveRange(mesajDetayList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}