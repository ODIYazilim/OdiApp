using AutoMapper;
using OdiApp.EntityLayer.Base;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerPuanDataServices;
using OdiApp.DTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerPuanModels;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerPuanLogicServices;

public class PerformerPuanLogicService : IPerformerPuanLogicService
{
    private readonly IMapper _mapper;
    private readonly IPerformerPuanDataService _performerPuanDataService;
    private readonly IKullaniciBasicDataService _kullaniciBasicDataService;

    public PerformerPuanLogicService(IMapper mapper, IPerformerPuanDataService performerPuanDataService, IKullaniciBasicDataService kullaniciBasicDataService)
    {
        _mapper = mapper;
        _performerPuanDataService = performerPuanDataService;
        _kullaniciBasicDataService = kullaniciBasicDataService;
    }

    #region Admin

    public async Task<OdiResponse<List<PerformerPuanListOutputDTO>>> AdminOyVerenOyListesiGetir(OyverenOyListesiGetirInputDTO model)
    {
        List<PerformerPuan>? performerPuanList = await _performerPuanDataService.PerformerPuanListesiGetirByOyVeren(model.OyVerenId);
        List<PerformerPuanListOutputDTO>? outputDtoList = null;

        if (performerPuanList?.Any() == true)
        {
            outputDtoList = _mapper.Map<List<PerformerPuanListOutputDTO>>(performerPuanList);

            foreach (var item in outputDtoList)
            {
                KullaniciBasic oyveren = await _kullaniciBasicDataService.KullaniciGetir(item.OyVerenId);
                item.OyVerenAdSoyad = oyveren.KullaniciAdSoyad;

                KullaniciBasic performer = await _kullaniciBasicDataService.KullaniciGetir(item.PerformerId);
                item.PerformerAdSoyad = performer.KullaniciAdSoyad;
            }
        }

        return OdiResponse<List<PerformerPuanListOutputDTO>?>.Success("Oy veren puan listesi getirildi.", outputDtoList, 200);
    }

    public async Task<OdiResponse<List<PerformerPuanListOutputDTO>>> AdminPerformerOyListesiGetir(PerformerOyListesiGetirInputDTO model)
    {
        List<PerformerPuan>? performerPuanList = await _performerPuanDataService.PerformerPuanListesiGetirByPerformer(model.PerformerId, model.OyVerenKayitGrubu, model.OyVerenKayitTuru);
        List<PerformerPuanListOutputDTO>? outputDtoList = null;

        if (performerPuanList?.Any() == true)
        {
            outputDtoList = _mapper.Map<List<PerformerPuanListOutputDTO>>(performerPuanList);

            foreach (var item in outputDtoList)
            {
                KullaniciBasic oyveren = await _kullaniciBasicDataService.KullaniciGetir(item.OyVerenId);
                item.OyVerenAdSoyad = oyveren.KullaniciAdSoyad;

                KullaniciBasic performer = await _kullaniciBasicDataService.KullaniciGetir(item.PerformerId);
                item.PerformerAdSoyad = performer.KullaniciAdSoyad;
            }
        }

        return OdiResponse<List<PerformerPuanListOutputDTO>?>.Success("Performer puan listesi getirildi.", outputDtoList, 200);
    }

    #endregion

    public async Task<OdiResponse<bool>> PerformerIcinPuanVer(PerformerIcinPuanVerInputDTO model, OdiUser user)
    {
        PerformerPuan performerPuan = _mapper.Map<PerformerPuan>(model);

        DateTime date = DateTime.Now;

        performerPuan.EklenmeTarihi = date;
        performerPuan.Ekleyen = user.AdSoyad;
        performerPuan.EkleyenId = user.Id;

        performerPuan.GuncellenmeTarihi = date;
        performerPuan.Guncelleyen = user.AdSoyad;
        performerPuan.GuncelleyenId = user.Id;

        await _performerPuanDataService.YeniPerformerPuan(performerPuan);

        return OdiResponse<bool>.Success("Performer puanı başarılı bir şekilde kayıt edildi.", true, 200);
    }

    public async Task<OdiResponse<PerformerPuanOutputDTO>> PerformerPuanGetir(PerformerIdDTO model)
    {
        var result = await _performerPuanDataService.PerformerPuanGetirByPerformerId(model.PerformerId);

        if (result == null)
        {
            return OdiResponse<PerformerPuanOutputDTO>.Fail("Performer bulunamadı.", "", 404);
        }

        return OdiResponse<PerformerPuanOutputDTO>.Success("Performer puan getirildi.", result, 200);
    }

    // Liste halinde PerformerId'ler için puan getir
    public async Task<OdiResponse<List<PerformerPuanOutputDTO>>> PerformerListesiPuanGetir(List<PerformerIdDTO> model)
    {
        var performerIds = model.Select(x => x.PerformerId).ToList();

        var result = await _performerPuanDataService.PerformerListesiPuanGetir(performerIds);

        if (result == null || !result.Any())
        {
            return OdiResponse<List<PerformerPuanOutputDTO>>.Fail("Performers bulunamadı.", "", 404);
        }

        return OdiResponse<List<PerformerPuanOutputDTO>>.Success("Performer puan listesi getirildi.", result, 200);
    }
}