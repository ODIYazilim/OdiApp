using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.IslemlerDTOs.CallbackIslemler;
using OdiApp.EntityLayer.IslemlerModels.CallbackIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.CallbackIslemler
{
    public class CallbackDataService : ICallbackDataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public CallbackDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<CallbackAyarlari> YeniCallbackAyarlari(CallbackAyarlari ayarlar)
        {
            await _dbContext.CallbackAyarlari.AddAsync(ayarlar);

            return ayarlar;
        }

        public async Task<CallbackSaat> CallbackSaatiGetir(string callbackSaatId)
        {
            return await _dbContext.CallbackSaatleri.FirstOrDefaultAsync(x => x.Id == callbackSaatId);
        }

        public async Task<CallbackSaat> CallbackSaatGuncelle(CallbackSaat saat)
        {
            _dbContext.CallbackSaatleri.Update(saat);

            return saat;
        }

        public async Task<List<CallbackNot>> YeniCallbackNotlari(List<CallbackNot> notlar)
        {
            await _dbContext.CallbackNotlari.AddRangeAsync(notlar);

            return notlar;
        }

        public async Task<List<CallbackSenaryo>> YeniCallbackSenaryolari(List<CallbackSenaryo> senaryolar)
        {
            await _dbContext.CallbackSenaryolari.AddRangeAsync(senaryolar);

            return senaryolar;
        }

        public async Task<List<CallbackSaat>> YeniCallbackTakvimi(List<CallbackSaat> takvimTarihSaatleri)
        {
            await _dbContext.CallbackSaatleri.AddRangeAsync(takvimTarihSaatleri);

            return takvimTarihSaatleri;
        }


        public async Task<List<CallbackSaat>> CallbackTakvimiGetir(string projeId)
        {

            return await _dbContext.CallbackSaatleri.AsNoTracking().Where(x => x.ProjeId == projeId).ToListAsync();
        }
        public async Task<bool> CheckCallbackAyarlari(string projeId)
        {
            return await _dbContext.CallbackAyarlari.AnyAsync(x => x.ProjeId == projeId);
        }

        public async Task SaveChangesAsync()
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {

                    await transaction.RollbackAsync();
                    throw; // Hatanın dışarıya fırlatılması
                }
            }

        }

        public async Task DeleteCallbackAyarlariTakvim()
        {
            string query = @"truncate table CallbackAyarlari
                            truncate table CallbackNotlari
                            truncate table CallbackSenaryolari
                            truncate table CallBackSaatleri";

            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.ExecuteAsync(query);

        }

        public async Task<List<CallbackTakvimSaatleriOutputDTO>> CallbackTakvimSaatGetir(string projeId)
        {
            var callbackTakvimList = await _dbContext.CallbackSaatleri
         .Where(ct => ct.ProjeId == projeId) // Proje Id'ye göre filtreleme
         .GroupBy(ct => ct.TarihSaat.Date) // TarihSaat alanına göre grupla
         .Select(group => new CallbackTakvimSaatleriOutputDTO
         {
             Tarih = group.Key, // Grup anahtarını doğrudan tarih olarak atayabiliriz
             TarihLabel = GetTarihLabel(group.Key),
             CallbackSaatleri = group.Select(ct => new CallbackSaatOutputDTO
             {
                 CallbackSaatId = ct.Id,
                 ProjeId = ct.ProjeId,
                 TarihSaat = ct.TarihSaat,
                 Dolu = ct.Dolu,
                 Not = ct.Not,
                 Kilitli = ct.Kilitli
             }).ToList()
         })
         .ToListAsync();

            return callbackTakvimList;


        }
        private static string GetTarihLabel(DateTime tarih)
        {
            // Tarihi belirtilen formatta biçimlendirme
            string formattedDate = tarih.ToString("dd MMMM yyyy - dddd");

            return formattedDate;
        }

        public async Task CallbackSaatleriKilitle(List<string> saatIdleri)
        {
            string query = "Update CallbackSaatleri set Kilitli=1 where Id in @Ids ";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync(query, new { Ids = saatIdleri.ToArray() });

        }
        public async Task CallbackSaatleriKilidiAc(List<string> saatIdleri)
        {
            string query = "Update CallbackSaatleri set Kilitli=0 where Id in @Ids ";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync(query, new { Ids = saatIdleri.ToArray() });
        }
        public CallbackAyarlari CallbackAyarlariGuncelle(CallbackAyarlari ayarlar)
        {
            _dbContext.CallbackAyarlari.Update(ayarlar);
            return ayarlar;
        }

        public async Task<CallbackAyarlari> CallbackAyarlariGetir(string projeId)
        {
            return await _dbContext.CallbackAyarlari.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeId == projeId);
        }

        public async Task<List<CallbackGonderilecekPerformerOutput>> CallbackGonderilecekPerformerListesi(string projeId)
        {
            string query = "Select * from CallbackKabulEdilenOpsiyonView where ProjeId=@ProjeId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<CallbackGonderilecekPerformerOutput>(query, new { ProjeId = projeId });
            return result.ToList();
        }

        public async Task<List<Callback>> YeniCallback(List<Callback> callbackList)
        {
            await _dbContext.Callback.AddRangeAsync(callbackList);

            return callbackList;
        }
        public async Task<Callback> CallbackGuncelle(Callback callback)
        {
            _dbContext.Callback.Update(callback);
            await SaveChangesAsync();
            return callback;
        }
        public async Task<List<CallbackOutputDTO>> YapimCallbackListesiGetir(string projeId)
        {
            string query = "Select * from CallbackView where ProjeId=@ProjeId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<CallbackOutputDTO>(query, new { ProjeId = projeId });
            return result.ToList();

        }

        public async Task<List<CallbackOutputDTO>> MenajerCallbackListesiGetir(string menajerId)
        {
            string query = "Select * from CallbackView where MenajerId=@MenajerId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<CallbackOutputDTO>(query, new { MenajerId = menajerId });
            return result.ToList();
        }

        public async Task<CallbackOutputDTO> CallbackOutputGetir(string callbackId)
        {
            string query = "Select * from CallbackView where CallbackId=@Id";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryFirstOrDefaultAsync<CallbackOutputDTO>(query, new { Id = callbackId });
            return result;
        }
        public async Task<CallbackOutputDTO> CallbackOutputGetir(string projeId, string performerId)
        {
            string query = "Select * from CallbackView where ProjeId=@ProjeId and PerformerId=@PerformerId";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryFirstOrDefaultAsync<CallbackOutputDTO>(query, new { ProjeId = projeId, PerformerId = performerId });
            return result;
        }
        public Task<Callback> CallbackGetir(string callbackId)
        {
            return _dbContext.Callback.AsNoTracking().FirstOrDefaultAsync(x => x.Id == callbackId);
        }

        public async Task<bool> CallbackGonderilmismi(string performerId, string projeId)
        {
            return await _dbContext.Callback.AnyAsync(x => x.ProjeId == projeId && x.PerformerId == performerId);
        }

        public async Task<Callback> CallbackGetir(string projeId, string performerId)
        {
            return await _dbContext.Callback.FirstOrDefaultAsync(x => x.ProjeId == projeId && x.PerformerId == performerId);
        }

        public async Task<CallbackNot> CallbackNotGetir(string projeId, string rolId)
        {
            CallbackNot not = await _dbContext.CallbackNotlari.FirstOrDefaultAsync(x => x.ProjeId == projeId & x.RolId == rolId);
            if (not == null) not = await _dbContext.CallbackNotlari.FirstOrDefaultAsync(x => x.ProjeId == projeId);
            return not;
        }

        public async Task<CallbackSenaryo> CallbackSenaryoGetir(string projeId, string rolId)
        {
            CallbackSenaryo senaryo = await _dbContext.CallbackSenaryolari.FirstOrDefaultAsync(x => x.ProjeId == projeId & x.RolId == rolId);
            if (senaryo == null) senaryo = await _dbContext.CallbackSenaryolari.FirstOrDefaultAsync(x => x.ProjeId == projeId);
            return senaryo;
        }


    }
}