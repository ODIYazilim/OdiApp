using AutoMapper;
using OdiApp.BusinessLayer.Core;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler.Interfaces;
using OdiApp.DataAccessLayer.PerformerDataServices.FizikselOzellikler;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.FizikselOzellikler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler;

public class AdminFizikselOzellikLogicService : IAdminFizikselOzellikLogicService
{
    private readonly IMapper _mapper;
    private readonly IFizikselOzellikDataService _dataService;

    public AdminFizikselOzellikLogicService(IMapper mapper, IFizikselOzellikDataService dataService)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    public async Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiEkle(FizikselOzellikTipi tip, OdiUser user)
    {
        tip.EklenmeTarihi = DateTime.Now;
        tip.EkleyenId = user.Id;
        tip.Ekleyen = user.AdSoyad;

        tip.GuncellenmeTarihi = DateTime.Now;
        tip.GuncelleyenId = user.Id;
        tip.Guncelleyen = user.AdSoyad;

        await _dataService.FizikselOzellikTipiEkle(tip);

        return OdiResponse<List<FizikselOzellikTipi>>.Success("Fiziksel Özellik Tipi Eklendi", await _dataService.FizikselOzellikTipiListe(tip.DilId), 200);
    }

    public async Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiGuncelle(FizikselOzellikTipi tip, OdiUser user)
    {
        FizikselOzellikTipi tipp = await _dataService.FizikselOzellikTipiGetir(tip.Id);

        Fonksiyonlar.UpdateNonDefaultProperties(tip, tipp);

        tipp.GuncellenmeTarihi = DateTime.Now;
        tipp.GuncelleyenId = user.Id;
        tipp.Guncelleyen = user.AdSoyad;

        await _dataService.FizikselOzellikTipiGuncelle(tipp);

        return OdiResponse<List<FizikselOzellikTipi>>.Success("Fiziksel Özellik Tipi Guncellendi", await _dataService.FizikselOzellikTipiListe(tipp.DilId), 200);
    }

    public async Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiListe(DilIdDTO dilId)
    {
        return OdiResponse<List<FizikselOzellikTipi>>.Success("Fiziksel Özellik Tipleri Listelendi", await _dataService.FizikselOzellikTipiListe(dilId.DilId), 200);
    }

    public async Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiSil(FizikselOzellikTipiIdDTO id)
    {
        FizikselOzellikTipi tip = await _dataService.FizikselOzellikTipiGetir(id.FizikselOzellikTipiId);
        if (tip == null) return OdiResponse<List<FizikselOzellikTipi>>.Fail("Bu id ilefiziksel özellik tipi bulunamadı", "BadRequest", 400);
        List<FizikselOzellik> ozList = await _dataService.FizikselOzellikListesi(tip.FizikselOzellikTipKodu);
        if (ozList.Count > 0) return OdiResponse<List<FizikselOzellikTipi>>.Fail("Bu tipe bağlı özellikler olduğu için bu tipi silemezsiniz.", "BadRequest", 400);

        bool sonuc = await _dataService.FizikselOzellikTipiSil(tip);
        if (!sonuc) return OdiResponse<List<FizikselOzellikTipi>>.Fail("Tip Silinemedi.", "BadRequest", 400);
        return OdiResponse<List<FizikselOzellikTipi>>.Success("Fiziksel Özellik tipi silindi", await _dataService.FizikselOzellikTipiListe(tip.DilId), 200);
    }

    public async Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiDurumDegistir(FizikselOzellikTipiIdDTO id, OdiUser user)
    {
        FizikselOzellikTipi tip = await _dataService.FizikselOzellikTipiGetir(id.FizikselOzellikTipiId);

        if (tip.Durum) tip.Durum = false;
        else tip.Durum = true;

        tip.GuncellenmeTarihi = DateTime.Now;
        tip.Guncelleyen = user.AdSoyad;
        tip.GuncelleyenId = user.Id;

        await _dataService.FizikselOzellikTipiGuncelle(tip);
        return OdiResponse<List<FizikselOzellikTipi>>.Success("Fiziksel Özellik tipi silindi", await _dataService.FizikselOzellikTipiListe(tip.DilId), 200);
    }
}
