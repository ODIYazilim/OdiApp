using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.PerformerDataServices.CVFormAlanlariDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.DeneyimDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.Egitim;
using OdiApp.DataAccessLayer.PerformerDataServices.FizikselOzellikler;
using OdiApp.DataAccessLayer.PerformerDataServices.KisiselOzelliklerDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerGuncellenenAlaniDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs.Interfaces;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerEtiketleriDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerProfilAlanlariDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.SektorDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekData;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.MenajerPerformerGuncellenenAlanlarDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVEgitim;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVYetenek;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;
using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.DTOs.PerformerDTOs.SektorDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.DeneyimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.FizikselOzelliklerDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.GuncellenenAlanDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerEtiketDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.EntityLayer.PerformerModels.Deneyimler;
using OdiApp.EntityLayer.PerformerModels.Egitim;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;
using OdiApp.EntityLayer.PerformerModels.KisiselOzellikler;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;

public class PerformerCVLogicService : IPerformerCVLogicService
{
    private readonly IMapper _mapper;
    private readonly IPerformerCVDataService _performerCVDataService;
    private readonly IAmazonS3Service _amazonS3Service;

    private readonly IMenajerPerformerGuncellenenAlaniDataService _menajerPerformerGuncellenenAlaniDataService;
    private readonly IKullaniciBasicDataService _kullaniciBasicDataService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private readonly ICVDataService _cVDataService;
    private readonly ICVFormAlanlariDataService _cvFormAlanlariDataService;
    private readonly IYetenekTemsilcisiDataService _yetenekTemsilcisiDataService;
    private readonly IDeneyimDataService _deneyimDataService;
    private readonly ICVEgitimDataService _cVEgitimDataService;
    private readonly IYetenekDataService _yetenekDataService;
    private readonly IEgitimDataService _egitimDataService;
    private readonly IFizikselOzellikDataService _fizikselOzellikDataService;
    private readonly IPerformerProfilAlanlariDataService _performerProfilAlanlariDataService;
    private readonly IPerformerEtiketleriDataService _performerEtiketleriDataService;
    private readonly ISektorDataService _sektorDataService;
    private readonly IKisiselOzelliklerDataService _kisiselOzelliklerDataService;

    public PerformerCVLogicService(IMapper mapper, IPerformerCVDataService performerCVDataService, ICVFormAlanlariDataService cvFormAlanlariDataService, IMenajerPerformerGuncellenenAlaniDataService menajerPerformerGuncellenenAlaniDataService, IKullaniciBasicDataService kullaniciBasicDataService, IHttpClientFactory httpClientFactory, IConfiguration configuration, ICVDataService cVDataService, IYetenekTemsilcisiDataService yetenekTemsilcisiDataService, IDeneyimDataService deneyimDataService, IYetenekDataService yetenekDataService, IAmazonS3Service amazonS3Service, ICVEgitimDataService cVEgitimDataService, IEgitimDataService egitimDataService, IPerformerProfilAlanlariDataService performerProfilAlanlariDataService, IFizikselOzellikDataService fizikselOzellikDataService, IPerformerEtiketleriDataService performerEtiketleriDataService, ISektorDataService sektorDataService, IKisiselOzelliklerDataService kisiselOzelliklerDataService)
    {
        _mapper = mapper;
        _performerCVDataService = performerCVDataService;
        _menajerPerformerGuncellenenAlaniDataService = menajerPerformerGuncellenenAlaniDataService;
        _kullaniciBasicDataService = kullaniciBasicDataService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _cVDataService = cVDataService;
        _cvFormAlanlariDataService = cvFormAlanlariDataService;
        _yetenekTemsilcisiDataService = yetenekTemsilcisiDataService;
        _deneyimDataService = deneyimDataService;
        _yetenekDataService = yetenekDataService;
        _amazonS3Service = amazonS3Service;
        _cVEgitimDataService = cVEgitimDataService;
        _egitimDataService = egitimDataService;
        _performerProfilAlanlariDataService = performerProfilAlanlariDataService;
        _fizikselOzellikDataService = fizikselOzellikDataService;
        _performerEtiketleriDataService = performerEtiketleriDataService;
        _sektorDataService = sektorDataService;
        _kisiselOzelliklerDataService = kisiselOzelliklerDataService;
    }

    #region Performer Filtreleme İşlemleri

    #endregion

    public async Task<OdiResponse<PagedData<KullaniciBilgileriDTO>>> FiltrelenenPerformerlar(FiltrelenenPerformerlarInputDTO model, string jwt)
    {
        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = string.Empty;

        ProjeOutputDTO proje = null;

        if (!string.IsNullOrEmpty(model.ProjeId))
        {
            url = _configuration.GetSection("GatewayServerURL").Value + "/servis/proje/proje-detay-getir";
            OdiResponse<ProjeOutputDTO> projeDetayGetirApiResult = await webApiRequest.Post<ProjeOutputDTO, ProjeIdDTO>(url, jwt, new ProjeIdDTO() { ProjeId = model.ProjeId });

            if (!projeDetayGetirApiResult.IsSuccessful)
                return OdiResponse<PagedData<KullaniciBilgileriDTO>>.Fail("Performer bilgileri getirilemedi.", projeDetayGetirApiResult.Errors, 400);

            proje = projeDetayGetirApiResult.Data;
        }

        PagedData<string> performerIdPagedList = await _cVDataService.FiltrelenenPerformerlarGetir(model, proje);

        //Filtrelenen performerların bilgileri getiriliyor.
        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";
        OdiResponse<List<KullaniciBilgileriDTO>> performerBilgileriListeApiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdPagedList.DataList);

        if (!performerBilgileriListeApiResult.IsSuccessful)
            return OdiResponse<PagedData<KullaniciBilgileriDTO>>.Fail("Performer bilgileri getirilemedi.", performerBilgileriListeApiResult.Errors, 400);

        PagedData<KullaniciBilgileriDTO> result = new PagedData<KullaniciBilgileriDTO>();

        result.PageNo = performerIdPagedList.PageNo;
        result.PageCount = performerIdPagedList.PageCount;
        result.Records = performerIdPagedList.Records;
        result.RecordsPerPage = performerIdPagedList.RecordsPerPage;
        result.DataList = performerBilgileriListeApiResult.Data;

        List<PerformerMenajerListItemOutputDTO> performerMenajerList = await _yetenekTemsilcisiDataService.PerformerMenajerListesiGetir(performerBilgileriListeApiResult.Data.Select(x => x.Id).ToList());

        foreach (var item in result.DataList)
        {
            item.MenajerId = performerMenajerList.FirstOrDefault(x => x.PerformerId == item.Id)?.MenajerId;
            item.MenajerAdSoyad = performerMenajerList.FirstOrDefault(x => x.PerformerId == item.Id)?.MenajerAdSoyad;
            item.ProfilFotografi = string.IsNullOrEmpty(item.ProfilFotografiDosyaYolu) ? "" : _amazonS3Service.GetPreSignedUrl(item.ProfilFotografiDosyaYolu);
        }

        return OdiResponse<PagedData<KullaniciBilgileriDTO>>.Success("Performer listesi getirildi.", result, 200);
    }

    public async Task<OdiResponse<FiltrelenenPerformerlarOutputDTO>> FiltrelenenPerformerlarFiltreAyarlari(FiltreAyarlariInputDTO model, string jwt, int dilId)
    {
        FiltrelenenPerformerlarOutputDTO result = new FiltrelenenPerformerlarOutputDTO();

        result.Filtreler = new List<FiltrelenenPerformerlarFilterItem>();

        //Cinsiyet Filtresi
        FiltrelenenPerformerlarFilterItem cinsiyetFiltre = new FiltrelenenPerformerlarFilterItem();

        cinsiyetFiltre.FiltreBasligi = "Cinsiyet";
        cinsiyetFiltre.AlanKodu = "CNSY";
        cinsiyetFiltre.FilterType = PerformerFilterType.MultiSelectFilter;

        //Cinsiyet seçenekleri
        List<FizikselOzellik>? cinsiyetSecenekleri = await _fizikselOzellikDataService.FizikselOzellikListesiByDilId("CNSY", dilId);

        if (cinsiyetSecenekleri?.Any() == true)
        {
            cinsiyetFiltre.FilterItems = cinsiyetSecenekleri.Select(x => new MultiSelectFilterItem() { Adi = x.FizikselOzellikAdi, StringDeger = x.FizikselOzellikKodu }).ToList();
        }

        result.Filtreler.Add(cinsiyetFiltre);

        //Yaş Filtresi
        FiltrelenenPerformerlarFilterItem yasFiltre = new FiltrelenenPerformerlarFilterItem();

        yasFiltre.FiltreBasligi = "Yaş";
        yasFiltre.AlanKodu = "";
        yasFiltre.FilterType = PerformerFilterType.YasFilter;

        result.Filtreler.Add(yasFiltre);

        //>>> Diğer Kişisel ve Fiziksel Özellikler Filtre İşlemleri

        //Detaylı olmayan basit görünümde gösterilecek olan filtre alanları
        List<string> gosterilecekFiltreAlanKodlari = new List<string>() {
            "BOYU"
        };

        //Yaş gibi, Cinsiyet gibi bazı alanlar özel veya sıralamanın dışınca oluşturulduğu için bu alanları yoksaymak gerekir. 
        List<string> yoksayilacakFiltreAlanKodları = new List<string>()
        {
            "CNSY",
            "DOGT",
            "CBSY",
            "CBTY"
        };

        //Tüm kişisel özelliklerinin form alanlarını getirir.
        List<CVFormAlani> cVFormAlanListesi = await _cvFormAlanlariDataService.TumCVFormAlanlariListesiGetir();

        //Sadece gösterilecek olanların form alanlarını getirir.
        //List<CVFormAlani> cVFormAlanListesi = await _cvFormAlanlariDataService.CVFormAlanlariListesiGetirByAlanKodlari(gosterilecekFiltreAlanKodlari);

        List<CVDataBasicDTO> cvDataBasicList = await _cVDataService.CVDataVerileriGetir();

        if (cVFormAlanListesi?.Any() == true)
        {
            //Tüm kişisel özellikleri getirir.
            List<KisiselOzellik> kisiselOzelliklerListesi = await _kisiselOzelliklerDataService.KisiselOzellikListe(dilId);

            foreach (KisiselOzellik kisiselOzellik in kisiselOzelliklerListesi)
            {
                //Eğer detaylı filtre istenmiyorsa ve gösterilecek alanlarda da ilgili filtre yoksa, geçilir.
                if (!model.DetayliFiltre && !gosterilecekFiltreAlanKodlari.Contains(kisiselOzellik.KisiselOzellikKodu))
                    continue;

                //Eğer yok sayılacaklar listesinde ise bir sonraki geçilecek.
                if (yoksayilacakFiltreAlanKodları.Contains(kisiselOzellik.KisiselOzellikKodu))
                    continue;

                CVFormAlani? cVFormAlani = cVFormAlanListesi?.Where(x => x.AlanKodu == kisiselOzellik.KisiselOzellikKodu)?.FirstOrDefault();

                if (cVFormAlani == null)
                    continue;

                FiltrelenenPerformerlarFilterItem kisiselOzellikFiltre = new FiltrelenenPerformerlarFilterItem();

                kisiselOzellikFiltre.FiltreBasligi = kisiselOzellik.KisiselOzellikAdi;
                kisiselOzellikFiltre.AlanKodu = kisiselOzellik.KisiselOzellikKodu;
                kisiselOzellikFiltre.DetayliFiltre = !gosterilecekFiltreAlanKodlari.Contains(kisiselOzellik.KisiselOzellikKodu);

                if (cVFormAlani != null && cVFormAlani.DataType == "Int")
                {
                    kisiselOzellikFiltre.FilterType = PerformerFilterType.RangeFilter;
                }
                else
                {
                    kisiselOzellikFiltre.FilterType = PerformerFilterType.MultiSelectFilter;

                    kisiselOzellikFiltre.FilterItems = cvDataBasicList
                        .Where(x => x.AlanKodu == kisiselOzellik.KisiselOzellikKodu)
                        .Select(x => new MultiSelectFilterItem() { Adi = x.Deger, StringDeger = x.Deger })
                        .ToList();
                }

                result.Filtreler.Add(kisiselOzellikFiltre);
            }

            //Tüm fiziksel özellik tiplerini getirir.
            List<FizikselOzellikTipi> fizikselOzelliklerTipListesi = await _fizikselOzellikDataService.FizikselOzellikTipiListe(dilId);

            //Kodlar yerine isimlerin getirilmesi için kullanılır.
            List<FizikselOzellik> tumFizikselOzelliklerListesi = await _fizikselOzellikDataService.TumFizikselOzellikListesiByDilId(dilId);

            foreach (FizikselOzellikTipi fizikselOzellik in fizikselOzelliklerTipListesi)
            {
                //Eğer detaylı filtre istenmiyorsa ve gösterilecek alanlarda da ilgili filtre yoksa, geçilir.
                if (!model.DetayliFiltre && !gosterilecekFiltreAlanKodlari.Contains(fizikselOzellik.FizikselOzellikTipKodu))
                    continue;

                //Eğer yok sayılacaklar listesinde ise bir sonraki geçilecek.
                if (yoksayilacakFiltreAlanKodları.Contains(fizikselOzellik.FizikselOzellikTipKodu))
                    continue;

                CVFormAlani? cVFormAlani = cVFormAlanListesi?.Where(x => x.AlanKodu == fizikselOzellik.FizikselOzellikTipKodu)?.FirstOrDefault();

                if (cVFormAlani == null)
                    continue;

                FiltrelenenPerformerlarFilterItem fizikselOzellikFiltre = new FiltrelenenPerformerlarFilterItem();

                fizikselOzellikFiltre.FiltreBasligi = fizikselOzellik.FizikselOzellikTipAdi;
                fizikselOzellikFiltre.AlanKodu = fizikselOzellik.FizikselOzellikTipKodu;
                fizikselOzellikFiltre.DetayliFiltre = !gosterilecekFiltreAlanKodlari.Contains(fizikselOzellik.FizikselOzellikTipKodu);

                if (cVFormAlani != null && cVFormAlani.DataType == "Int")
                {
                    fizikselOzellikFiltre.FilterType = PerformerFilterType.RangeFilter;
                }
                else
                {
                    fizikselOzellikFiltre.FilterType = PerformerFilterType.MultiSelectFilter;


                    fizikselOzellikFiltre.FilterItems = cvDataBasicList
                        .Where(x => x.AlanKodu == fizikselOzellik.FizikselOzellikTipKodu)
                        .Select(x => new MultiSelectFilterItem() { Adi = x.Deger, StringDeger = x.Deger })
                        .ToList();

                    foreach (MultiSelectFilterItem item in fizikselOzellikFiltre.FilterItems)
                    {
                        item.Adi = tumFizikselOzelliklerListesi
                            .Where(x => x.FizikselOzellikKodu == item.StringDeger)
                            .Select(x => x.FizikselOzellikAdi)
                            .FirstOrDefault() ?? item.Adi;
                    }
                }

                result.Filtreler.Add(fizikselOzellikFiltre);
            }
        }

        //<<< Diğer Kişisel ve Fiziksel Özellikler Filtre İşlemleri

        //Puan Filtresi
        FiltrelenenPerformerlarFilterItem puanFiltre = new FiltrelenenPerformerlarFilterItem();

        puanFiltre.FiltreBasligi = "Puan";
        puanFiltre.AlanKodu = "";
        puanFiltre.FilterType = PerformerFilterType.PuanFilter;

        result.Filtreler.Add(puanFiltre);

        //Eğitim Filtresi
        FiltrelenenPerformerlarFilterItem egitimFiltre = new FiltrelenenPerformerlarFilterItem();

        egitimFiltre.FiltreBasligi = "Eğitim";
        egitimFiltre.AlanKodu = "";
        egitimFiltre.FilterType = PerformerFilterType.EgitimFilter;

        egitimFiltre.FilterItems = new List<MultiSelectFilterItem>() {
            new MultiSelectFilterItem() { Adi = "Eğitimi Olmayanlar", IntDeger = -1 }
        };

        List<EgitimTipi>? egitimTipiListesi = await _egitimDataService.EgitimTipiListesi();

        if (egitimTipiListesi?.Any() == true)
        {
            egitimFiltre.FilterItems.AddRange(egitimTipiListesi.Select(x => new MultiSelectFilterItem() { Adi = x.Tip, IntDeger = x.Id }));
        }

        result.Filtreler.Add(egitimFiltre);

        //Deneyim Filtresi
        FiltrelenenPerformerlarFilterItem deneyimFiltre = new FiltrelenenPerformerlarFilterItem();

        deneyimFiltre.FiltreBasligi = "Deneyim";
        deneyimFiltre.AlanKodu = "";
        deneyimFiltre.FilterType = PerformerFilterType.DeneyimFilter;

        deneyimFiltre.FilterItems = new List<MultiSelectFilterItem>() {
            new MultiSelectFilterItem() { Adi = "Deneyimi Olmayanlar", StringDeger = "XXX" }
        };

        List<Deneyim>? deneyimTipiListesi = await _deneyimDataService.DeneyimTipiListesi(dilId);

        if (deneyimTipiListesi?.Any() == true)
        {
            deneyimFiltre.FilterItems.AddRange(deneyimTipiListesi.Select(x => new MultiSelectFilterItem() { Adi = x.DeneyimAdi, StringDeger = x.DeneyimKodu }));
        }

        result.Filtreler.Add(deneyimFiltre);

        //Yetenekler Filtresi
        FiltrelenenPerformerlarFilterItem yeteneklerFiltre = new FiltrelenenPerformerlarFilterItem();

        yeteneklerFiltre.FiltreBasligi = "Yetenekler";
        yeteneklerFiltre.AlanKodu = "";
        yeteneklerFiltre.FilterType = PerformerFilterType.YetenekFilter;

        yeteneklerFiltre.FilterItems = new List<MultiSelectFilterItem>();

        List<YetenekTipiDTO> yetenekListesi = await _yetenekDataService.YetenekListesi(dilId);

        if (yetenekListesi?.Any() == true)
        {
            foreach (YetenekTipiDTO yetenekTipi in yetenekListesi)
            {
                var multiSelectFilterItem = new MultiSelectFilterItem();

                multiSelectFilterItem.Adi = yetenekTipi.YetenekTipiAdi;
                multiSelectFilterItem.StringDeger = yetenekTipi.YetenekTipiKodu;

                multiSelectFilterItem.GrupMu = true;

                multiSelectFilterItem.AltItemlar = new List<MultiSelectFilterItem>();

                foreach (YetenekOutputDTO yetenek in yetenekTipi.Liste)
                {
                    var altItem = new MultiSelectFilterItem();

                    altItem.Adi = yetenek.YetenekAdi;
                    altItem.StringDeger = yetenek.YetenekKodu;

                    multiSelectFilterItem.AltItemlar.Add(altItem);
                }

                yeteneklerFiltre.FilterItems.Add(multiSelectFilterItem);
            }
        }

        result.Filtreler.Add(yeteneklerFiltre);

        if (!model.MenajerFiltreAyarlari)
        {
            //Menajer Filtresi
            FiltrelenenPerformerlarFilterItem yetenekTemsilcisiFiltre = new FiltrelenenPerformerlarFilterItem();

            yetenekTemsilcisiFiltre.FiltreBasligi = "Ajans/Menajer";
            yetenekTemsilcisiFiltre.AlanKodu = "";
            yetenekTemsilcisiFiltre.FilterType = PerformerFilterType.YetenekTemsilcisiFilter;

            yetenekTemsilcisiFiltre.FilterItems = new List<MultiSelectFilterItem>()
            {
                new MultiSelectFilterItem() { Adi = "ODİ", StringDeger = "ODI" }
            };

            List<KullaniciBasic> yetenekTemsilcisiListesi = await _kullaniciBasicDataService.KullaniciListesiGetirByKayitGrubuKodu(KayitGrupKodlari.YetenekTemsilcisi);

            if (yetenekTemsilcisiListesi?.Any() == true)
            {
                yetenekTemsilcisiFiltre.FilterItems.AddRange(yetenekTemsilcisiListesi.Select(x => new MultiSelectFilterItem() { StringDeger = x.KullaniciId, Adi = x.KullaniciAdSoyad }));
            }

            result.Filtreler.Add(yetenekTemsilcisiFiltre);

            //Profil Tipine Göre Filtresi
            FiltrelenenPerformerlarFilterItem profilTipiFiltre = new FiltrelenenPerformerlarFilterItem();

            profilTipiFiltre.FiltreBasligi = "Profil Tipine Göre";
            profilTipiFiltre.AlanKodu = "";
            profilTipiFiltre.FilterType = PerformerFilterType.ProfilTipiFilter;

            profilTipiFiltre.FilterItems = new List<MultiSelectFilterItem>
            {
                new MultiSelectFilterItem() { Adi = "Eksik Profil", IntDeger = (int)ProfilTipiFilterType.EksikProfil },
                new MultiSelectFilterItem() { Adi = "Onaylı Profil", IntDeger = (int)ProfilTipiFilterType.OnayliProfil },
                new MultiSelectFilterItem() { Adi = "Premium Profil", IntDeger = (int)ProfilTipiFilterType.PremiumProfil }
            };

            result.Filtreler.Add(profilTipiFiltre);
        }

        //İçeriğe Göre Filtresi
        FiltrelenenPerformerlarFilterItem icerigeGoreFiltre = new FiltrelenenPerformerlarFilterItem();

        icerigeGoreFiltre.FiltreBasligi = "İçeriğe Göre";
        icerigeGoreFiltre.AlanKodu = "";
        icerigeGoreFiltre.FilterType = PerformerFilterType.IcerigeGoreFilter;

        icerigeGoreFiltre.FilterItems = new List<MultiSelectFilterItem>
        {
            new MultiSelectFilterItem() { Adi = "Tanıtım Videosu Olanlar", IntDeger = (int)IcerigeGoreFilterType.TanitimVideosuOlanlar },
            new MultiSelectFilterItem() { Adi = "Mimik Videosu Olanlar", IntDeger = (int)IcerigeGoreFilterType.MimikVideosuOlanlar },
            new MultiSelectFilterItem() { Adi = "Showreel'ı Olanlar", IntDeger = (int)IcerigeGoreFilterType.ShowreeliOlanlar },
            new MultiSelectFilterItem() { Adi = "Portre Kolajı Olanlar", IntDeger = (int)IcerigeGoreFilterType.PortreKolajiOlanlar }
        };

        result.Filtreler.Add(icerigeGoreFiltre);

        if (!model.MenajerFiltreAyarlari)
        {
            //Yine de Göster Filtresi
            FiltrelenenPerformerlarFilterItem yineDeGosterFiltre = new FiltrelenenPerformerlarFilterItem();

            yineDeGosterFiltre.FiltreBasligi = "Yine de Göster";
            yineDeGosterFiltre.AlanKodu = "";
            yineDeGosterFiltre.FilterType = PerformerFilterType.YineDeGosterFilter;

            yineDeGosterFiltre.FilterItems = new List<MultiSelectFilterItem>
            {
                new MultiSelectFilterItem() { Adi = "Müsait Olmayanlar", IntDeger = (int)YineDeGosterFilterType.MusaitOlmayanlar },
                new MultiSelectFilterItem() { Adi = "Yasaklı Kelimeye Takılanlar", IntDeger = (int)YineDeGosterFilterType.YasakliKelimeyeTakilanlar },
                new MultiSelectFilterItem() { Adi = "Bütçe Üstü Olanlar", IntDeger = (int)YineDeGosterFilterType.ButceUstuOlanlar },
                new MultiSelectFilterItem() { Adi = "Katılım Şehrine Uymayanlar", IntDeger = (int)YineDeGosterFilterType.KatilimSehrineUymayanlar }
            };

            result.Filtreler.Add(yineDeGosterFiltre);
        }

        return OdiResponse<FiltrelenenPerformerlarOutputDTO>.Success("Filtre ayarları getirildi.", result, 200);
    }

    public async Task<OdiResponse<ProjeyeGoreOnerilenOyuncularOutputDTO>> ProjeyeGoreOnerilenOyuncular(ProjeyeGoreOnerilenOyuncularInputDTO model, string jwt)
    {
        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/proje/proje-rol-filtre-getir";

        ProjeRolIdDTO requestModel = new ProjeRolIdDTO();

        requestModel.ProjeRolId = model.ProjeRolId;

        OdiResponse<FiltrelenenPerformerlarInputDTO> projeRolFiltreGetirApiResult = await webApiRequest.Post<FiltrelenenPerformerlarInputDTO, ProjeRolIdDTO>(url, jwt, requestModel);

        if (!projeRolFiltreGetirApiResult.IsSuccessful)
        {
            return OdiResponse<ProjeyeGoreOnerilenOyuncularOutputDTO>.Fail("Performer listesi getirilemedi.", projeRolFiltreGetirApiResult.Errors, 400);
        }

        //Sayfalama isteği burada hazırlanıyor ve buna göre performer listesi alınıyor. 
        FiltrelenenPerformerlarInputDTO filtrelenenPerformerlarInputDTO = projeRolFiltreGetirApiResult.Data;

        filtrelenenPerformerlarInputDTO.Page = model.Page;
        filtrelenenPerformerlarInputDTO.Limit = model.Limit;

        //İlk başta sadece onaylı kullanıcıların getirilmesi için filtre ekleniyor.
        filtrelenenPerformerlarInputDTO.ProfilTipiFilter = new List<ProfilTipiFilterType>() { ProfilTipiFilterType.OnayliProfil };

        ProjeyeGoreOnerilenOyuncularOutputDTO resultModel = new ProjeyeGoreOnerilenOyuncularOutputDTO();

        OdiResponse<PagedData<KullaniciBilgileriDTO>> kullanicilarPagedDataResponse = await FiltrelenenPerformerlar(filtrelenenPerformerlarInputDTO, jwt);

        if (!kullanicilarPagedDataResponse.IsSuccessful)
        {
            return OdiResponse<ProjeyeGoreOnerilenOyuncularOutputDTO>.Fail("Performer listesi getirilemedi.", kullanicilarPagedDataResponse.Errors, 400);
        }

        resultModel.KullanicilarPagedData = kullanicilarPagedDataResponse.Data;
        resultModel.UygulananFiltreler = projeRolFiltreGetirApiResult.Data;

        return OdiResponse<ProjeyeGoreOnerilenOyuncularOutputDTO>.Success("Performer listesi getirildi.", resultModel, 200);
    }

    #region Sektör İşlemleri

    public async Task<OdiResponse<List<SektorOutputDTO>>> SektorListesi(int dilId)
    {
        return OdiResponse<List<SektorOutputDTO>>.Success("Sektör listesi getirildi.", _mapper.Map<List<SektorOutputDTO>>(await _sektorDataService.SektorListesiGetir(dilId)), 200);
    }

    #endregion

    #region Performer CV

    public async Task<OdiResponse<CVOutputDTO>> PerformerCVGetir(PerformerIdDTO id, int dilId)
    {
        string mesaj = "";
        CVOutputDTO cv = await CvGetir(id.ToString(), dilId);
        if (cv.CVId == null) mesaj = "Henüz cv oluşturulmamış ";
        else mesaj = "CV getirildi";

        return OdiResponse<CVOutputDTO>.Success(mesaj, cv, 200);
        //PerformerCVOutputDTO cvold = await _performerCVDataService.PerformerCVGetirByUserId(id.PerformerId);
        //return OdiResponse<PerformerCVOutputDTO>.Success("Performer CV getirildi", cv, 200);
    }

    private async Task<CVOutputDTO> CvGetir(string performerId, int dilId)
    {
        string mesaj = "";
        KullaniciBasic kb = await _kullaniciBasicDataService.KullaniciGetir(performerId);
        List<string> kayitTurleri = new List<string>(kb.KayitTuruKodu.Split(','));
        CV cv = await _cVDataService.CVGetir(performerId);
        CVOutputDTO cvOutput = new CVOutputDTO();

        if (cv == null)
        {
            cvOutput.PerformerId = kb.KullaniciId;
            cvOutput.cVFormAlanlariDTOs = await CVFormAlanlariGetir(kayitTurleri, dilId);
        }
        else
        {
            cvOutput.CVId = cv.Id;
            cvOutput.PerformerId = cv.PerformerId;
            cvOutput.MenajerGordu = cv.MenajerGordu;
            cvOutput.MenajerGorduTarih = cv.MenajerGorduTarih;
            cvOutput.MenajerId = cv.MenajerId;
            cvOutput.cVFormAlanlariDTOs = await CVFormAlanlariGetir(kayitTurleri, dilId);

            if (cv.DataList != null)
            {
                foreach (var item in cvOutput.cVFormAlanlariDTOs)
                {
                    CVData data = cv.DataList.Where(x => x.AlanKodu == item.AlanKodu).FirstOrDefault();

                    item.Deger = ConvertDegerToObject(item.DataType, data?.Deger);
                    item.Deger2 = ConvertDegerToObject(item.DataType, data?.Deger2);
                }
            }
        }

        return cvOutput;
    }

    private object ConvertDegerToObject(string dataType, string deger)
    {
        if (!string.IsNullOrEmpty(deger))
        {
            if (dataType == "String") return deger;
            if (dataType == "Tarih") return Convert.ToDateTime(deger);
            if (dataType == "Int") return Convert.ToInt32(deger);
            if (dataType == "Slider Int") return Convert.ToInt32(deger);
            if (dataType == "Boolean") return Convert.ToBoolean(deger);
            if (dataType == "Select") return deger;
            if (dataType == "Multi Select") return deger.Split(',').ToList();
            else return deger;
        }

        else return deger;
    }

    public async Task<OdiResponse<RolOzellikAyarlariDTO>> RolOzellikAyarlariGetir(KayitTuruKodlariDTO kayitTuruKodlariDTO, int dilId, string jwt)
    {
        RolOzellikAyarlariDTO result = new RolOzellikAyarlariDTO();

        List<YetenekTemsilcisiPerformerEtiketTipi> performerEtiketTipleri = await _performerEtiketleriDataService.YetenekTemsilcisiPerformerEtiketTipiListesiGetir(dilId);

        result.PerformerEtiketListesi = performerEtiketTipleri.Select(x => new PerformerEtiketTipiDTO()
        {
            PerformerEtiketTipAdi = x.EtiketTipAdi,
            PerformerEtiketTipKodu = x.EtiketTipKodu
        }).ToList();

        List<PerformerEtiket> allPerformerEtiketListesi = await _performerEtiketleriDataService.PerformerEtiketListesiGetir(dilId);

        foreach (var item in result.PerformerEtiketListesi)
        {
            List<PerformerEtiket> performerEtiketListesi = allPerformerEtiketListesi.Where(x => x.EtiketTipKodu == item.PerformerEtiketTipKodu).ToList();

            item.Liste = performerEtiketListesi.Select(x => new PerformerEtiketOutputDTO() { PerformerEtiketAdi = x.EtiketAdi, PerformerEtiketKodu = x.EtiketKodu }).ToList();
        }

        //result.FizikselOzellikListesi = _mapper.Map<List<FizikselOzellikTipiOutputDTO>>(await _fizikselOzellikDataService.FizikselOzellikTipiListesi(dilId));
        result.FizikselOzellikListesi = _mapper.Map<List<FizikselOzellikTipiOutputDTO>>(await _fizikselOzellikDataService.FizikselOzellikTipiListesiByKayitTuruKodu(kayitTuruKodlariDTO.KayitTuruKodlari, dilId));
        result.DeneyimListesi = _mapper.Map<List<DeneyimDTO>>(await _deneyimDataService.DeneyimListesi(dilId));
        result.EgitimListesi = _mapper.Map<List<EgitimTipiDTO>>(await _egitimDataService.EgitimListesi());
        result.YetenekListesi = _mapper.Map<List<YetenekTipiDTO>>(await _yetenekDataService.YetenekListesi(dilId));

        return OdiResponse<RolOzellikAyarlariDTO>.Success("Rol özellik ayarları getirildi", result, 200);
    }

    public async Task<OdiResponse<CVAyarlariDTO>> CVAyarlariGetir(CVAyarlariGetirInputModel model, int dilId)
    {
        CVAyarlariDTO cvAyarlari = new CVAyarlariDTO();

        cvAyarlari.CVFormAlanlari = await _cvFormAlanlariDataService.CVFormAlanlariGetir(model.KayitTuruList, dilId);

        //cvAyarlari.EgitimListesi = 
        //cvAyarlari.YetenekListesi = _mapper.Map<List<YetenekTipiDTO>>(await _yetenekDataService.YetenekListesi());
        //cvAyarlari.DeneyimListesi = _mapper.Map<List<DeneyimTipiDTO>>(await _deneyimDataService.DeneyimListesi());

        return OdiResponse<CVAyarlariDTO>.Success("Cv ayarları getirildi", cvAyarlari, 200);
    }

    private async Task<List<CVFormAlanlariDTO>> CVFormAlanlariGetir(List<string> kayitTurleri, int dilId)
    {
        return await _cvFormAlanlariDataService.CVFormAlanlariGetir(kayitTurleri, dilId);
    }

    public async Task<OdiResponse<CVOutputDTO>> YeniPerformerCV(CVCreateDTO cvDTO, OdiUser user, string jwt, int dilId)
    {
        bool varmi = await _cVDataService.CVVarmi(cvDTO.PerformerId);
        if (varmi) return OdiResponse<CVOutputDTO>.Fail("Bu kullanıcıya ait CV i bulunmaktadır. Güncelleme işlemi yapınız", "Bad request", 400);

        PerformerMenajerListItemOutputDTO performerMenajer = await _yetenekTemsilcisiDataService.PerformerMenajerGetir(cvDTO.PerformerId);

        CV cv = _mapper.Map<CV>(cvDTO);

        cv.MenajerId = performerMenajer.MenajerId;

        cv.EklenmeTarihi = DateTime.Now;
        cv.Ekleyen = user.AdSoyad;
        cv.EkleyenId = user.Id;

        cv.GuncelleyenId = user.Id;
        cv.Guncelleyen = user.AdSoyad;
        cv.GuncellenmeTarihi = DateTime.Now;

        foreach (var item in cv.DataList)
        {
            item.EklenmeTarihi = DateTime.Now;
            item.Ekleyen = user.AdSoyad;
            item.EkleyenId = user.Id;

            item.GuncelleyenId = user.Id;
            item.Guncelleyen = user.AdSoyad;
            item.GuncellenmeTarihi = DateTime.Now;
        }

        cv = await _cVDataService.YeniCV(cv);
        //PerformerCV cv = _mapper.Map<PerformerCV>(cvDTO);

        //cv.EklenmeTarihi = DateTime.Now;
        //cv.Ekleyen = user.AdSoyad;
        //cv.EkleyenId = user.Id;

        //cv.GuncelleyenId = user.Id;
        //cv.Guncelleyen = user.AdSoyad;
        //cv.GuncellenmeTarihi = DateTime.Now;

        //await _performerCVDataService.YeniPerformerCV(cv);

        //PerformerCVOutputDTO output = await _performerCVDataService.PerformerCVGetirByUserId(cv.PerformerId);

        return OdiResponse<CVOutputDTO>.Success("CV oluşturuldu", await CvGetir(cv.PerformerId, dilId), 200);

    }

    public async Task<OdiResponse<CVOutputDTO>> PerformerCVGuncelle(CVUpdateDTO cvDTO, OdiUser user, string jwt, int dilId)
    {
        CV cv = await _cVDataService.CVGetir(cvDTO.PerformerId);

        if (cv == null) return OdiResponse<CVOutputDTO>.Fail("CV bulunamadı. Güncelleme işlemi başarısız.", "Bad request", 400);

        if (string.IsNullOrEmpty(cv.MenajerId) && user.Id != cv.MenajerId)
        {
            await KontrolleriYapVeGuncellemeleriKaydet(cv, cvDTO, cv.MenajerId, user);

            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/bildirim/pushbildirim/yeni-bildirim";

            OdiBildirimCreateDTO requestModel = new OdiBildirimCreateDTO();

            KullaniciBasic menajer = await _kullaniciBasicDataService.KullaniciGetir(cv.MenajerId);
            KullaniciBasic performer = await _kullaniciBasicDataService.KullaniciGetir(cv.PerformerId);

            requestModel.BildirimTipi = BildirimTipleri.CVGuncelleme;
            requestModel.Mesaj = $"{performer.KullaniciAdSoyad} adlı kullanıcı CV güncelledi.";
            requestModel.Baslik = "CV Güncelleme";
            requestModel.AltBaslik = "";
            requestModel.KullaniciId = menajer.KullaniciId;
            requestModel.KullaniciAdSoyad = menajer.KullaniciAdSoyad;
            requestModel.GonderenKullaniciId = user.Id;
            requestModel.GonderenKullaniciAdSoyad = user.AdSoyad;
            requestModel.DosyaYolu = string.Empty;

            OdiResponse<OdiBildirimOutputDTO> apiResult = await webApiRequest.Post<OdiBildirimOutputDTO, OdiBildirimCreateDTO>(url, jwt, requestModel);
        }

        cv.GuncelleyenId = user.Id;
        cv.Guncelleyen = user.AdSoyad;
        cv.GuncellenmeTarihi = DateTime.Now;

        foreach (var yeniData in cvDTO.DataList)
        {
            var mevcutData = cv.DataList?.FirstOrDefault(x => x.AlanKodu == yeniData.AlanKodu);

            if (mevcutData != null)
            {
                // Mevcut veriyi güncelle
                mevcutData.Deger = yeniData.Deger;
                mevcutData.Deger2 = yeniData.Deger2;
                mevcutData.GuncelleyenId = user.Id;
                mevcutData.Guncelleyen = user.AdSoyad;
                mevcutData.GuncellenmeTarihi = DateTime.Now;
            }
            else
            {
                // Yeni veri ekle
                cv.DataList?.Add(new CVData
                {
                    CVId = cv.Id,
                    AlanKodu = yeniData.AlanKodu,
                    Deger = yeniData.Deger,
                    Deger2 = yeniData.Deger2,
                    EkleyenId = user.Id,
                    Ekleyen = user.AdSoyad,
                    EklenmeTarihi = DateTime.Now,
                    GuncelleyenId = user.Id,
                    Guncelleyen = user.AdSoyad,
                    GuncellenmeTarihi = DateTime.Now
                });
            }
        }

        //var silinecekVeriler = cv.DataList?.Where(x => !cvDTO.DataList.Any(y => y.AlanKodu == x.AlanKodu)).ToList();
        //if (silinecekVeriler != null)
        //{
        //    foreach (var silinecek in silinecekVeriler)
        //    {
        //        cv.DataList?.Remove(silinecek);
        //    }
        //}

        await _cVDataService.CVGuncelle(cv);

        return OdiResponse<CVOutputDTO>.Success("CV güncellendi.", await CvGetir(cv.PerformerId, dilId), 200);
    }

    //public async Task<OdiResponse<List<KullaniciBilgileriDTO>>> MenajerGuncellenenPerformerlarListesi(MenajerGuncellenenPerformerlarListesiInputDTO model, string jwt)
    //{
    //    List<string> performerIdList = await _menajerPerformerGuncellenenAlaniDataService.MenajerGuncellenenPerformerlarIdListesi(model.YetenekTemsilcisiId, model.GorulduOlanlariDahilEt, model.EklenmeTarihindenItibaren);

    //    WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
    //    string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";
    //    OdiResponse<List<KullaniciBilgileriDTO>> apiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdList);

    //    if (!apiResult.IsSuccessful)
    //    {
    //        return OdiResponse<List<KullaniciBilgileriDTO>>.Fail(apiResult.Message, "Bad request", 400);
    //    }

    //    List<PerformerMenajerListItemOutputDTO> performerMenajerList = await _yetenekTemsilcisiDataService.PerformerMenajerListesiGetir(apiResult.Data.Select(x => x.Id).ToList());

    //    foreach (KullaniciBilgileriDTO kullanici in apiResult.Data)
    //    {
    //        kullanici.MenajerId = performerMenajerList.FirstOrDefault(x => x.PerformerId == kullanici.Id)?.MenajerId;
    //        kullanici.MenajerAdSoyad = performerMenajerList.FirstOrDefault(x => x.PerformerId == kullanici.Id)?.MenajerAdSoyad;
    //        kullanici.ProfilFotografi = string.IsNullOrEmpty(kullanici.ProfilFotografiDosyaYolu) ? "" : _amazonS3Service.GetPreSignedUrl(kullanici.ProfilFotografiDosyaYolu);
    //    }

    //    return OdiResponse<List<KullaniciBilgileriDTO>>.Success("Güncelleme yapan performerların listesi getirildi.", apiResult.Data, 200);
    //}

    public async Task<OdiResponse<List<MenajerPerformerGuncellenenAlaniOutputDTO>>> MenajerPerformerGuncellenenAlanlarListesi(MenajerPerformerGuncellenenAlanlarListesiInputDTO model)
    {
        List<MenajerPerformerGuncellenenAlani> list = await _menajerPerformerGuncellenenAlaniDataService.MenajerPerformerGuncellenenAlaniListesiSon1AyGetir(model.PerformerId, model.MenajerId);
        List<MenajerPerformerGuncellenenAlaniOutputDTO> dtoList = null;

        if (list?.Any() == true)
        {
            dtoList = _mapper.Map<List<MenajerPerformerGuncellenenAlaniOutputDTO>>(list);

            foreach (var item in dtoList)
            {
                KullaniciBasic kullaniciBasic = await _kullaniciBasicDataService.KullaniciGetir(item.PerformerId);
                item.PerformerAdSoyad = kullaniciBasic?.KullaniciAdSoyad ?? string.Empty;
            }
        }

        return OdiResponse<List<MenajerPerformerGuncellenenAlaniOutputDTO>>.Success("Güncellenen alanlar listesi getirildi.", dtoList, 200);
    }

    public async Task<OdiResponse<bool>> MenajerPerformerGuncellenenAlanlarGoruldu(MenajerPerformerGuncellenenAlaniIdDTO model, OdiUser user)
    {
        MenajerPerformerGuncellenenAlani menajerPerformerGuncellenenAlani = await _menajerPerformerGuncellenenAlaniDataService.MenajerPerformerGuncellenenAlaniGetir(model.MenajerPerformerGuncellenenAlaniId);
        if (menajerPerformerGuncellenenAlani == null) return OdiResponse<bool>.Fail("Kayıt bulunamadı", "Not Found", 404);

        DateTime date = DateTime.Now;

        menajerPerformerGuncellenenAlani.MenajerGordu = true;
        menajerPerformerGuncellenenAlani.MenajerGorduTarihi = date;

        menajerPerformerGuncellenenAlani.GuncellenmeTarihi = date;
        menajerPerformerGuncellenenAlani.Guncelleyen = user.AdSoyad;
        menajerPerformerGuncellenenAlani.GuncelleyenId = user.Id;

        menajerPerformerGuncellenenAlani = await _menajerPerformerGuncellenenAlaniDataService.MenajerPerformerGuncellenenAlaniGuncelle(menajerPerformerGuncellenenAlani);


        return OdiResponse<bool>.Success("Görüldü işlemi tamamlandı.", true, 200);
    }

    public async Task<OdiResponse<bool>> GuncellenenAlanEkle(GuncelenenAlanEkleInputDTO model, OdiUser user, string jwt)
    {
        PerformerMenajerListItemOutputDTO menajerDTO = await _yetenekTemsilcisiDataService.PerformerMenajerGetir(model.PerformerId);

        if (menajerDTO == null) return OdiResponse<bool>.Fail("Menajer bulunamadı.", "Bad request", 400);

        MenajerPerformerGuncellenenAlani guncellenenAlanlar = new MenajerPerformerGuncellenenAlani()
        {
            MenajerId = menajerDTO.MenajerId,
            PerformerId = model.PerformerId,
            GuncellemeTipi = model.GuncellemeTipi,
            GuncellenenAlan = model.GuncellenenAlan,
            GuncellenmeTarihi = DateTime.Now,
            MenajerGordu = false,
            EklenmeTarihi = DateTime.Now,
            EkleyenId = user.Id,
            Ekleyen = user.AdSoyad,
            GuncelleyenId = user.Id,
            Guncelleyen = user.AdSoyad
        };

        await _menajerPerformerGuncellenenAlaniDataService.YeniMenajerPerformerGuncellenenAlani(guncellenenAlanlar);

        WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
        string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/bildirim/pushbildirim/yeni-bildirim";

        OdiBildirimCreateDTO requestModel = new OdiBildirimCreateDTO();

        KullaniciBasic menajer = await _kullaniciBasicDataService.KullaniciGetir(menajerDTO.MenajerId);
        KullaniciBasic performer = await _kullaniciBasicDataService.KullaniciGetir(model.PerformerId);

        requestModel.BildirimTipi = BildirimTipleri.CVGuncelleme;
        requestModel.Mesaj = $"{performer.KullaniciAdSoyad} adlı kullanıcı profil fotoğrafını güncelledi.";
        requestModel.Baslik = "Profil fotoğrafı güncelleme";
        requestModel.AltBaslik = "";
        requestModel.KullaniciId = menajerDTO.MenajerId;
        requestModel.KullaniciAdSoyad = menajer?.KullaniciAdSoyad ?? "";
        requestModel.GonderenKullaniciId = user.Id;
        requestModel.GonderenKullaniciAdSoyad = user.AdSoyad;
        requestModel.DosyaYolu = string.Empty;

        OdiResponse<OdiBildirimOutputDTO> apiResult = await webApiRequest.Post<OdiBildirimOutputDTO, OdiBildirimCreateDTO>(url, jwt, requestModel);

        return OdiResponse<bool>.Success("Güncellenen alan eklendi. Bildirim gönderildi.", true, 200);
    }

    private async Task KontrolleriYapVeGuncellemeleriKaydet(CV eskiModel, CVUpdateDTO yeniModel, string menajerId, OdiUser user)
    {
        MenajerPerformerGuncellenenAlani guncellenenAlanlar = new MenajerPerformerGuncellenenAlani()
        {
            MenajerId = menajerId,
            PerformerId = yeniModel.PerformerId,
            GuncellemeTipi = PerformerCVGuncellemeTipi.CV,
            GuncellenenAlan = "CV Güncellendi. Güncellenen alanlar; ",
            GuncellenmeTarihi = DateTime.Now,
            MenajerGordu = false,
            EklenmeTarihi = DateTime.Now,
            EkleyenId = user.Id,
            Ekleyen = user.AdSoyad,
            GuncelleyenId = user.Id,
            Guncelleyen = user.AdSoyad
        };

        // Kullanıcının kayıt türü kodlarını al
        KullaniciBasic performerBasic = await _kullaniciBasicDataService.KullaniciGetir(yeniModel.PerformerId);

        // CVFormAlanlari listesini al
        var cvFormAlanlari = await _cvFormAlanlariDataService.CVFormAlanlariListesiGetir(performerBasic.KayitTuruKodu.Split(',').ToList(), yeniModel.DataList.Select(d => d.AlanKodu).ToList());

        void AlanDegisimiKontrolu<T>(T eskiDeger, T yeniDeger, string alanKodu)
        {
            if (!EqualityComparer<T>.Default.Equals(eskiDeger, yeniDeger))
            {
                var alanAdi = cvFormAlanlari.FirstOrDefault(x => x.AlanKodu == alanKodu)?.AlanAdi ?? alanKodu;
                guncellenenAlanlar.GuncellenenAlan = guncellenenAlanlar.GuncellenenAlan + alanAdi + ", ";
            }
        }

        // CVData alanlarını kontrol et
        foreach (var yeniData in yeniModel.DataList)
        {
            var eskiData = eskiModel.DataList?.FirstOrDefault(x => x.AlanKodu == yeniData.AlanKodu);
            if (eskiData != null)
            {
                AlanDegisimiKontrolu(eskiData.Deger, yeniData.Deger, yeniData.AlanKodu);
                AlanDegisimiKontrolu(eskiData.Deger2, yeniData.Deger2, yeniData.AlanKodu);
            }
        }

        guncellenenAlanlar.GuncellenenAlan = guncellenenAlanlar.GuncellenenAlan.TrimEnd(',', ' ') + ".";

        await _menajerPerformerGuncellenenAlaniDataService.YeniMenajerPerformerGuncellenenAlani(guncellenenAlanlar);
    }

    #endregion

    #region CV Deneyim

    public async Task<OdiResponse<List<DeneyimDTO>>> DeneyimListesi(int dilId)
    {
        return OdiResponse<List<DeneyimDTO>>.Success("Deneyim Listesi Getirildi", await _deneyimDataService.DeneyimListesi(dilId), 200);
    }

    public async Task<OdiResponse<List<CVDeneyimOutputDTO>>> YeniCvDeneyim(CVDeneyimCreateDTO yeniDeneyim, OdiUser user, int dilId)
    {
        CVDeneyim deneyim = new CVDeneyim { CVId = yeniDeneyim.CVId, DeneyimKodu = yeniDeneyim.DeneyimKodu, Detaylar = new List<CVDeneyimDetay>() };

        deneyim.Ekleyen = user.AdSoyad;
        deneyim.EkleyenId = user.Id;
        deneyim.EklenmeTarihi = DateTime.Now;

        deneyim.Guncelleyen = user.AdSoyad;
        deneyim.GuncelleyenId = user.Id;
        deneyim.GuncellenmeTarihi = DateTime.Now;

        List<CVDeneyimDetay> cvDetayList = new List<CVDeneyimDetay>();

        foreach (var item in yeniDeneyim.Detaylar)
        {
            CVDeneyimDetay detay = new CVDeneyimDetay();
            detay.FormAlaniKodu = item.AlanKodu;
            detay.Deger = Convert.ToString(item.Deger);

            detay.Ekleyen = user.AdSoyad;
            detay.EkleyenId = user.Id;
            detay.EklenmeTarihi = DateTime.Now;

            detay.Guncelleyen = user.AdSoyad;
            detay.GuncelleyenId = user.Id;
            detay.GuncellenmeTarihi = DateTime.Now;

            cvDetayList.Add(detay);
        }

        deneyim.Detaylar = cvDetayList;

        deneyim = await _cVDataService.YeniCVDeneyim(deneyim);

        return OdiResponse<List<CVDeneyimOutputDTO>>.Success("Deneyim Eklendi", await _deneyimDataService.CVDeneyimListesi(deneyim.CVId, dilId), 200);
    }

    public async Task<OdiResponse<List<CVDeneyimOutputDTO>>> CVDeneyimSil(CVDeneyimDeleteDTO deneyimSil, int dilId)
    {
        bool sonuc = await _cVDataService.CVDeneyimSil(deneyimSil.CVDeneyimId);
        string mesaj = "";
        if (sonuc) mesaj = "Deneyim Silindi";
        else mesaj = "Bu id ye ait deneyim bulunamadı";
        return OdiResponse<List<CVDeneyimOutputDTO>>.Success("Deneyim Silindi", await _deneyimDataService.CVDeneyimListesi(deneyimSil.CVId, dilId), 400);
    }

    public async Task<OdiResponse<List<CVDeneyimOutputDTO>>> CVDeneyimListesi(CVIdDTO cvId, int dilId)
    {
        return OdiResponse<List<CVDeneyimOutputDTO>>.Success("Cv Deneyim Listesi Getirildi", await _deneyimDataService.CVDeneyimListesi(cvId.ToString(), dilId), dilId);
    }

    #endregion

    #region CV Yetenek
    public async Task<OdiResponse<List<YetenekTipiDTO>>> YetenekListesi(int dilId)
    {
        return OdiResponse<List<YetenekTipiDTO>>.Success("Yetenek Listesi Getirildi", await _yetenekDataService.YetenekListesi(dilId), 200);
    }

    public async Task<OdiResponse<List<CVYetenekOutputDTO>>> CVYetenekListesi(string cvId, int dilId)
    {
        return OdiResponse<List<CVYetenekOutputDTO>>.Success("CV Yetenek Listesi Getirildi", await CVYetenekOutputDTOList(cvId, dilId), 200);
    }

    private async Task<List<CVYetenekOutputDTO>> CVYetenekOutputDTOList(string cvId, int dilId)
    {
        List<CVYetenekOutputDTO> resultList = await _cVDataService.CVYetenekListesi(cvId, dilId);

        foreach (var item in resultList)
        {
            item.VideoUrl = !string.IsNullOrEmpty(item.VideoUrl) ? _amazonS3Service.GetPreSignedUrl(item.VideoUrl) : null;
        }

        return resultList;
    }


    public async Task<OdiResponse<List<CVYetenekOutputDTO>>> YeniCVYetenek(CVYetenekCreateDTO cvYetenek, OdiUser user, int dilId)
    {
        CVYetenek yetenek = new CVYetenek { CVId = cvYetenek.CVId, YetenekTipiKodu = cvYetenek.YetenekTipiKodu, YetenekKodu = cvYetenek.YetenekKodu, Derece = cvYetenek.Derece };

        yetenek.EklenmeTarihi = DateTime.Now;
        yetenek.Ekleyen = user.AdSoyad;
        yetenek.EkleyenId = user.Id;

        yetenek.GuncellenmeTarihi = DateTime.Now;
        yetenek.Guncelleyen = user.AdSoyad;
        yetenek.GuncelleyenId = user.Id;

        await _cVDataService.YeniCVYetenek(yetenek);

        return OdiResponse<List<CVYetenekOutputDTO>>.Success("Yeni CV Yetenek Oluşturuldu", await CVYetenekOutputDTOList(cvYetenek.CVId, dilId), 200);
    }

    public async Task<OdiResponse<List<CVYetenekOutputDTO>>> CVYetenekSil(CVYetenekDeleteDTO cvYetenek, int dilId)
    {
        bool result = await _cVDataService.CVYetenekSil(cvYetenek.CVYetenekId);
        string mesaj = "";
        if (result) mesaj = "CV Yetenek Silindi";
        else mesaj = "Bu id ye sahip bir CV yetenek bulunamadı";
        return OdiResponse<List<CVYetenekOutputDTO>>.Success(mesaj, await CVYetenekOutputDTOList(cvYetenek.CVId, dilId), 200);
    }

    public async Task<OdiResponse<CVYetenekVideoOutputDTO>> YeniCVYetenekVideo(CVYetenekVideoCreateDTO cvYetenekVideo, OdiUser user)
    {
        //en fazla 1 video yüklenebilir
        CVYetenekVideoOutputDTO output = new CVYetenekVideoOutputDTO();
        bool result = await _cVDataService.CheckCVYetenekVideosu(cvYetenekVideo.CVYetenekId);
        if (result) return OdiResponse<CVYetenekVideoOutputDTO>.Success("Daha önce bu yetenek için video eklenmiş. En fazla 1 video yükleyebilirsiniz", output, 400);


        CVYetenekVideo video = new CVYetenekVideo { CVYetenekId = cvYetenekVideo.CVYetenekId, Video = cvYetenekVideo.VideoUrl, Tags = cvYetenekVideo.Tags };
        video.EklenmeTarihi = DateTime.Now;
        video.Ekleyen = user.AdSoyad;
        video.EkleyenId = user.Id;

        video.GuncellenmeTarihi = DateTime.Now;
        video.Guncelleyen = user.AdSoyad;
        video.GuncelleyenId = user.Id;

        video = await _cVDataService.YeniCVYetenekVideosu(video);

        CVYetenekVideoOutputDTO videoOutput = new CVYetenekVideoOutputDTO { CVYetenekId = video.CVYetenekId, VideoUrl = _amazonS3Service.GetPreSignedUrl(video.Video) };

        return OdiResponse<CVYetenekVideoOutputDTO>.Success("Yeni cv yetenek videosu kayıt edildi", videoOutput, 200);
    }

    public async Task<OdiResponse<bool>> CVYetenekVideoSil(CVYetenekVideoIdDTO cvYetenekVideoId)
    {
        string mesaj = "";
        bool result = await _cVDataService.CVYetenekVideosuSil(cvYetenekVideoId.ToString());
        if (result) return OdiResponse<bool>.Success("Yetenek videosu silindi", true, 200);
        else return OdiResponse<bool>.Success("Bu id ye ait bir video bulunamadı", true, 400);
    }
    #endregion

    #region CV Eğitim

    public async Task<OdiResponse<List<EgitimTipiDTO>>> OkullarListesi()
    {
        return OdiResponse<List<EgitimTipiDTO>>.Success("Okullar Listesi Getirildi", _mapper.Map<List<EgitimTipiDTO>>(await _egitimDataService.EgitimListesi()), 200);
    }

    public async Task<OdiResponse<List<CVEgitimOutputDTO>>> CVEgitimListesi(CVIdDTO cvId)
    {
        return OdiResponse<List<CVEgitimOutputDTO>>.Success("CV Egitim Listesi Getirildi", await _cVDataService.CVEgitimListesi(cvId.ToString()), 200);
    }

    public async Task<OdiResponse<List<CVEgitimOutputDTO>>> YeniCVEgitim(CVEgitimCreateDTO cvEgitim, OdiUser user)
    {
        CVEgitim egitim = new CVEgitim
        {
            CVId = cvEgitim.CVId,
            EgitimTipiId = cvEgitim.EgitimTipiId,
            OkulId = cvEgitim.OkulId,
            BolumId = cvEgitim.BolumId,
            Yil = cvEgitim.Yil
        };

        egitim.EklenmeTarihi = DateTime.Now;
        egitim.Ekleyen = user.AdSoyad;
        egitim.EkleyenId = user.Id;

        egitim.GuncellenmeTarihi = DateTime.Now;
        egitim.Guncelleyen = user.AdSoyad;
        egitim.GuncelleyenId = user.Id;

        egitim = await _cVDataService.YeniCVEgitim(egitim);
        return OdiResponse<List<CVEgitimOutputDTO>>.Success("CV Egitim Listesi Getirildi", await _cVDataService.CVEgitimListesi(cvEgitim.CVId), 200);
    }

    public async Task<OdiResponse<List<CVEgitimOutputDTO>>> CVEgitimSil(CVEgitimDeleteDTO cvDelete)
    {
        bool resut = await _cVDataService.CVEgitimSil(cvDelete.CVEgitimId);
        if (resut)
            return OdiResponse<List<CVEgitimOutputDTO>>.Success("CV Egitim  Silindi", await _cVDataService.CVEgitimListesi(cvDelete.CVId), 200);
        else return OdiResponse<List<CVEgitimOutputDTO>>.Success("Bu id ile eğitim bilgisi bulunamadı", await _cVDataService.CVEgitimListesi(cvDelete.CVId), 400);
    }



    #endregion

    #region Profil Video

    public async Task<OdiResponse<List<ProfilVideoOutputDTO>>> YeniProfilVideo(ProfilVideoCreateDTO videoDTO, OdiUser user, string jwt)
    {
        ProfilVideo video = _mapper.Map<ProfilVideo>(videoDTO);

        video.EklenmeTarihi = DateTime.Now;
        video.EkleyenId = user.Id;
        video.EkleyenId = user.AdSoyad;

        video.GuncellenmeTarihi = DateTime.Now;
        video.GuncelleyenId = user.Id;
        video.Guncelleyen = user.AdSoyad;

        await _cVDataService.YeniProfilVideosu(video);

        if (videoDTO.VideoTipiKodu == "TNTM")
        {
            var guncellenenAlanInput = new GuncelenenAlanEkleInputDTO
            {
                PerformerId = videoDTO.PerformerId,
                GuncellemeTipi = PerformerCVGuncellemeTipi.TNTM,
                GuncellenenAlan = "Yeni tanıtım videosu eklendi."
            };

            var result = await GuncellenenAlanEkle(guncellenenAlanInput, user, jwt);

            if (!result.IsSuccessful)
            {
                return OdiResponse<List<ProfilVideoOutputDTO>>.Fail("Güncelleme sırasında hata oluştu.", "Error", 400);
            }
        }

        List<ProfilVideoOutputDTO> list = await _cVDataService.ProfilVideolariListesi(videoDTO.PerformerId);
        foreach (var item in list)
        {
            if (!string.IsNullOrEmpty(item.VideoURL)) item.VideoURL = _amazonS3Service.GetPreSignedUrl(item.VideoURL);
        }

        return OdiResponse<List<ProfilVideoOutputDTO>>.Success("Profil Videosu eklendi", list, 200);
    }

    public async Task<OdiResponse<List<ProfilVideoOutputDTO>>> ProfilVideosuSil(ProfilVideoDeleteDTO id, OdiUser user, string jwt)
    {
        ProfilVideo profilVideo = await _cVDataService.ProfilVideoGetir(id.ProfilVideoId);

        if (profilVideo == null) return OdiResponse<List<ProfilVideoOutputDTO>>.Fail("Bu id ile profil videosu bulunamadı.", "Bad request", 404);

        if (profilVideo.VideoTipiKodu == "TNTM")
        {
            var guncellenenAlanInput = new GuncelenenAlanEkleInputDTO
            {
                PerformerId = profilVideo.PerformerId,
                GuncellemeTipi = PerformerCVGuncellemeTipi.TNTM,
                GuncellenenAlan = "Tanıtım videosu silindi."
            };

            var result = await GuncellenenAlanEkle(guncellenenAlanInput, user, jwt);

            if (!result.IsSuccessful)
            {
                return OdiResponse<List<ProfilVideoOutputDTO>>.Fail("Güncelleme sırasında hata oluştu.", "Error", 400);
            }
        }

        await _cVDataService.ProfilVideosuSil(profilVideo);

        List<ProfilVideoOutputDTO> list = await _cVDataService.ProfilVideolariListesi(id.PerformerId);

        foreach (var item in list)
        {
            if (!string.IsNullOrEmpty(item.VideoURL)) item.VideoURL = _amazonS3Service.GetPreSignedUrl(item.VideoURL);
        }

        return OdiResponse<List<ProfilVideoOutputDTO>>.Success("Profil videosu silindi", list, 200);
    }

    public async Task<OdiResponse<List<ProfilVideoOutputDTO>>> ProfilVideosuTagsGuncelle(ProfilVideosuTagsUpdateDTO tagsUpdate, OdiUser user)
    {
        ProfilVideo video = await _cVDataService.ProfilVideoGetir(tagsUpdate.ProfilVideoId);
        video.VideoTags = tagsUpdate.VideoTags;

        video.GuncellenmeTarihi = DateTime.Now;
        video.GuncelleyenId = user.Id;
        video.Guncelleyen = user.AdSoyad;

        await _cVDataService.ProfilVideosuGuncelle(video);

        List<ProfilVideoOutputDTO> list = await _cVDataService.ProfilVideolariListesi(tagsUpdate.PerformerId);
        foreach (var item in list)
        {
            if (!string.IsNullOrEmpty(item.VideoURL)) item.VideoURL = _amazonS3Service.GetPreSignedUrl(item.VideoURL);
        }

        return OdiResponse<List<ProfilVideoOutputDTO>>.Success("Profil Video Tags Güncellendi", list, 200);
    }

    public async Task<OdiResponse<List<ProfilVideoAlbumDTO>>> ProfilVideoListesi(PerformerIdDTO performerId, int dilId)
    {
        List<ProfilVideoTipiOutputDTO> tipListesi = await _cVDataService.VideoTipiListesi(dilId);

        if (!(tipListesi != null && tipListesi.Any())) return OdiResponse<List<ProfilVideoAlbumDTO>>.Fail("Herhangi bir video tipi bulunamadı.", "Bad request", 400);

        List<ProfilVideoOutputDTO> list = await _cVDataService.ProfilVideolariListesi(performerId.ToString());

        if (list != null && list.Any())
        {
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.VideoURL)) item.VideoURL = _amazonS3Service.GetPreSignedUrl(item.VideoURL);
            }
        }

        List<ProfilVideoAlbumDTO> albumList = new List<ProfilVideoAlbumDTO>();

        foreach (var tip in tipListesi)
        {
            ProfilVideoAlbumDTO album = new ProfilVideoAlbumDTO();

            album.Sira = tip.Sira;
            album.VideoTipi = tip.VideoTipi;
            album.VideoTipiKodu = tip.VideoTipiKodu;
            album.PremiumVideoLimit = tip.PremiumVideoLimit;
            album.NormalVideoLimit = tip.NormalVideoLimit;
            album.OnerilenEtiketler = tip.OnerilenEtiketler;

            album.Videolar = list.Where(x => x.VideoTipiKodu == tip.VideoTipiKodu).ToList();

            albumList.Add(album);
        }

        return OdiResponse<List<ProfilVideoAlbumDTO>>.Success("Profil Video Listesi Getirildi", albumList, 200);
    }

    public async Task<OdiResponse<List<TopluProfilVideoAlbumDTO>>> TopluProfilVideoListesi(List<PerformerIdDTO> performerIdList, int dilId)
    {
        List<KullaniciBasic> kullanicilar = await _kullaniciBasicDataService.KullaniciListesiGetir(performerIdList.Select(s => s.PerformerId).ToList());

        List<TopluProfilVideoAlbumDTO> resultList = new List<TopluProfilVideoAlbumDTO>();

        foreach (KullaniciBasic kullanici in kullanicilar)
        {
            List<ProfilVideoTipiOutputDTO> tipListesi = await _cVDataService.VideoTipiListesi(dilId);

            if (!(tipListesi != null && tipListesi.Any())) return OdiResponse<List<TopluProfilVideoAlbumDTO>>.Fail("Herhangi bir video tipi bulunamadı.", "Bad request", 400);

            List<ProfilVideoOutputDTO> list = await _cVDataService.ProfilVideolariListesi(kullanici.KullaniciId);

            if (list != null && list.Any())
            {
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.VideoURL)) item.VideoURL = _amazonS3Service.GetPreSignedUrl(item.VideoURL);
                }
            }

            List<ProfilVideoAlbumDTO> albumList = new List<ProfilVideoAlbumDTO>();

            foreach (var tip in tipListesi)
            {
                ProfilVideoAlbumDTO album = new ProfilVideoAlbumDTO();

                album.Sira = tip.Sira;
                album.VideoTipi = tip.VideoTipi;
                album.VideoTipiKodu = tip.VideoTipiKodu;
                album.PremiumVideoLimit = tip.PremiumVideoLimit;
                album.NormalVideoLimit = tip.NormalVideoLimit;
                album.OnerilenEtiketler = tip.OnerilenEtiketler;

                album.Videolar = list.Where(x => x.VideoTipiKodu == tip.VideoTipiKodu).ToList();

                albumList.Add(album);
            }

            TopluProfilVideoAlbumDTO topluFotoAlbumDTO = new TopluProfilVideoAlbumDTO();

            topluFotoAlbumDTO.KullaniciId = kullanici.KullaniciId;
            topluFotoAlbumDTO.KullaniciAdSoyad = kullanici.KullaniciAdSoyad;
            topluFotoAlbumDTO.KullaniciEmail = kullanici.KullaniciEmail;
            topluFotoAlbumDTO.KullaniciTelefon = kullanici.KullaniciTelefon;
            topluFotoAlbumDTO.Albumler = albumList;

            resultList.Add(topluFotoAlbumDTO);
        }

        return OdiResponse<List<TopluProfilVideoAlbumDTO>>.Success("Kullanıcılara ait video albüm listesi getirildi.", resultList, 200);
    }

    public OdiResponse<List<string>> ShowreelsHashTags()
    {
        List<string> tags = new List<string> { "Dram", "Komedi", "Psikolojik" };
        return OdiResponse<List<string>>.Success("Showreels tag ları getirildi", tags, 200);
    }

    public async Task<OdiResponse<List<ProfilVideoTipiOutputDTO>>> ProfilVideoTipleri(int dilId)
    {
        return OdiResponse<List<ProfilVideoTipiOutputDTO>>.Success("Profil VideoTipleri Getirildi", await _cVDataService.VideoTipiListesi(dilId), 200);
    }

    public async Task<OdiResponse<ProfilDolulukOraniOutputDTO>> ProfilDolulukOrani(PerformerIdDTO model)
    {
        return OdiResponse<ProfilDolulukOraniOutputDTO>.Success("Profil doluluk oranı Getirildi", await _performerProfilAlanlariDataService.ProfilDolulukOrani(model.PerformerId), 200);
    }

    #endregion
}