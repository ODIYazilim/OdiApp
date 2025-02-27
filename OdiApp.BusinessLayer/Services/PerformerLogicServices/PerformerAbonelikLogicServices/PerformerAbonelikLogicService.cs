using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikUrunuDataServices;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikLogicServices;

public class PerformerAbonelikLogicService : IPerformerAbonelikLogicService
{
    private readonly IPerformerAbonelikDataService _performerAbonelikDataService;
    private readonly IPerformerAbonelikUrunuDataService _performerAbonelikUrunuDataService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public PerformerAbonelikLogicService(IPerformerAbonelikDataService performerAbonelikDataService, IMapper mapper, IPerformerAbonelikUrunuDataService performerAbonelikUrunuDataService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _performerAbonelikDataService = performerAbonelikDataService;
        _mapper = mapper;
        _performerAbonelikUrunuDataService = performerAbonelikUrunuDataService;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<OdiResponse<PerformerAbonelik>> PerformerAbonelikBitisTarihiGuncelle(PerformerAbonelikTarihDTO model, OdiUser user)
    {
        PerformerAbonelik performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGetirById(model.PerformerAbonelikId);

        DateTime bitisTarihi = performerAbonelik.AbonelikBitisTarihi;
        DateTime uzatilmisTarih = model.UzatilmisBitisTarih;

        performerAbonelik.AbonelikBitisTarihi = uzatilmisTarih;
        performerAbonelik.AbonelikVeren = model.AbonelikVeren;

        DateTime date = DateTime.Now;

        performerAbonelik.GuncellenmeTarihi = date;
        performerAbonelik.Guncelleyen = user.AdSoyad;
        performerAbonelik.GuncelleyenId = user.Id;

        performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGuncelle(performerAbonelik);

        PerformerAbonelikSureUzatma performerAbonelikSureUzatma = new PerformerAbonelikSureUzatma();

        performerAbonelikSureUzatma.PerformerAbonelikId = model.PerformerAbonelikId;
        performerAbonelikSureUzatma.BitisTarihi = bitisTarihi;
        performerAbonelikSureUzatma.UzatilmisBitisTarihi = uzatilmisTarih;
        performerAbonelikSureUzatma.UzatmaSebebi = model.AbonelikUzatmaSebebi;

        performerAbonelikSureUzatma.EklenmeTarihi = date;
        performerAbonelikSureUzatma.Ekleyen = user.AdSoyad;
        performerAbonelikSureUzatma.EkleyenId = user.Id;

        performerAbonelikSureUzatma.GuncellenmeTarihi = date;
        performerAbonelikSureUzatma.Guncelleyen = user.AdSoyad;
        performerAbonelikSureUzatma.GuncelleyenId = user.Id;

        await _performerAbonelikDataService.YeniPerformerAbonelikSureUzatma(performerAbonelikSureUzatma);

        return OdiResponse<PerformerAbonelik>.Success("Performer abonelik bitiş tarihi güncellendi.", performerAbonelik, 200);
    }

    public async Task<OdiResponse<string>> PerformerAbonelikOlustur(PerformerAbonelikCreateDTO model, OdiUser user)
    {
        bool kontrol = await PerformerAbonelikKayitKontrolu(new PAKayitEdilebilirmiDTO() { PerformerAbonelikUrunuId = model.AbonelikUrunId, PerformerId = model.PerformerId });

        DateTime date = DateTime.Now;

        if (kontrol)
        {
            PerformerAbonelik performerAbonelik = _mapper.Map<PerformerAbonelik>(model);

            performerAbonelik.EklenmeTarihi = date;
            performerAbonelik.Ekleyen = user.AdSoyad;
            performerAbonelik.EkleyenId = user.Id;

            performerAbonelik.GuncellenmeTarihi = date;
            performerAbonelik.Guncelleyen = user.AdSoyad;
            performerAbonelik.GuncelleyenId = user.Id;

            await _performerAbonelikDataService.YeniPerformerAbonelik(performerAbonelik);

            return OdiResponse<string>.Success("Performer abonelik olusturuldu.", performerAbonelik.Id, 200);
        }
        else
        {
            if (model.SureUzat)
            {
                PerformerAbonelikUrunu performerAbonelikUrunu = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGetir(model.AbonelikUrunId);

                if (performerAbonelikUrunu == null) return OdiResponse<string>.Fail("Performer abonelik oluşturulması yapılamadı.", "Performer abonelik ürünü bulunamadı", 404);

                PerformerAbonelik performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGetirByPerformerId(model.PerformerId);

                DateTime bitisTarihi = performerAbonelik.AbonelikBitisTarihi;
                DateTime uzatilmisTarih = performerAbonelik.AbonelikBitisTarihi.AddMonths(performerAbonelikUrunu.OdemePeriodu);

                performerAbonelik.AbonelikBitisTarihi = uzatilmisTarih;

                performerAbonelik.GuncellenmeTarihi = date;
                performerAbonelik.Guncelleyen = user.AdSoyad;
                performerAbonelik.GuncelleyenId = user.Id;

                performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGuncelle(performerAbonelik);

                PerformerAbonelikSureUzatma performerAbonelikSureUzatma = new PerformerAbonelikSureUzatma();

                performerAbonelikSureUzatma.PerformerAbonelikId = performerAbonelik.Id;
                performerAbonelikSureUzatma.BitisTarihi = bitisTarihi;
                performerAbonelikSureUzatma.UzatilmisBitisTarihi = uzatilmisTarih;
                performerAbonelikSureUzatma.UzatmaSebebi = model.SureUzatmaSebebi ?? string.Empty;

                performerAbonelikSureUzatma.EklenmeTarihi = date;
                performerAbonelikSureUzatma.Ekleyen = user.AdSoyad;
                performerAbonelikSureUzatma.EkleyenId = user.Id;

                performerAbonelikSureUzatma.GuncellenmeTarihi = date;
                performerAbonelikSureUzatma.Guncelleyen = user.AdSoyad;
                performerAbonelikSureUzatma.GuncelleyenId = user.Id;

                await _performerAbonelikDataService.YeniPerformerAbonelikSureUzatma(performerAbonelikSureUzatma);

                return OdiResponse<string>.Success("Performer abonelik olusturuldu.", performerAbonelik.Id, 200);
            }
            else
            {
                return OdiResponse<string>.Fail("Performer abonelik oluşturulması yapılamadı.", "", 400);
            }
        }
    }

    public async Task<OdiResponse<AboneligiSonlandirOutputDTO>> PerformerAbonelikBitir(AboneligiSonlandirInputDTO model, OdiUser user)
    {
        PerformerAbonelik performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGetirByPerformerId(model.KullaniciId);

        DateTime date = DateTime.Now;

        performerAbonelik.Aktif = false;
        performerAbonelik.SonlanmaSebebi = (int)PerformerAbonelikSonlanmaSebepleri.PerformerIptalEtti;
        performerAbonelik.UcretsizYenile = false;
        performerAbonelik.Yenile = false;
        performerAbonelik.AbonelikIptalTarihi = date;

        performerAbonelik.GuncellenmeTarihi = date;
        performerAbonelik.Guncelleyen = user.AdSoyad;
        performerAbonelik.GuncelleyenId = user.Id;

        performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGuncelle(performerAbonelik);

        return OdiResponse<AboneligiSonlandirOutputDTO>.Success("Performer abonelik bitti.", new AboneligiSonlandirOutputDTO() { AbonelikBitisTarihi = performerAbonelik.AbonelikBitisTarihi }, 200);
    }

    public async Task<OdiResponse<PerformerAbonelikOzetiGetirOutputDTO>> PerformerAbonelikOzetiGetir(KullaniciIdDTO model, string jwtToken)
    {
        PerformerAbonelikOzetiGetirOutputDTO result = null;

        PerformerAbonelik performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGetirByPerformerId(model.KullaniciId);

        if (performerAbonelik != null)
        {
            PerformerAbonelikUrunu performerAbonelikUrunu = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGetir(performerAbonelik.AbonelikUrunId);

            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/odemeler/abonelik-kart-bilgisi-getir";

            AbonelikReferenceCodeDTO requestModel = new AbonelikReferenceCodeDTO();

            requestModel.AbonelikReferenceCode = performerAbonelik.AbonelikReferenceCode;

            OdiResponse<AbonelikKartBilgisiGetirOutputDTO> apiResult = await webApiRequest.Post<AbonelikKartBilgisiGetirOutputDTO, AbonelikReferenceCodeDTO>(url, jwtToken, requestModel);

            if (apiResult.IsSuccessful)
            {
                result = new PerformerAbonelikOzetiGetirOutputDTO();

                result.FotografSayisi = performerAbonelikUrunu.FotografSayisi;
                result.TanitimVideosuSayisi = performerAbonelikUrunu.TanitimVideosuSayisi;
                result.ShowreelSayisi = performerAbonelikUrunu.ShowreelSayisi;
                result.PerformansVideosuSayisi = performerAbonelikUrunu.PerformansVideosuSayisi;
                result.KalanFotografSayisi = performerAbonelik.KalanFotografSayisi;
                result.KalanTanitimVideosuSayisi = performerAbonelik.KalanTanitimVideosuSayisi;
                result.KalanShowreelSayisi = performerAbonelik.KalanShowreelSayisi;
                result.KalanPerformerVideosuSayisi = performerAbonelik.KalanPerformerVideosuSayisi;
                result.AbonelikBitisTarihi = performerAbonelik.AbonelikBitisTarihi;
                result.AbonelikBaslangicTarihi = performerAbonelik.AbonelikBaslangicTarihi;
                result.OdemePeriodu = performerAbonelikUrunu.OdemePeriodu;
                result.UrunAdi = performerAbonelikUrunu.UrunAdi;
                result.KartBinNumber = apiResult.Data.KartBinNumber;
                result.KartLastFourDigit = apiResult.Data.KartLastFourDigit;
                result.AbonelikReferenceCode = performerAbonelik.AbonelikReferenceCode;
                result.KullaniciAbonelikId = performerAbonelik.Id;
                result.KullaniciAbonelikUrunId = performerAbonelik.AbonelikUrunId;
                result.KullaniciReferanceCode = performerAbonelik.KullaniciReferenceCode;
                result.PlanReferanceCode = performerAbonelikUrunu.ReferenceCode;
            }
            else
            {
                return OdiResponse<PerformerAbonelikOzetiGetirOutputDTO>.Fail("Performer abonelik özeti getirme başarısız.", apiResult.Errors, 400);
            }
        }

        return OdiResponse<PerformerAbonelikOzetiGetirOutputDTO>.Success("Performer abonelik özeti getirildi.", result, 200);
    }

    public async Task<OdiResponse<bool>> PerformerAbonelikYenilemeyiKapat(AbonelikIdDTO model, OdiUser user)
    {
        PerformerAbonelik performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGetirById(model.AbonelikId);

        performerAbonelik.UcretsizYenile = false;
        performerAbonelik.Yenile = false;

        performerAbonelik.GuncellenmeTarihi = DateTime.Now;
        performerAbonelik.Guncelleyen = user.AdSoyad;
        performerAbonelik.GuncelleyenId = user.Id;

        performerAbonelik = await _performerAbonelikDataService.PerformerAbonelikGuncelle(performerAbonelik);

        return OdiResponse<bool>.Success("Abonelik yenileme kapatıldı.", true, 200);
    }

    private async Task<bool> PerformerAbonelikKayitKontrolu(PAKayitEdilebilirmiDTO model)
    {
        return await _performerAbonelikDataService.PerformerAbonelikKayitKontrolu(model.PerformerAbonelikUrunuId, model.PerformerId);
    }
}