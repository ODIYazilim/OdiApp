using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikUrunuDataServices;
using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikUrunDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikUrunuLogicServices;

public class PerformerAbonelikUrunuLogicService : IPerformerAbonelikUrunuLogicService
{
    private readonly IPerformerAbonelikUrunuDataService _performerAbonelikUrunuDataService;
    private readonly IMapper _mapper;

    public PerformerAbonelikUrunuLogicService(IPerformerAbonelikUrunuDataService performerAbonelikUrunuDataService, IMapper mapper)
    {
        _performerAbonelikUrunuDataService = performerAbonelikUrunuDataService;
        _mapper = mapper;
    }

    public async Task<OdiResponse<string>> YeniPerformerAbonelikUrunu(PerformerAbonelikUrunuCreateDTO model, OdiUser user)
    {
        PerformerAbonelikUrunu performerAbonelikUrunu = _mapper.Map<PerformerAbonelikUrunu>(model);

        DateTime date = DateTime.Now;

        performerAbonelikUrunu.Aktif = true;

        performerAbonelikUrunu.EklenmeTarihi = date;
        performerAbonelikUrunu.Ekleyen = user.AdSoyad;
        performerAbonelikUrunu.EkleyenId = user.Id;

        performerAbonelikUrunu.GuncellenmeTarihi = date;
        performerAbonelikUrunu.Guncelleyen = user.AdSoyad;
        performerAbonelikUrunu.GuncelleyenId = user.Id;

        performerAbonelikUrunu = await _performerAbonelikUrunuDataService.YeniPerformerAbonelikUrunu(performerAbonelikUrunu);

        return OdiResponse<string>.Success("Performer abonelik urunu oluşturuldu.", performerAbonelikUrunu.Id, 200);
    }

    public async Task<OdiResponse<bool>> PerformerAbonelikUrunuGuncelle(PerformerAbonelikUrunuUpdateDTO model, OdiUser user)
    {
        PerformerAbonelikUrunu performerAbonelikUrunu = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGetir(model.PerformerAbonelikUrunuId);

        if (performerAbonelikUrunu == null) return OdiResponse<bool>.Fail("Bu id ile kayıtlı performer abonelik ürünü bulunamadı.", "Not Found", 404);

        performerAbonelikUrunu = _mapper.Map(model, performerAbonelikUrunu);

        DateTime date = DateTime.Now;

        performerAbonelikUrunu.GuncellenmeTarihi = date;
        performerAbonelikUrunu.Guncelleyen = user.AdSoyad;
        performerAbonelikUrunu.GuncelleyenId = user.Id;

        await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGuncelle(performerAbonelikUrunu);

        return OdiResponse<bool>.Success("Abonelik ürünü güncellendi.", true, 200);
    }

    public async Task<OdiResponse<bool>> PerformerAbonelikUrunDurumGuncelle(PerformerAbonelikUrunuIdDTO model, OdiUser user)
    {
        PerformerAbonelikUrunu performerAbonelikUrunu = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGetir(model.PerformerAbonelikUrunuId);

        if (performerAbonelikUrunu == null) return OdiResponse<bool>.Fail("Bu id ile kayıtlı performer abonelik ürünü bulunamadı.", "Not Found", 404);


        DateTime date = DateTime.Now;

        performerAbonelikUrunu.Aktif = !performerAbonelikUrunu.Aktif;

        performerAbonelikUrunu.GuncellenmeTarihi = date;
        performerAbonelikUrunu.Guncelleyen = user.AdSoyad;
        performerAbonelikUrunu.GuncelleyenId = user.Id;

        await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGuncelle(performerAbonelikUrunu);

        return OdiResponse<bool>.Success("Abonelik ürünü aktif durumu güncellendi.", true, 200);
    }

    public async Task<OdiResponse<List<PerformerAbonelikUrunuOutputDTO>>> PerformerAbonelikUrunuListele()
    {
        List<PerformerAbonelikUrunu> performerAbonelikUrunuList = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunListesiGetir();

        return OdiResponse<List<PerformerAbonelikUrunuOutputDTO>>.Success("Performer abonelik ürünleri getirildi.", _mapper.Map<List<PerformerAbonelikUrunuOutputDTO>>(performerAbonelikUrunuList), 200);
    }

    public async Task<OdiResponse<string>> PerformerAbonelikUrunuIsimGetir(AbonelikUrunuIdDTO model)
    {
        PerformerAbonelikUrunu performerAbonelikUrunu = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGetir(model.AbonelikUrunuId);

        return OdiResponse<string>.Success("Performer abonelik ürün adı getirildi.", performerAbonelikUrunu.UrunAdi, 200);
    }
}