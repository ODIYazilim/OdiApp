using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikLogicServices;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikUrunuDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerMenajerSozlesmeDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.Enums.AbonelikEnums;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs.MenajerAbonelikDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs.GlobalDTOs.YetenekTemsilcisiAbonelikDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;
using OdiApp.EntityLayer.PerformerModels.PerformerMenajerModels;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerMenajerLogicServices;

public class PerformerMenajerLogicService : IPerformerMenajerLogicService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IPerformerAbonelikLogicService _performerAbonelikLogicService;
    private readonly IPerformerAbonelikUrunuDataService _performerAbonelikUrunuDataService;
    private readonly IPerformerMenajerSozlesmeDataService _performerMenajerSozlesmeDataService;
    private readonly IKullaniciBasicDataService _kullaniciBasicDataService;
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IYetenekTemsilcisiDataService _yetenekTemsilcisiDataService;

    public PerformerMenajerLogicService(IMapper mapper, IConfiguration configuration, IHttpClientFactory httpClientFactory, IPerformerAbonelikLogicService performerAbonelikLogicService, IPerformerAbonelikUrunuDataService performerAbonelikUrunuDataService, IPerformerMenajerSozlesmeDataService performerMenajerSozlesmeDataService, IKullaniciBasicDataService kullaniciBasicDataService, IAmazonS3Service amazonS3Service, IYetenekTemsilcisiDataService yetenekTemsilcisiDataService)
    {
        _mapper = mapper;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _performerAbonelikLogicService = performerAbonelikLogicService;
        _performerAbonelikUrunuDataService = performerAbonelikUrunuDataService;
        _performerMenajerSozlesmeDataService = performerMenajerSozlesmeDataService;
        _kullaniciBasicDataService = kullaniciBasicDataService;
        _amazonS3Service = amazonS3Service;
        _yetenekTemsilcisiDataService = yetenekTemsilcisiDataService;
    }

    public async Task<OdiResponse<MenajerAbonelikKalanKullanimOutputDTO>> MenajerAbonelikKalanKullanimSayilariGetir(YetenekTemsilcisiIdDTO model, string jwtToken)
    {
        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/menajer-abonelik-kalan-kullanim-sayilari";

        OdiResponse<MenajerAbonelikKalanKullanimOutputDTO> apiResult = await webApiRequest.Post<MenajerAbonelikKalanKullanimOutputDTO, YetenekTemsilcisiIdDTO>(url, jwtToken, model);

        if (apiResult?.IsSuccessful == true)
        {
            return OdiResponse<MenajerAbonelikKalanKullanimOutputDTO>.Success("Kalan kullanım sayıları getirildi.", apiResult.Data, 200);
        }
        else
        {
            return OdiResponse<MenajerAbonelikKalanKullanimOutputDTO>.Fail("Kalan kullanım sayıları alınamadı.", apiResult?.Errors ?? null, 400);
        }
    }

    public async Task<OdiResponse<bool>> MenajerAbonelikPerformerPremiumDagitma(PerformerPremiumDagitmaInputDTO model, string jwtToken, OdiUser user)
    {
        OdiResponse<MenajerAbonelikKalanKullanimOutputDTO> kalanKullanimSayisiResult = await MenajerAbonelikKalanKullanimSayilariGetir(new YetenekTemsilcisiIdDTO() { YetenekTemsilcisiId = model.YetenekTemsilcisiId }, jwtToken);

        if (kalanKullanimSayisiResult?.IsSuccessful == true)
        {
            if (kalanKullanimSayisiResult.Data.KalanPremiumProfilSayisi > model.PerformerListesi.Count)
            {
                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-bilgileri-getir";

                YetenekTemsilcisiAbonelikIdDTO requestModel = new YetenekTemsilcisiAbonelikIdDTO();

                requestModel.YetenekTemsilcisiAbonelikId = model.YetenekTemsilcisiAbonelikId;

                OdiResponse<YetenekTemsilcisiAbonelikBilgileriGetirOutputDTO> apiResult = await webApiRequest.Post<YetenekTemsilcisiAbonelikBilgileriGetirOutputDTO, YetenekTemsilcisiAbonelikIdDTO>(url, jwtToken, requestModel);

                if (apiResult?.IsSuccessful == true)
                {
                    PerformerAbonelikUrunu performerAbonelikUrunu = await _performerAbonelikUrunuDataService.PerformerAbonelikUrunuGetirByPeriod(apiResult.Data.OdemePeriodu);

                    if (performerAbonelikUrunu != null)
                    {
                        bool anyErrorReceived = false;
                        int successPremiumGiveCount = 0;
                        List<string> allErrorMessages = new List<string>();

                        foreach (var item in model.PerformerListesi)
                        {
                            PerformerAbonelikCreateDTO abonelik = new PerformerAbonelikCreateDTO();

                            abonelik.PerformerId = item.PerformerId;
                            abonelik.MenajerId = model.YetenekTemsilcisiId;
                            abonelik.AbonelikUrunId = performerAbonelikUrunu.Id;
                            abonelik.KalanFotografSayisi = performerAbonelikUrunu.FotografSayisi;
                            abonelik.KalanTanitimVideosuSayisi = performerAbonelikUrunu.TanitimVideosuSayisi;
                            abonelik.KalanShowreelSayisi = performerAbonelikUrunu.ShowreelSayisi;
                            abonelik.KalanPerformerVideosuSayisi = performerAbonelikUrunu.PerformansVideosuSayisi;
                            abonelik.AbonelikVeren = (int)AbonelikVeren.Menajer;
                            abonelik.UcretsizYenile = true;
                            abonelik.Yenile = true;
                            abonelik.AbonelikBaslangicTarihi = apiResult.Data.AbonelikBaslangicTarihi;
                            abonelik.AbonelikBitisTarihi = apiResult.Data.AbonelikBitisTarihi;
                            abonelik.Aktif = true;
                            abonelik.SureUzat = true;
                            abonelik.SureUzatmaSebebi = PerformerAbonelikUzatmaSebepleri.MAY;
                            abonelik.AbonelikReferenceCode = "";

                            OdiResponse<string> result = await _performerAbonelikLogicService.PerformerAbonelikOlustur(abonelik, user);

                            if (result?.IsSuccessful == false)
                            {
                                anyErrorReceived = true;
                                allErrorMessages.Add(string.Format("{0} id'li Performer'a premium eklenemedi. Hatalar: {1}", item.PerformerId, string.Join(" | ", result.Errors)));
                            }
                            else
                            {
                                successPremiumGiveCount++;
                            }
                        }

                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-kalan-premium-profil-sayisi-ekleme-cikarma";

                        YetenekTemsilcisiKalanPremiumProfilSayisiEklemeCikarmaInputDTO kalanHakEklemeCikarmaRequest = new YetenekTemsilcisiKalanPremiumProfilSayisiEklemeCikarmaInputDTO();

                        kalanHakEklemeCikarmaRequest.YetenekTemsilcisiAbonelikId = model.YetenekTemsilcisiAbonelikId;
                        kalanHakEklemeCikarmaRequest.Ekle = false;
                        kalanHakEklemeCikarmaRequest.EklemeCikarmaDegeri = successPremiumGiveCount;

                        OdiResponse<int> kalanHakEklemeCikarmaResult = await webApiRequest.Post<int, YetenekTemsilcisiKalanPremiumProfilSayisiEklemeCikarmaInputDTO>(url, jwtToken, kalanHakEklemeCikarmaRequest);

                        if (kalanHakEklemeCikarmaResult?.IsSuccessful == true)
                        {
                            if (anyErrorReceived)
                            {
                                return OdiResponse<bool>.Fail("Bazı işlemlerde hata meydana geldi.", allErrorMessages, 400);
                            }
                            else
                            {
                                return OdiResponse<bool>.Success("Premiumlar dağıtıldı.", true, 200);
                            }
                        }
                        else
                        {
                            allErrorMessages.AddRange(kalanHakEklemeCikarmaResult.Errors);
                            return OdiResponse<bool>.Fail("Bazı işlemlerde hata meydana geldi.", allErrorMessages, 400);
                        }
                    }
                    else
                    {
                        return OdiResponse<bool>.Fail("Premiumlar dağıtılamadı.", "İlgili periyoda ait abonelik ürünü bulunamadı.", 400);
                    }
                }
                else
                {
                    return OdiResponse<bool>.Fail("Kalan kullanım sayıları alınamadı.", apiResult?.Errors ?? null, 400);
                }
            }
            else
            {
                return OdiResponse<bool>.Fail("Premium için kalan performer sayısı kullanımı yetersiz.", kalanKullanimSayisiResult?.Errors ?? null, 400);
            }
        }
        else
        {
            return OdiResponse<bool>.Fail("Premiumlar dağıtılamadı.", kalanKullanimSayisiResult?.Errors ?? null, 400);
        }
    }

    #region Performer Menajer Sözleşme

    public async Task<OdiResponse<PerformerMenajerSozlesmeOutputDTO>> PerformerMenajerSozlesmeEkle(PerformerMenajerSozlesmeCreateDTO model, OdiUser user, string jwt)
    {
        PerformerMenajerSozlesme performerMenajerSozlesme = _mapper.Map<PerformerMenajerSozlesme>(model);

        performerMenajerSozlesme.SozlesmeyiEkleyenId = user.Id;

        DateTime date = DateTime.Now;

        performerMenajerSozlesme.Ekleyen = user.AdSoyad;
        performerMenajerSozlesme.EkleyenId = user.Id;
        performerMenajerSozlesme.EklenmeTarihi = date;

        performerMenajerSozlesme.Guncelleyen = user.AdSoyad;
        performerMenajerSozlesme.GuncelleyenId = user.Id;
        performerMenajerSozlesme.GuncellenmeTarihi = date;

        performerMenajerSozlesme = await _performerMenajerSozlesmeDataService.YeniPerformerMenajerSozlesme(performerMenajerSozlesme);

        return OdiResponse<PerformerMenajerSozlesmeOutputDTO>.Success("Yeni sözleşme oluşturuldu", await PerformerMenajerSozlesmeGetir(performerMenajerSozlesme.PerformerId, performerMenajerSozlesme.MenajerId, jwt), 200);
    }

    public async Task<OdiResponse<PerformerMenajerSozlesmeOutputDTO>> PerformerMenajerSozlesmeGuncelle(PerformerMenajerSozlesmeUpdateDTO yeniPerformerMenajerSozlesme, OdiUser user, string jwt)
    {
        PerformerMenajerSozlesme performerMenajerSozlesme = await _performerMenajerSozlesmeDataService.PerformerMenajerSozlesmeGetirById(yeniPerformerMenajerSozlesme.PerformerMenajerSozlesmeId);

        if (performerMenajerSozlesme == null) return OdiResponse<PerformerMenajerSozlesmeOutputDTO>.Fail("Bu id ile bir kayıt bulunamadı.", "Not Found", 404);

        performerMenajerSozlesme = _mapper.Map(yeniPerformerMenajerSozlesme, performerMenajerSozlesme);

        performerMenajerSozlesme.Guncelleyen = user.AdSoyad;
        performerMenajerSozlesme.GuncelleyenId = user.Id;
        performerMenajerSozlesme.GuncellenmeTarihi = DateTime.Now;

        performerMenajerSozlesme = await _performerMenajerSozlesmeDataService.PerformerMenajerSozlesmeGuncelle(performerMenajerSozlesme);

        return OdiResponse<PerformerMenajerSozlesmeOutputDTO>.Success("Sözleşme güncellendi", await PerformerMenajerSozlesmeGetir(performerMenajerSozlesme.PerformerId, performerMenajerSozlesme.MenajerId, jwt), 200);
    }

    //public async Task<OdiResponse<PerformerMenajerSozlesmeOutputDTO>> PerformerMenajerSozlesmeGetir(PerformerMenajerSozlesmeGetirInputDTO model, string jwt)
    //{
    //    return OdiResponse<PerformerMenajerSozlesmeOutputDTO>.Success("Sözleşme getirildi", await PerformerMenajerSozlesmeGetir(model.PerformerId, model.MenajerId, jwt), 200);
    //}

    //public async Task<OdiResponse<MenajerPerformerSozlesmeGetirOutputDTO>> MenajerPerformerSozlesmeGetir(MenajerPerformerSozlesmeGetirInputDTO model)
    //{
    //    PerformerMenajerSozlesme performerMenajerSozlesme = await _performerMenajerSozlesmeDataService.PerformerMenajerSozlesmeGetirByMenajerPerformerId(model.PerformerId, model.MenajerId);
    //    if (performerMenajerSozlesme == null) return OdiResponse<MenajerPerformerSozlesmeGetirOutputDTO>.Fail("Bu id ile bir kayıt bulunamadı.", "Not Found", 404);

    //    MenajerPerformerSozlesmeGetirOutputDTO dto = new MenajerPerformerSozlesmeGetirOutputDTO();

    //    KullaniciBasic menajer = await _kullaniciBasicDataService.KullaniciGetir(model.MenajerId);

    //    dto.MenajerId = model.MenajerId;

    //    if (menajer != null)
    //    {
    //        dto.MenajerAdSoyad = menajer.KullaniciAdSoyad;
    //        dto.MenajerProfilFotografi = string.IsNullOrEmpty(dto.MenajerProfilFotografi) ? "" : _amazonS3Service.GetPreSignedUrl(dto.MenajerProfilFotografi);
    //        dto.MenajerTelefonNumarasi = menajer.KullaniciTelefon;
    //        dto.MenajerEmail = menajer.KullaniciEmail;
    //    }

    //    dto.SozleşmeImzaTarihi = performerMenajerSozlesme.SozleşmeImzaTarihi;
    //    dto.SozlesmeBitisTarihi = performerMenajerSozlesme.SozlesmeBitisTarihi;
    //    dto.SozlesmeSuresi = performerMenajerSozlesme.SozlesmeSuresi;
    //    dto.KalanGun = (performerMenajerSozlesme.SozlesmeBitisTarihi - DateTime.Now).Days;

    //    dto.SozlesmeDosyasi = string.IsNullOrEmpty(performerMenajerSozlesme.SozlesmeDosyasi) ? "" : _amazonS3Service.GetPreSignedUrl(performerMenajerSozlesme.SozlesmeDosyasi);

    //    return OdiResponse<MenajerPerformerSozlesmeGetirOutputDTO>.Success("Sözleşme getirildi.", dto, 200);
    //}

    public async Task<OdiResponse<List<MenajerPerformerSozlesmeGetirOutputDTO>>> MenajerPerformerSozlesmeListesiGetir(MenajerPerformerSozlesmeGetirInputDTO model)
    {
        List<PerformerMenajerSozlesme> performerMenajerSozlesmeListesi = await _performerMenajerSozlesmeDataService.PerformerMenajerSozlesmeListesiGetirByMenajerPerformerId(model.PerformerId, model.MenajerId);
        if (performerMenajerSozlesmeListesi.Any() == false) return OdiResponse<List<MenajerPerformerSozlesmeGetirOutputDTO>>.Fail("Herhangi bir kayıt bulunamadı.", "Not Found", 404);

        List<MenajerPerformerSozlesmeGetirOutputDTO> resultList = new();

        KullaniciBasic menajer = await _kullaniciBasicDataService.KullaniciGetir(model.MenajerId);

        foreach (PerformerMenajerSozlesme performerMenajerSozlesme in performerMenajerSozlesmeListesi)
        {
            MenajerPerformerSozlesmeGetirOutputDTO dto = new MenajerPerformerSozlesmeGetirOutputDTO();

            dto.MenajerId = model.MenajerId;
            dto.MenajerAdSoyad = menajer?.KullaniciAdSoyad ?? model.MenajerId;

            if (menajer != null)
            {
                dto.MenajerProfilFotografi = string.IsNullOrEmpty(dto.MenajerProfilFotografi) ? "" : _amazonS3Service.GetPreSignedUrl(dto.MenajerProfilFotografi);
                dto.MenajerTelefonNumarasi = menajer.KullaniciTelefon;
                dto.MenajerEmail = menajer.KullaniciEmail;
            }

            dto.PerformerMenajerSozlesmeId = performerMenajerSozlesme.Id;
            dto.SozleşmeImzaTarihi = performerMenajerSozlesme.SozleşmeImzaTarihi;
            dto.SozlesmeBitisTarihi = performerMenajerSozlesme.SozlesmeBitisTarihi;
            dto.SozlesmeSuresi = performerMenajerSozlesme.SozlesmeSuresi;
            dto.KalanGun = (performerMenajerSozlesme.SozlesmeBitisTarihi - DateTime.Now).Days;

            dto.SozlesmeDosyasi = string.IsNullOrEmpty(performerMenajerSozlesme.SozlesmeDosyasi) ? "" : _amazonS3Service.GetPreSignedUrl(performerMenajerSozlesme.SozlesmeDosyasi);

            resultList.Add(dto);
        }

        return OdiResponse<List<MenajerPerformerSozlesmeGetirOutputDTO>>.Success("Sözleşmeler getirildi.", resultList, 200);
    }

    //public async Task<OdiResponse<List<PerformerMenajerSozlesmeOutputDTO>>> PerformerMenajerSozlesmeListesiGetir(MenajerIdDTO model, string jwt)
    //{
    //    List<PerformerMenajerSozlesme> performerMenajerSozlesmeList = await _performerMenajerSozlesmeDataService.PerformerMenajerSozlesmeListesiGetir(model.MenajerId);

    //    List<PerformerMenajerSozlesmeOutputDTO> dtoList = _mapper.Map<List<PerformerMenajerSozlesmeOutputDTO>>(performerMenajerSozlesmeList);

    //    List<string> performerIdList = dtoList.Select(x => x.PerformerId).ToList();

    //    WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
    //    string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";
    //    OdiResponse<List<KullaniciBilgileriDTO>> apiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdList);

    //    if (apiResult.IsSuccessful)
    //    {
    //        foreach (var item in dtoList)
    //        {
    //            item.MenajerAdSoyad = (await _kullaniciBasicDataService.KullaniciGetir(item.MenajerId))?.KullaniciAdSoyad ?? string.Empty;
    //            item.SozlesmeyiEkleyenAdSoyad = (await _kullaniciBasicDataService.KullaniciGetir(item.SozlesmeyiEkleyenId))?.KullaniciAdSoyad ?? string.Empty;

    //            item.Performer = apiResult.Data.Where(x => x.Id == item.PerformerId).FirstOrDefault();

    //            item.SozlesmeDosyasi = string.IsNullOrEmpty(item.SozlesmeDosyasi) ? "" : _amazonS3Service.GetPreSignedUrl(item.SozlesmeDosyasi);

    //            if (item.Performer != null)
    //            {
    //                PerformerMenajerListItemOutputDTO menajer = await _yetenekTemsilcisiDataService.PerformerMenajerGetir(item.PerformerId);

    //                if (menajer != null)
    //                {
    //                    item.Performer.MenajerId = menajer.MenajerId;
    //                    item.Performer.MenajerAdSoyad = menajer.MenajerAdSoyad;
    //                }
    //            }
    //        }

    //        return OdiResponse<List<PerformerMenajerSozlesmeOutputDTO>>.Success("Sözleşme listesi getirildi", dtoList, 200);
    //    }
    //    else
    //    {
    //        return OdiResponse<List<PerformerMenajerSozlesmeOutputDTO>>.Fail("Kullanıcı bilgileri alınamadı.", apiResult.Errors, 400);
    //    }
    //}

    private async Task<PerformerMenajerSozlesmeOutputDTO> PerformerMenajerSozlesmeGetir(string performerId, string menajerId, string jwt)
    {
        PerformerMenajerSozlesme performerMenajerSozlesme = await _performerMenajerSozlesmeDataService.PerformerMenajerSozlesmeGetirByMenajerPerformerId(performerId, menajerId);
        if (performerMenajerSozlesme == null) return new PerformerMenajerSozlesmeOutputDTO();

        PerformerMenajerSozlesmeOutputDTO dto = _mapper.Map<PerformerMenajerSozlesmeOutputDTO>(performerMenajerSozlesme);

        KullaniciBasic menajer = await _kullaniciBasicDataService.KullaniciGetir(dto.MenajerId);

        if (menajer != null)
            dto.MenajerAdSoyad = menajer.KullaniciAdSoyad;

        KullaniciBasic sozlenmeyiEkleyen = await _kullaniciBasicDataService.KullaniciGetir(dto.SozlesmeyiEkleyenId);

        if (sozlenmeyiEkleyen != null)
            dto.SozlesmeyiEkleyenAdSoyad = sozlenmeyiEkleyen.KullaniciAdSoyad;

        dto.SozlesmeDosyasi = string.IsNullOrEmpty(dto.SozlesmeDosyasi) ? "" : _amazonS3Service.GetPreSignedUrl(dto.SozlesmeDosyasi);

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("IdentityServerURL").Value + "/api/user/GetUserById";
        OdiResponse<KullaniciBilgileriDTO> apiResult = await webApiRequest.Post<KullaniciBilgileriDTO, KullaniciIdDTO>(url, jwt, new KullaniciIdDTO { KullaniciId = performerId });

        if (apiResult.IsSuccessful && apiResult.Data != null)
        {
            dto.Performer = apiResult.Data;

            if (dto.Performer != null)
            {
                dto.Performer.ProfilFotografi = string.IsNullOrEmpty(dto.Performer.ProfilFotografiDosyaYolu) ? "" : _amazonS3Service.GetPreSignedUrl(dto.Performer.ProfilFotografiDosyaYolu);

                PerformerMenajerListItemOutputDTO performerMenajer = await _yetenekTemsilcisiDataService.PerformerMenajerGetir(performerId);

                if (performerMenajer != null)
                {
                    dto.Performer.MenajerId = performerMenajer.MenajerId;
                    dto.Performer.MenajerAdSoyad = performerMenajer.MenajerAdSoyad;
                }
            }
        }

        return dto;
    }

    #endregion
}