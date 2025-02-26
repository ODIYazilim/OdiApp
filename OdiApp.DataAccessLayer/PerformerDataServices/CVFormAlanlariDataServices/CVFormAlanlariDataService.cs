using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;
using OdiApp.EntityLayer.PerformerModels.KisiselOzellikler;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;
using OdiApp.EntityLayer.PerformerModels.SesRengiModels;
using System.Data;

namespace OdiApp.DataAccessLayer.PerformerDataServices.CVFormAlanlariDataServices;

public class CVFormAlanlariDataService : ICVFormAlanlariDataService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public CVFormAlanlariDataService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<List<CVFormAlanlariDTO>> CVFormAlanlariGetir(List<string> kayitTuruList, int dilId)
    {
        using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        string kayitTuruKoduListesiString = string.Join(",", kayitTuruList);

        var parameters = new
        {
            DilId = dilId
        };

        var formAlanlari = await connection.QueryAsync<CVFormAlanlariDTO>("GetFormAlanlariView", parameters, commandType: CommandType.StoredProcedure);

        //List<CVFormAlanlariDTO> list = formAlanlari.OrderBy(x => x.Sira).ToList();
        List<CVFormAlanlariDTO> kayitTurunaGoreListe = new List<CVFormAlanlariDTO>();
        var filteredList = formAlanlari
          .Where(x => kayitTuruKoduListesiString.Contains(x.KayitTuruKodu))
          .GroupBy(x => x.AlanKodu)
          .Select(g => g.First())
          .OrderBy(x => x.Sira)
          .ToList();

        foreach (var item in filteredList)
        {
            if (item.DataType != "Select")
            {
                continue;
            }

            if (item.AlanTipi == "Kişisel Bilgiler")
            {
                List<CVFormAlanlariSelectModel> list = new List<CVFormAlanlariSelectModel>();
                List<KisiselOzellik> koList = await _dbContext.KisiselOzellikler.Where(x => x.KisiselOzellikKodu == item.AlanKodu && x.DilId == dilId).ToListAsync();
                foreach (var ko in koList)
                {
                    CVFormAlanlariSelectModel slcMdl = new CVFormAlanlariSelectModel { OzellikAdi = ko.KisiselOzellikAdi, OzellikKodu = ko.KisiselOzellikKodu, Sira = ko.Sira };
                    list.Add(slcMdl);
                }
                item.SelectModel = list;
            }
            if (item.AlanTipi == "Fiziksel Özellikler")
            {
                List<CVFormAlanlariSelectModel> list = new List<CVFormAlanlariSelectModel>();
                List<FizikselOzellik> foList = await _dbContext.FizikselOzellikler.Where(x => x.FizikselOzellikTipKodu == item.AlanKodu && x.DilId == dilId).ToListAsync();
                foreach (var fo in foList)
                {
                    CVFormAlanlariSelectModel slcMdl = new CVFormAlanlariSelectModel { OzellikAdi = fo.FizikselOzellikAdi, OzellikKodu = fo.FizikselOzellikKodu, Sira = fo.Sira };
                    list.Add(slcMdl);
                }
                item.SelectModel = list;
            }
            if (item.AlanTipi == "Ses Bilgileri" && item.AlanKodu == "SESR")
            {
                List<CVFormAlanlariSelectModel> list = new List<CVFormAlanlariSelectModel>();
                List<SesRengi> sesrList = await _dbContext.SesRenkleri.Where(x => x.SesRengiKodu == item.AlanKodu && x.DilId == dilId).ToListAsync();
                foreach (var sesr in sesrList)
                {
                    CVFormAlanlariSelectModel slcMdl = new CVFormAlanlariSelectModel { OzellikAdi = sesr.SesRengiAdi, OzellikKodu = sesr.SesRengiKodu, Sira = sesr.Sira };
                    list.Add(slcMdl);
                }
                item.SelectModel = list;
            }
            if (item.AlanTipi == "Ses Bilgileri" && item.AlanKodu == "SESD")
            {
                List<CVFormAlanlariSelectModel> list = new List<CVFormAlanlariSelectModel>();
                List<SeslendirmeDili> sesdList = await _dbContext.SeslendirmeDilleri.Where(x => x.SeslendirmeDiliKodu == item.AlanKodu && x.DilId == dilId).ToListAsync();
                foreach (var sesd in sesdList)
                {
                    CVFormAlanlariSelectModel slcMdl = new CVFormAlanlariSelectModel { OzellikAdi = sesd.SeslendirmeDiliAdi, OzellikKodu = sesd.SeslendirmeDiliKodu, Sira = sesd.Sira };
                    list.Add(slcMdl);
                }
                item.SelectModel = list;
            }
        }

        return filteredList;
    }

    public async Task<List<CVFormAlani>> CVFormAlanlariListesiGetir(List<string> kayitTuruKodlari, List<string> alanKodlari)
    {
        return await _dbContext.CVFormAlanlari
            .Where(x => kayitTuruKodlari.Contains(x.KayitTuruKodu) && alanKodlari.Contains(x.AlanKodu))
            .ToListAsync();
    }

    public async Task<List<CVFormAlani>> CVFormAlanlariListesiGetirByAlanKodlari(List<string> alanKodlari)
    {
        return await _dbContext.CVFormAlanlari
            .Where(x => alanKodlari.Contains(x.AlanKodu))
            .GroupBy(x => x.AlanKodu)
            .Select(x => x.First())
            .ToListAsync();
    }

    public async Task<List<CVFormAlani>> TumCVFormAlanlariListesiGetir()
    {
        return await _dbContext.CVFormAlanlari
          .GroupBy(x => x.AlanKodu)
          .Select(x => x.First())
          .ToListAsync();
    }
}