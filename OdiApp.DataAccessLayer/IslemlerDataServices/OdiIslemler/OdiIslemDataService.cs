using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler
{
    public class OdiIslemDataService : IOdiIslemDataService
    {
        ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public OdiIslemDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<List<OdiTalep>> YeniOdiTalep(List<OdiTalep> odiTalepList)
        {
            await _dbContext.OdiTalepleri.AddRangeAsync(odiTalepList);
            await _dbContext.SaveChangesAsync();
            return odiTalepList;
        }

        public async Task<OdiTalep> OdiTalepGuncelle(OdiTalep odiTalep)
        {
            _dbContext.OdiTalepleri.Update(odiTalep);
            await _dbContext.SaveChangesAsync();
            return odiTalep;
        }

        public async Task<OdiTalep> OdiTalepGetirById(string odiTalepId)
        {
            return await _dbContext.OdiTalepleri.FirstOrDefaultAsync(x => x.Id == odiTalepId);
        }

        public async Task<List<OdiTalep>> OdiTalepListesiGetirByProjePerformer(string performerId, string projeId)
        {
            return await _dbContext.OdiTalepleri.Where(x => x.TalepGonderilenPerformerId == performerId && x.ProjeId == projeId).ToListAsync();
        }

        public async Task<OdiTalepOutputDTO> OdiTalepGetir(string odiTalepId)
        {
            string query = @"Select * from OdiTalepView where OdiTalepId=@OdiTalepId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { OdiTalepId = odiTalepId });
            return result.FirstOrDefault();
        }

        public async Task<List<OdiTalepOutputDTO>> OdiTalepListesiGetirByGonderen(string gonderenId)
        {
            string query = @"Select * from OdiTalepView where TalepGonderenId=@TalepGonderenId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { TalepGonderenId = gonderenId });
            return result.ToList();
        }

        public async Task<List<OdiTalepOutputDTO>> OdiTalepListesiGetirByGonderen(string gonderenId, int number)
        {
            string query = "Select Top " + number + " * from OdiTalepView where TalepGonderenId=@TalepGonderenId order by OdiTalepTarihi";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { TalepGonderenId = gonderenId });
            return result.ToList();
        }

        public async Task<List<OdiTalepOutputDTO>> OdiTalepListesiGetirByMenajer(string menajerId)
        {
            string query = @"Select * from OdiTalepView where TalepGonderilenMenajerId=@TalepGonderilenMenajerId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { TalepGonderilenMenajerId = menajerId });
            return result.ToList();

        }

        public async Task<List<OdiTalepPerformerIslemOutputDTO>> OdiTalepListesiGetirByPerformer(string performerId)
        {
            string query = @"Select * from OdiTalepView where TalepGonderilenPerformerId=@TalepGonderilenPerformerId and PerformeraIletildi=1";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepPerformerIslemOutputDTO>(query, new { TalepGonderilenPerformerId = performerId });
            return result.ToList();

        }

        public async Task<bool> CheckOdiTalep(string performerId, string rolId)
        {
            return await _dbContext.OdiTalepleri.AnyAsync(x => x.TalepGonderilenPerformerId == performerId && x.ProjeRolId == rolId);
        }

        //
        public async Task<List<(OdiTalepOutputDTO, PerformerOdi)>> MenajerIzlemeListesi(string menajerId)
        {
            string query = @"Select * from OdiTalepView where TalepGonderilenMenajerId=@TalepGonderilenMenajerId and OdiYuklendi=1";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { TalepGonderilenMenajerId = menajerId });

            List<OdiTalepOutputDTO> odiTaleplist = result.ToList();
            List<string> odiIdleri = odiTaleplist.Select(x => x.OdiTalepId).ToList();
            List<PerformerOdi> odiList = await _dbContext.PerformerOdi.Where(x => odiIdleri.Contains(x.OdiTalepId))
                .Include(x => x.PerformerOdiFotograflar)
                .Include(x => x.PerformerOdiSorular)
                .Include(x => x.PerformerOdiVideo)
                .Include(x => x.PerformerOdiSes)
                .Include(x => x.TekrarCekOneriListesi).ToListAsync();

            List<(OdiTalepOutputDTO, PerformerOdi)> list = new List<(OdiTalepOutputDTO, PerformerOdi)>();

            foreach (var talep in odiTaleplist)
            {
                var myTuple = (talep, odiList.FirstOrDefault(x => x.OdiTalepId == talep.OdiTalepId));
                list.Add(myTuple);
            }

            return list;
        }

        public async Task<List<(OdiTalepOutputDTO, PerformerOdi)>> PerformerIzlemeListesi(string performerId)
        {
            string query = @"Select * from OdiTalepView where TalepGonderilenPerformerId=@TalepGonderilenPerformerId and OdiYuklendi=1";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { TalepGonderilenPerformerId = performerId });

            List<OdiTalepOutputDTO> odiTaleplist = result.ToList();
            List<string> odiIdleri = odiTaleplist.Select(x => x.OdiTalepId).ToList();
            List<PerformerOdi> odiList = await _dbContext.PerformerOdi.Where(x => odiIdleri.Contains(x.OdiTalepId))
                .Include(x => x.PerformerOdiFotograflar)
                .Include(x => x.PerformerOdiSorular)
                .Include(x => x.PerformerOdiVideo)
                .Include(x => x.PerformerOdiSes)
                .Include(x => x.TekrarCekOneriListesi).ToListAsync();

            List<(OdiTalepOutputDTO, PerformerOdi)> list = new List<(OdiTalepOutputDTO, PerformerOdi)>();

            foreach (var talep in odiTaleplist)
            {
                var myTuple = (talep, odiList.FirstOrDefault(x => x.OdiTalepId == talep.OdiTalepId));
                list.Add(myTuple);
            }

            return list;
        }

        public async Task<List<(OdiTalepOutputDTO, PerformerOdi)>> YapimIzlemeListesi(List<string> yetkililer)
        {
            Array yetkiliIdleri = yetkililer.ToArray();
            string query = "Select * from OdiTalepView where TalepGonderenId in @YetkilIdleri and MenajerOdiOnayi=1";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiTalepOutputDTO>(query, new { YetkilIdleri = yetkiliIdleri });

            List<OdiTalepOutputDTO> odiTaleplist = result.ToList();
            List<string> odiIdleri = odiTaleplist.Select(x => x.OdiTalepId).ToList();
            List<PerformerOdi> odiList = await _dbContext.PerformerOdi.Where(x => odiIdleri.Contains(x.OdiTalepId))
                .Include(x => x.PerformerOdiFotograflar)
                .Include(x => x.PerformerOdiSorular)
                .Include(x => x.PerformerOdiVideo)
                .Include(x => x.PerformerOdiSes)
                .Include(x => x.TekrarCekOneriListesi).ToListAsync();

            List<(OdiTalepOutputDTO, PerformerOdi)> list = new List<(OdiTalepOutputDTO, PerformerOdi)>();

            foreach (var talep in odiTaleplist)
            {
                var myTuple = (talep, odiList.FirstOrDefault(x => x.OdiTalepId == talep.OdiTalepId));
                list.Add(myTuple);
            }

            return list;
        }



    }
}
