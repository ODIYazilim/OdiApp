using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;
using System.Data;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolBilgileri
{
    public class ProjeRolDataService : IProjeRolDataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProjeRolDataService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<List<RolOzellikPerformerEtiket>> PerformerEtiketleriGetir(string projeRolOzellikId)
        {
            return await _dbContext.RolOzellikPerformerEtiketler
                .Where(x => x.ProjeRolOzellikId == projeRolOzellikId)
                .ToListAsync();
        }

        public async Task<bool> PerformerEtiketleriEkle(List<RolOzellikPerformerEtiket> performerEtiketler)
        {
            await _dbContext.RolOzellikPerformerEtiketler.AddRangeAsync(performerEtiketler);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PerformerEtiketleriSil(List<RolOzellikPerformerEtiket> performerEtiketler)
        {
            _dbContext.RolOzellikPerformerEtiketler.RemoveRange(performerEtiketler);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<RolOzellikFiziksel>> FizikselOzellikleriGetir(string projeRolOzellikId)
        {
            return await _dbContext.RolOzellikFizikseller
                .Where(x => x.ProjeRolOzellikId == projeRolOzellikId)
                .ToListAsync();
        }

        public async Task<bool> FizikselOzellikleriEkle(List<RolOzellikFiziksel> fizikselOzellikler)
        {
            await _dbContext.RolOzellikFizikseller.AddRangeAsync(fizikselOzellikler);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FizikselOzellikleriSil(List<RolOzellikFiziksel> fizikselOzellikler)
        {
            _dbContext.RolOzellikFizikseller.RemoveRange(fizikselOzellikler);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<RolOzellikDeneyim>> DeneyimKodlariniGetir(string projeRolOzellikId)
        {
            return await _dbContext.RolOzellikDeneyimler
                .Where(x => x.ProjeRolOzellikId == projeRolOzellikId)
                .ToListAsync();
        }

        public async Task<bool> DeneyimKodlariEkle(List<RolOzellikDeneyim> deneyimKodlari)
        {
            await _dbContext.RolOzellikDeneyimler.AddRangeAsync(deneyimKodlari);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeneyimKodlariSil(List<RolOzellikDeneyim> deneyimKodlari)
        {
            _dbContext.RolOzellikDeneyimler.RemoveRange(deneyimKodlari);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<RolOzellikEgitim>> EgitimTipleriniGetir(string projeRolOzellikId)
        {
            return await _dbContext.RolOzellikEgitimler
                .Where(x => x.ProjeRolOzellikId == projeRolOzellikId)
                .ToListAsync();
        }

        public async Task<bool> EgitimTipleriEkle(List<RolOzellikEgitim> egitimTipleri)
        {
            await _dbContext.RolOzellikEgitimler.AddRangeAsync(egitimTipleri);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EgitimTipleriSil(List<RolOzellikEgitim> egitimTipleri)
        {
            _dbContext.RolOzellikEgitimler.RemoveRange(egitimTipleri);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<RolOzellikYetenek>> YetenekTipleriniGetir(string projeRolOzellikId)
        {
            return await _dbContext.RolOzellikYetenekler
                .Where(x => x.ProjeRolOzellikId == projeRolOzellikId)
                .ToListAsync();
        }

        public async Task<bool> YetenekTipleriEkle(List<RolOzellikYetenek> yetenekTipleri)
        {
            await _dbContext.RolOzellikYetenekler.AddRangeAsync(yetenekTipleri);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> YetenekTipleriSil(List<RolOzellikYetenek> yetenekTipleri)
        {
            _dbContext.RolOzellikYetenekler.RemoveRange(yetenekTipleri);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProjeRolOzellik> ProjeRolOzellikGetir(string projeRolId)
        {
            return await _dbContext.ProjeRolOzellikleri
                .AsNoTracking()
                .Include(x => x.FizikselOzellikler)
                .Include(x => x.DeneyimKodlari)
                .Include(x => x.EgitimTipleri)
                .Include(x => x.YetenekTipleri)
                .Include(x => x.PerformerEtiketleri)
                .FirstOrDefaultAsync(x => x.ProjeRolId == projeRolId);
        }

        public async Task<List<ProjeRolOzellik>> ProjeRolOzellikListeGetir(string projeRolId)
        {
            return await _dbContext.ProjeRolOzellikleri
                .AsNoTracking()
                .Include(x => x.FizikselOzellikler)
                .Include(x => x.DeneyimKodlari)
                .Include(x => x.EgitimTipleri)
                .Include(x => x.YetenekTipleri)
                .Include(x => x.PerformerEtiketleri)
                .Where(x => x.ProjeRolId == projeRolId)
                .ToListAsync();
        }

        public async Task<bool> YeniProjeRolOzellik(ProjeRolOzellik ozellik)
        {
            await _dbContext.ProjeRolOzellikleri.AddAsync(ozellik);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> YeniProjeRolOzellik(List<ProjeRolOzellik> ozellikListesi)
        {
            await _dbContext.ProjeRolOzellikleri.AddRangeAsync(ozellikListesi);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ProjeRolOzellikGuncelle(ProjeRolOzellik ozellik)
        {
            _dbContext.ProjeRolOzellikleri.Update(ozellik);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ProjeRolOzellikSil(string projeRolId)
        {
            var entity = await _dbContext.ProjeRolOzellikleri
                .Include(x => x.FizikselOzellikler)
                .Include(x => x.DeneyimKodlari)
                .Include(x => x.EgitimTipleri)
                .Include(x => x.YetenekTipleri)
                .Include(x => x.PerformerEtiketleri)
                .FirstOrDefaultAsync(x => x.ProjeRolId == projeRolId);

            if (entity != null)
            {
                _dbContext.ProjeRolOzellikleri.Remove(entity);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public Task<ProjeRol> ProjeRolGetir(string projeRolId)
        {
            return _dbContext.ProjeRolleri.Include(x => x.RolOzellik).Include(x => x.AnketSorulari).Include(x => x.RolOzellik).AsNoTracking().FirstOrDefaultAsync(x => x.Id == projeRolId);
        }

        public Task<ProjeRol> ProjeRolFullGetir(string projeRolId)
        {
            return _dbContext.ProjeRolleri
                .Include(x => x.RolOzellik)
                    .ThenInclude(ro => ro.FizikselOzellikler)
                .Include(x => x.RolOzellik)
                    .ThenInclude(ro => ro.DeneyimKodlari)
                .Include(x => x.RolOzellik)
                    .ThenInclude(ro => ro.EgitimTipleri)
                .Include(x => x.RolOzellik)
                    .ThenInclude(ro => ro.YetenekTipleri)
                .Include(x => x.RolOzellik)
                    .ThenInclude(ro => ro.PerformerEtiketleri)
                .Include(x => x.AnketSorulari)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projeRolId);
        }

        public Task<List<ProjeRol>> ProjeRolleriGetir(string projeId)
        {
            return _dbContext.ProjeRolleri.Include(x => x.RolOzellik).Include(x => x.AnketSorulari).Include(x => x.RolOzellik).AsNoTracking().Where(x => x.ProjeId == projeId).ToListAsync();
        }

        public async Task<ProjeRol> YeniProjeRol(ProjeRol rol)
        {
            await _dbContext.ProjeRolleri.AddAsync(rol);
            await _dbContext.SaveChangesAsync();
            return await this.ProjeRolGetir(rol.Id);
        }

        public async Task<ProjeRol> ProjeRolGuncelle(ProjeRol rol)
        {
            _dbContext.ProjeRolleri.Update(rol);
            await _dbContext.SaveChangesAsync();
            return rol;
        }

        public async Task<ProjeRolPerformer> YeniProjeRolPerformer(ProjeRolPerformer model)
        {
            await _dbContext.ProjeRolPerformer.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<ProjeRolPerformerOutputDTO>> ProjeRolPerformerListGetir(string projeId)
        {
            string query = @"SELECT * FROM ProjeRolPerformerOutputView WHERE ProjeId = @ProjeId";
            var parameters = new { ProjeId = projeId };
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<ProjeRolPerformerOutputDTO>(query, parameters);
            return result.ToList();
        }

        public async Task<bool> ProjeRolAnketSorusuSil(List<ProjeRolAnketSorusu> anketSorusuListe)
        {
            _dbContext.ProjeRolAnketSorulari.RemoveRange(anketSorusuListe);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjeRolAnketSorusu>> YeniProjeRolAnketSorusu(List<ProjeRolAnketSorusu> anketSorusuListe)
        {
            await _dbContext.ProjeRolAnketSorulari.AddRangeAsync(anketSorusuListe);
            await _dbContext.SaveChangesAsync();
            return anketSorusuListe;
        }

        public async Task<List<ProjeRolAnketSorusu>> ProjeRolAnketSorusuListeGetir(List<string> idList)
        {
            return await _dbContext.ProjeRolAnketSorulari.Where(x => idList.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<ProjeRolAnketSorusu>> ProjeRolAnketSorusuListeGetir(string projeRolId)
        {
            return await _dbContext.ProjeRolAnketSorulari.Where(x => x.ProjeRolId == projeRolId).ToListAsync();
        }

        public async Task<List<ProjeRolAnketSorusu>> ProjeRolAnketSorusuGuncelle(List<ProjeRolAnketSorusu> anketSorusuListe)
        {
            _dbContext.ProjeRolAnketSorulari.UpdateRange(anketSorusuListe);
            await _dbContext.SaveChangesAsync();
            return anketSorusuListe;
        }

        public async Task<List<RolAgirlikTipi>> RolAgirlikTipiAktifListeGetir()
        {
            return await _dbContext.RolAgirlikTipleri.Where(x => x.Aktif).ToListAsync();
        }
    }
}