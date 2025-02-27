using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekTemsilcisiModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekTemsilcisiLogicServices;

public class YetenekTemsilcisiLogicService : IYetenekTemsilcisiLogicService
{
    private readonly IYetenekTemsilcisiDataService _yetenekTemsilcisiDataService;
    private readonly IMapper _mapper;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public YetenekTemsilcisiLogicService(IYetenekTemsilcisiDataService yetenekTemsilcisiDataService, IMapper mapper, IAmazonS3Service amazonS3Service, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _yetenekTemsilcisiDataService = yetenekTemsilcisiDataService;
        _mapper = mapper;
        _amazonS3Service = amazonS3Service;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<OdiResponse<PagedData<KullaniciBilgileriDTO>>> PerformerListesi(PerformerListesiInputDTO model, string jwt)
    {
        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = "";

        List<string>? aktifKullaniciIdList = null;

        if (model.PerformerListelemeTipi == PerformerListelemeTipi.OnaylananPasif)
        {
            url = _configuration.GetSection("IdentityServerUrl").Value + "/api/user/AktifPerformerIdListesi";

            OdiResponse<List<string>> aktifPerformerIdListesiApiResult = await webApiRequest.Get<List<string>>(url, jwt);

            if (!aktifPerformerIdListesiApiResult.IsSuccessful)
            {
                return OdiResponse<PagedData<KullaniciBilgileriDTO>>.Fail("Performer bilgileri getirilemedi.", aktifPerformerIdListesiApiResult.Errors, 400);
            }

            aktifKullaniciIdList = aktifPerformerIdListesiApiResult.Data;
        }

        PagedData<string> performerIdPagedList = await _yetenekTemsilcisiDataService.PerformerListesi(model, aktifKullaniciIdList);

        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";

        OdiResponse<List<KullaniciBilgileriDTO>> apiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdPagedList.DataList);

        if (!apiResult.IsSuccessful)
        {
            return OdiResponse<PagedData<KullaniciBilgileriDTO>>.Fail("Performer bilgileri getirilemedi.", apiResult.Errors, 400);
        }

        PagedData<KullaniciBilgileriDTO> result = new PagedData<KullaniciBilgileriDTO>();

        result.PageNo = performerIdPagedList.PageNo;
        result.PageCount = performerIdPagedList.PageCount;
        result.Records = performerIdPagedList.Records;
        result.RecordsPerPage = performerIdPagedList.RecordsPerPage;
        result.DataList = apiResult.Data;

        List<PerformerMenajerListItemOutputDTO> performerMenajerList = await _yetenekTemsilcisiDataService.PerformerMenajerListesiGetir(apiResult.Data.Select(x => x.Id).ToList());

        foreach (var item in result.DataList)
        {
            item.MenajerId = performerMenajerList.FirstOrDefault(x => x.PerformerId == item.Id)?.MenajerId;
            item.MenajerAdSoyad = performerMenajerList.FirstOrDefault(x => x.PerformerId == item.Id)?.MenajerAdSoyad;
            item.ProfilFotografi = string.IsNullOrEmpty(item.ProfilFotografiDosyaYolu) ? "" : _amazonS3Service.GetPreSignedUrl(item.ProfilFotografiDosyaYolu);
        }

        return OdiResponse<PagedData<KullaniciBilgileriDTO>>.Success("Performer listesi getirildi.", result, 200);
    }

    public async Task<OdiResponse<PerformerListesiSayilariOutputDTO>> PerformerListesiSayilari(MenajerIdDTO model, string jwt)
    {

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("IdentityServerUrl").Value + "/api/user/AktifPerformerIdListesi";

        OdiResponse<List<string>> aktifPerformerIdListesiApiResult = await webApiRequest.Get<List<string>>(url, jwt);

        if (!aktifPerformerIdListesiApiResult.IsSuccessful)
        {
            return OdiResponse<PerformerListesiSayilariOutputDTO>.Fail("Performer bilgileri getirilemedi.", aktifPerformerIdListesiApiResult.Errors, 400);
        }

        List<string>? aktifKullaniciIdList = aktifPerformerIdListesiApiResult.Data;

        PerformerListesiSayilariOutputDTO result = await _yetenekTemsilcisiDataService.PerformerListesiSayilari(model.MenajerId, aktifKullaniciIdList);

        return OdiResponse<PerformerListesiSayilariOutputDTO>.Success("Performer listesi getirildi.", result, 200);
    }

    public async Task<OdiResponse<bool>> PerformerYetenekTemsilcisiAtama(PerformerYetenekTemsilcisiAtamaDTO model, OdiUser user)
    {
        PerformerYetenekTemsilcisi performerYetenekTemsilcisi = _mapper.Map<PerformerYetenekTemsilcisi>(model);

        DateTime date = DateTime.Now;

        performerYetenekTemsilcisi.EklenmeTarihi = date;
        performerYetenekTemsilcisi.Ekleyen = user.AdSoyad;
        performerYetenekTemsilcisi.EkleyenId = user.Id;

        performerYetenekTemsilcisi.GuncellenmeTarihi = date;
        performerYetenekTemsilcisi.Guncelleyen = user.AdSoyad;
        performerYetenekTemsilcisi.GuncelleyenId = user.Id;

        await _yetenekTemsilcisiDataService.YeniPerformerYetenekTemsilcisi(performerYetenekTemsilcisi);

        return OdiResponse<bool>.Success("Performer yetenek temsilcisi kaydedildi.", true, 200);
    }

    public async Task<OdiResponse<PerformerMenajerListItemOutputDTO>> PerformerMenajerGetir(KullaniciIdDTO model)
    {
        PerformerMenajerListItemOutputDTO menajer = await _yetenekTemsilcisiDataService.PerformerMenajerGetir(model.KullaniciId);

        if (menajer != null)
        {
            return OdiResponse<PerformerMenajerListItemOutputDTO>.Success("Menajer getirildi.", menajer, 200);
        }
        else
        {
            return OdiResponse<PerformerMenajerListItemOutputDTO>.Fail("Menajer bulunamadı.", "", 400);
        }
    }

    public async Task<OdiResponse<List<PerformerMenajerListItemOutputDTO>>> PerformerMenajerListesiGetir(List<KullaniciIdDTO> model)
    {
        return OdiResponse<List<PerformerMenajerListItemOutputDTO>>.Success("Menajer bilgileri getirildi.", await _yetenekTemsilcisiDataService.PerformerMenajerListesiGetir(model.Select(s => s.KullaniciId).ToList()), 200);
    }

    public async Task<OdiResponse<List<MenajerPerformerListItemOutputDTO>>> MenajerPerformerListesiGetir(List<KullaniciIdDTO> model)
    {
        return OdiResponse<List<MenajerPerformerListItemOutputDTO>>.Success("Performer bilgileri getirildi.", await _yetenekTemsilcisiDataService.MenajerPerformerListesiGetir(model.Select(s => s.KullaniciId).ToList()), 200);
    }
}