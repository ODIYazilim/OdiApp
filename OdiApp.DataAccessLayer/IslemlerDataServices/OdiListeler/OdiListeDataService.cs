using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.IslemlerDTOs.OdiListeler;
using OdiApp.EntityLayer.IslemlerModels.OdiListeler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OdiListeler
{
    public class OdiListeDataService : IOdiListeDataService
    {
        ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public OdiListeDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<OdiListe> YeniOdiListe(OdiListe liste)
        {
            await _dbContext.OdiListeleri.AddRangeAsync(liste);
            await _dbContext.SaveChangesAsync();
            return liste;
        }

        public async Task<List<OdiListeDetay>> YeniOdiListeDetay(List<OdiListeDetay> listeDetayList)
        {
            await _dbContext.OdiListeDetay.AddRangeAsync(listeDetayList);
            await _dbContext.SaveChangesAsync();
            return listeDetayList;
        }

        public async Task<List<OdiListeAdlariOutputDTO>> OdiListeListesi(string KullaniciId)
        {
            return await _dbContext.OdiListeleri.Where(x => x.KullaniciId == KullaniciId).Select(x => new OdiListeAdlariOutputDTO { OdiListeAdi = x.ListeAdi, OdiListeId = x.Id, Begendiklerim = x.Begendiklerim, Belki = x.Belki, YetkililerlePaylasilsin = x.YetkililerlePaylasilsin }).ToListAsync();
        }
        public async Task<OdiListeOutputDTO> OdiListeGetirById(string listeId)
        {
            string query = "select * from OdiListeView where OdiListeId =@OdiListeId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<OdiListeOutputDTO>(query, new { OdiListeId = listeId });
            return result.FirstOrDefault();
        }
        public async Task<List<OdiListeDetay>> OdiListeDetayListesi(string odiListeId)
        {
            return await _dbContext.OdiListeDetay.Where(x => x.OdiListeId == odiListeId).ToListAsync();
        }

        public async Task<bool> OdiListeSil(string odiListeId)
        {
            OdiListe list = await _dbContext.OdiListeleri.FirstOrDefaultAsync(x => x.Id == odiListeId);
            _dbContext.OdiListeleri.Remove(list);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> OdiListeDetaySil(List<string> odiListeDetayIdList)
        {
            List<OdiListeDetay> list = await _dbContext.OdiListeDetay.Where(x => odiListeDetayIdList.Contains(x.Id)).ToListAsync();
            _dbContext.OdiListeDetay.RemoveRange(list);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckOdiListeDetay(string odiListeId, string odiTalepId)
        {
            return await _dbContext.OdiListeDetay.AnyAsync(x => x.OdiListeId == odiListeId && x.OdiTalepId == odiTalepId);
        }
    }
}
