using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;
using OdiApp.DTOs.IslemlerDTOs.PerformerIslemler;
using OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OpsiyonIslemler
{
    public class OpsiyonDataService : IOpsiyonDataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public OpsiyonDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        #region 

        public async Task<List<OpsiyonListesi>> OpsiyonListesineEkle(List<OpsiyonListesi> opsList)
        {
            await _dbContext.OpsiyonListeleri.AddRangeAsync(opsList);
            await _dbContext.SaveChangesAsync();
            return opsList;
        }
        public async Task<List<OpsiyonListesiViewDTO>> OpsiyonListesiGetir(List<string> yetkiliIdleri, string projeId)
        {
            string query = @"Select * from OpsiyonListesiView where ListeyeEkleyenKullaniciId in @YetkilIdleri and ProjeId=@ProjeId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OpsiyonListesiViewDTO>(query, new { YetkilIdleri = yetkiliIdleri, ProjeId = projeId });
            List<OpsiyonListesiViewDTO> list = new List<OpsiyonListesiViewDTO>();
            foreach (var item in result)
            {
                item.Opsiyon = await OpsiyonViewGetir(item.OpsiyonListesiId);
            }
            return result.ToList();
        }

        public async Task<List<OpsiyonListesiViewDTO>> MenajerOpsiyonListesiGetir(string menajerId, string projeId)
        {
            string query = @"Select * from OpsiyonListesiView where MenajerId = @MenajerId and ProjeId=@ProjeId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OpsiyonListesiViewDTO>(query, new { MenajerId = menajerId, ProjeId = projeId });
            List<OpsiyonListesiViewDTO> list = new List<OpsiyonListesiViewDTO>();
            foreach (var item in result)
            {
                item.Opsiyon = await OpsiyonViewGetir(item.OpsiyonListesiId);
            }
            return result.ToList();
        }
        public async Task<OpsiyonViewDTO> OpsiyonViewGetir(string OpsiyonListesiId)
        {
            string query = "Select * from OpsiyonView where OpsiyonListesiId = @OpsiyonListesiId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OpsiyonViewDTO>(query, new { OpsiyonListesiId });
            OpsiyonViewDTO ops = result.FirstOrDefault();

            List<OpsiyonAnketSorulariOutputDTO> anketList = new List<OpsiyonAnketSorulariOutputDTO>();

            if (ops != null)
            {
                string query2 = "Select * from OpsiyonAnketSorulariView where OpsiyonId = @OpsiyonId";
                var result2 = await connection.QueryAsync<OpsiyonAnketSorulariOutputDTO>(query2, new { ops.OpsiyonId });
                ops.AnketSorulari = result2.ToList();
            }

            //var results = await connection.QueryAsync<OpsiyonViewDTO, OpsiyonAnketSorulariOutputDTO, OpsiyonViewDTO>(
            //   query,
            //   (op, anket) =>
            //   {
            //       op.AnketSorulari.Add(anket);
            //       return op;
            //   },
            //   new { OpsiyonListesiId = OpsiyonListesiId },
            //   splitOn: "OpsiyonAnketSorulariId");


            return ops;
        }
        public async Task<OpsiyonListesi> OpsiyonListesiGetir(string OpsiyonListId)
        {
            return _dbContext.OpsiyonListeleri.Include(x => x.Opsiyon).FirstOrDefault(x => x.Id == OpsiyonListId);
        }
        public async Task<bool> OpsiyonListesindenCikar(OpsiyonListesi opsList)
        {
            _dbContext.OpsiyonListeleri.Remove(opsList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckOpsiyonListesi(string performerId, string projeRolId)
        {
            return await _dbContext.OpsiyonListeleri.AnyAsync(x => x.PerformerId == performerId && x.ProjeRolId == projeRolId);
        }
        #endregion

        #region Opsiyon
        public async Task<List<OpsiyonViewDTO>> YeniOpsiyon(List<Opsiyon> opsiyonList)
        {
            await _dbContext.Opsiyon.AddRangeAsync(opsiyonList);
            await _dbContext.SaveChangesAsync();

            string query = "Select * from OpsiyonView where OpsiyonId in @Ids";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var results = await connection.QueryAsync<OpsiyonViewDTO, OpsiyonAnketSorulariOutputDTO, OpsiyonViewDTO>(
               query,
               (op, anket) =>
               {
                   op.AnketSorulari.Add(anket);
                   return op;
               },
               new { Ids = opsiyonList.Select(x => x.Id).ToList() },
               splitOn: "OpsiyonAnketSorulariId");


            return results.ToList();
        }

        public async Task<Opsiyon> OpsiyonGetir(string opsiyonId)
        {
            return await _dbContext.Opsiyon.AsNoTracking().Include(x => x.AnketSorulari).FirstOrDefaultAsync(f => f.Id == opsiyonId);
        }
        public async Task<OpsiyonViewDTO> OpsiyonViewGetirById(string opsiyonId)
        {
            string query = "Select * from OpsiyonView where Id = @Id";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OpsiyonViewDTO>(query, new { Id = opsiyonId });


            OpsiyonViewDTO ops = result.FirstOrDefault();

            List<OpsiyonAnketSorulariOutputDTO> anketList = new List<OpsiyonAnketSorulariOutputDTO>();

            if (ops != null)
            {
                string query2 = "Select * from OpsiyonAnketSorulariView where OpsiyonId = @OpsiyonId";
                var result2 = await connection.QueryAsync<OpsiyonAnketSorulariOutputDTO>(query2, new { ops.OpsiyonId });
                ops.AnketSorulari = result2.ToList();
            }

            return result as OpsiyonViewDTO;
        }
        public async Task<bool> CheckOpsiyon(string projeId, string menajerId, string performerId)
        {
            return await _dbContext.Opsiyon.AnyAsync(x => x.ProjeId == projeId && x.MenajerId == menajerId && x.PerformerId == performerId);
        }

        public async Task<Opsiyon> OpsiyonGuncelle(Opsiyon opsiyon)
        {
            _dbContext.Opsiyon.Update(opsiyon);
            await _dbContext.SaveChangesAsync();

            return opsiyon;
        }

        //PerfomerİSlemler için
        public async Task<RolOpsiyonBilgisiDTO> RolOpsiyonBilgisiGetir(string rolId)
        {
            Opsiyon ops = await _dbContext.Opsiyon.FirstOrDefaultAsync(x => x.ProjeRolId == rolId && x.PerformeraIletildi == true);
            if (ops == null) return null;
            RolOpsiyonBilgisiDTO opsBilgisi = new RolOpsiyonBilgisiDTO { ProjeRolId = ops.ProjeRolId, OpsiyonId = ops.Id };
            return opsBilgisi;
        }

        public async Task<OpsiyonAnketSorulari> OpsiyonAnketSorusuGetir(string SoruId)
        {
            return await _dbContext.OpsiyonAnketSorulari.FirstOrDefaultAsync(x => x.Id == SoruId);
        }

        public async Task<OpsiyonAnketSorulari> OpsiyonAnketSorusuGuncelle(OpsiyonAnketSorulari soru)
        {
            _dbContext.OpsiyonAnketSorulari.Update(soru);
            await _dbContext.SaveChangesAsync();
            return soru;
        }
        #endregion
    }
}