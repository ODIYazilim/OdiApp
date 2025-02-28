using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolOdiBilgileri;
using OdiApp.DataAccessLayer.Extensions;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeBilgileri;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolBilgileri;
using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolOdiBilgileri;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolAnketSorusuDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolOzellikDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;
using OdiApp.DTOs.SharedDTOs.UygulamaBilgileriDTOs;
using OdiApp.EntityLayer.ProjelerModels.OdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.OdiSes;
using OdiApp.EntityLayer.ProjelerModels.OdiSoru;
using OdiApp.EntityLayer.ProjelerModels.OdiVideo;
using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolBilgileri
{
    public class ProjeRolLogicService : IProjeRolLogicService
    {
        private readonly IProjeRolDataService _projeRolDataService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IProjeRolOdiLogicService _projeRolOdiLogicService;
        private readonly IProjeRolOdiDataService _projeRolOdiDataService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProjeDataService _projeDataService;

        public ProjeRolLogicService(IMapper mapper, IUseOtherService useOtherService, IConfiguration configuration, IProjeRolDataService projeRolDataService, IProjeRolOdiLogicService projeRolOdiLogicService, IHttpClientFactory httpClientFactory, IProjeRolOdiDataService projeRolOdiDataService, IProjeDataService projeDataService)
        {
            _projeRolDataService = projeRolDataService;
            _mapper = mapper;
            _configuration = configuration;
            _projeRolOdiLogicService = projeRolOdiLogicService;
            _httpClientFactory = httpClientFactory;
            _projeRolOdiDataService = projeRolOdiDataService;
            _projeDataService = projeDataService;
        }

        public async Task<OdiResponse<FiltrelenenPerformerlarInputDTO>> ProjeRolFiltreGetir(ProjeRolIdDTO projeRolIdDTO)
        {
            ProjeRol projeRol = await _projeRolDataService.ProjeRolFullGetir(projeRolIdDTO.ProjeRolId);

            if (projeRol == null) return OdiResponse<FiltrelenenPerformerlarInputDTO>.Fail("Rol bulunamadı", "Not Found", 404);

            Proje proje = await _projeDataService.ProjeGetir(projeRol.ProjeId);

            if (proje == null) return OdiResponse<FiltrelenenPerformerlarInputDTO>.Fail("Proje bulunamadı", "Not Found", 404);

            FiltrelenenPerformerlarInputDTO filtreDTO = new FiltrelenenPerformerlarInputDTO();

            //Yaş Filter
            YasFilter yasFilter = new YasFilter();

            yasFilter.MinYas = projeRol.YasBaslangic;
            yasFilter.MaxYas = projeRol.YasBitis;

            //yasFilter.MinDogumTarihi = DateTime.Now.AddYears((projeRol.YasBaslangic < 0 ? 0 : projeRol.YasBaslangic) * -1);
            //yasFilter.MaxDogumTarihi = DateTime.Now.AddYears((projeRol.YasBitis < 0 ? 100 : projeRol.YasBitis) * -1);

            //Yasaklı Kelimeler Filters
            if (!string.IsNullOrEmpty(proje.YasakliKelimeler))
            {
                filtreDTO.YasakliKelimelerFilter = new YasakliKelimelerFilter();
                filtreDTO.YasakliKelimelerFilter.ReklamMi = proje.ProjeTurKodu?.Contains("RKLM") ?? false;
                filtreDTO.YasakliKelimelerFilter.YasakliKelimelerList = proje.YasakliKelimeler.Split(",").Select(s => s.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            }

            //Cinsiyet Filter
            filtreDTO.MultiSelectFilters = new List<MultiSelectFilter>();

            if (!string.IsNullOrEmpty(projeRol.Cinsiyet))
            {
                MultiSelectFilter multiSelectFilterSehirler = new MultiSelectFilter();
                multiSelectFilterSehirler.AlanKodu = "CNSY";
                multiSelectFilterSehirler.Values = new List<string>() { projeRol.Cinsiyet };

                filtreDTO.MultiSelectFilters.Add(multiSelectFilterSehirler);
            }

            if (projeRol.RolOzellik != null)
            {
                //Range Filters
                filtreDTO.RangeFilters = new List<RangeFilter>();

                RangeFilter rangeFilterBoy = new RangeFilter();

                rangeFilterBoy.AlanKodu = "BOYU";
                rangeFilterBoy.Min = projeRol.RolOzellik.MinBoy;
                rangeFilterBoy.Max = projeRol.RolOzellik.MaxBoy;

                filtreDTO.RangeFilters.Add(rangeFilterBoy);

                RangeFilter rangeFilterKilo = new RangeFilter();

                rangeFilterKilo.AlanKodu = "KILO";
                rangeFilterKilo.Min = projeRol.RolOzellik.MinKilo;
                rangeFilterKilo.Max = projeRol.RolOzellik.MaxKilo;

                filtreDTO.RangeFilters.Add(rangeFilterKilo);

                //Multiselect Filters
                if (!string.IsNullOrEmpty(projeRol.RolOzellik.Sehirler))
                {
                    MultiSelectFilter multiSelectFilterSehirler = new MultiSelectFilter();

                    multiSelectFilterSehirler.AlanKodu = "YSHR";
                    multiSelectFilterSehirler.Values = new List<string>();
                    multiSelectFilterSehirler.Values.AddRange(projeRol.RolOzellik.Sehirler.Split(",").Select(s => s.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)));

                    filtreDTO.MultiSelectFilters.Add(multiSelectFilterSehirler);
                }

                if (!string.IsNullOrEmpty(projeRol.RolOzellik.Uyruk))
                {
                    MultiSelectFilter multiSelectFilterUyruklar = new MultiSelectFilter();

                    multiSelectFilterUyruklar.AlanKodu = "UYRK";
                    multiSelectFilterUyruklar.Values = new List<string>();
                    multiSelectFilterUyruklar.Values.AddRange(projeRol.RolOzellik.Uyruk.Split(",").Select(s => s.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)));

                    filtreDTO.MultiSelectFilters.Add(multiSelectFilterUyruklar);
                }

                if (projeRol?.RolOzellik?.FizikselOzellikler?.Any() == true)
                {
                    foreach (RolOzellikFiziksel fizikselOzellik in projeRol.RolOzellik.FizikselOzellikler)
                    {
                        MultiSelectFilter? multiSelectFilterFiziksel = filtreDTO.MultiSelectFilters.FirstOrDefault(x => x.AlanKodu == fizikselOzellik.FizikselOzellikTipiKodu);

                        //Eğer tipin kaydı varsa, o zaman değerlerine ekle.
                        if (multiSelectFilterFiziksel != null)
                        {
                            multiSelectFilterFiziksel.Values.Add(fizikselOzellik.FizikselOzellikAdiKodu);
                        }
                        //Eğer kaydı yoksa, yeni kayıt oluştur ve değerlerine ekle.
                        else
                        {
                            multiSelectFilterFiziksel = new MultiSelectFilter();

                            multiSelectFilterFiziksel.AlanKodu = fizikselOzellik.FizikselOzellikTipiKodu;

                            multiSelectFilterFiziksel.Values = new List<string>() {
                                fizikselOzellik.FizikselOzellikAdiKodu
                            };

                            filtreDTO.MultiSelectFilters.Add(multiSelectFilterFiziksel);
                        }
                    }
                }

                //Kayıt Türü Kodu Filter
                filtreDTO.KayitTuruKoduList = projeRol?.RolTuruKodu.Split(",").ToList();

                //Deneyim Filters
                filtreDTO.DeneyimKoduList = projeRol?.RolOzellik?.DeneyimKodlari?.Select(x => x.DeneyimKodu).ToList();

                //Eğitim Filters
                filtreDTO.EgitimTipiIdList = projeRol?.RolOzellik?.EgitimTipleri?.Select(x => x.EgitimTipiId).ToList();

                //Yetenek Filters
                filtreDTO.YetenekFilters = projeRol?.RolOzellik?.YetenekTipleri?.Select(x => new YetenekFilter() { MinDerece = 0, YetenekKodu = x.YetenekTipiKodu }).ToList();
            }

            return OdiResponse<FiltrelenenPerformerlarInputDTO>.Success("Filtre bilgileri getirildi.", filtreDTO, 200);
        }

        public async Task<OdiResponse<ProjeRolOzellikOutputDTO>> ProjeRolOzellikGetir(ProjeRolIdDTO projeRolIdDTO)
        {
            var entity = await _projeRolDataService.ProjeRolOzellikGetir(projeRolIdDTO.ProjeRolId);
            if (entity == null)
                return OdiResponse<ProjeRolOzellikOutputDTO>.Fail("Rol bulunamadı", "Not Found", 404);

            var dto = _mapper.Map<ProjeRolOzellikOutputDTO>(entity);
            return OdiResponse<ProjeRolOzellikOutputDTO>.Success("Rol getirildi", dto, 200);
        }

        public async Task<OdiResponse<bool>> ProjeRolOzellikSil(ProjeRolIdDTO projeRolIdDTO)
        {
            var result = await _projeRolDataService.ProjeRolOzellikSil(projeRolIdDTO.ProjeRolId);
            return result ? OdiResponse<bool>.Success("Rol silindi", true, 200)
                          : OdiResponse<bool>.Fail("Rol silinemedi", "Error", 500);
        }

        public async Task<OdiResponse<bool>> YeniProjeRolOzellik(ProjeRolOzellikCreateDTO ozellikDTO, OdiUser user)
        {
            var entity = _mapper.Map<ProjeRolOzellik>(ozellikDTO);

            foreach (var item in entity.FizikselOzellikler)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }

            foreach (var item in entity.DeneyimKodlari)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }

            foreach (var item in entity.EgitimTipleri)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }

            foreach (var item in entity.YetenekTipleri)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }

            foreach (var item in entity.PerformerEtiketleri)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }
            }

            entity.EklenmeTarihi = DateTime.Now;
            entity.Ekleyen = user.AdSoyad;
            entity.EkleyenId = user.Id;

            var result = await _projeRolDataService.YeniProjeRolOzellik(entity);
            return result ? OdiResponse<bool>.Success("Rol eklendi", true, 201)
                          : OdiResponse<bool>.Fail("Rol eklenemedi", "Error", 500);
        }

        public async Task<OdiResponse<bool>> ProjeRolOzellikGuncelle(ProjeRolOzellikUpdateDTO ozellikDTO, OdiUser user)
        {
            ProjeRolOzellik existingEntity = await _projeRolDataService.ProjeRolOzellikGetir(ozellikDTO.ProjeRolId);
            if (existingEntity == null)
                return OdiResponse<bool>.Fail("Güncellenecek rol bulunamadı", "Not Found", 404);

            _mapper.Map(ozellikDTO, existingEntity);

            existingEntity.GuncellenmeTarihi = DateTime.Now;
            existingEntity.GuncelleyenId = user.Id;
            existingEntity.Guncelleyen = user.AdSoyad;

            var fizikselOzelliklerResult = await UpdateFizikselOzellikler(existingEntity.Id, _mapper.Map<List<RolOzellikFiziksel>>(ozellikDTO.FizikselOzellikler), user);
            if (!fizikselOzelliklerResult)
                return OdiResponse<bool>.Fail("Fiziksel Özellikler güncellenemedi", "Error", 500);

            var deneyimKodlariResult = await UpdateDeneyimKodlari(existingEntity.Id, _mapper.Map<List<RolOzellikDeneyim>>(ozellikDTO.DeneyimKodlari), user);
            if (!deneyimKodlariResult)
                return OdiResponse<bool>.Fail("Deneyim Kodları güncellenemedi", "Error", 500);

            var egitimTipleriResult = await UpdateEgitimTipleri(existingEntity.Id, _mapper.Map<List<RolOzellikEgitim>>(ozellikDTO.EgitimTipleri), user);
            if (!egitimTipleriResult)
                return OdiResponse<bool>.Fail("Eğitim Tipleri güncellenemedi", "Error", 500);

            var yetenekTipleriResult = await UpdateYetenekTipleri(existingEntity.Id, _mapper.Map<List<RolOzellikYetenek>>(ozellikDTO.YetenekTipleri), user);
            if (!yetenekTipleriResult)
                return OdiResponse<bool>.Fail("Yetenek Tipleri güncellenemedi", "Error", 500);

            var performerEtiketleriResult = await UpdatePerformerEtiketleri(existingEntity.Id, _mapper.Map<List<RolOzellikPerformerEtiket>>(ozellikDTO.PerformerEtiketleri), user);
            if (!performerEtiketleriResult)
                return OdiResponse<bool>.Fail("Performer Etiketleri güncellenemedi", "Error", 500);

            existingEntity.FizikselOzellikler = null;
            existingEntity.DeneyimKodlari = null;
            existingEntity.EgitimTipleri = null;
            existingEntity.YetenekTipleri = null;
            existingEntity.PerformerEtiketleri = null;

            var result = await _projeRolDataService.ProjeRolOzellikGuncelle(existingEntity);
            return result ? OdiResponse<bool>.Success("Rol güncellendi", true, 200)
                          : OdiResponse<bool>.Fail("Rol güncellenemedi", "Error", 500);
        }

        private async Task<bool> UpdatePerformerEtiketleri(string projeRolOzellikId, List<RolOzellikPerformerEtiket> yeniPerformerEtiketleri, OdiUser user)
        {
            var mevcutPerformerEtiketleri = await _projeRolDataService.PerformerEtiketleriGetir(projeRolOzellikId);

            var eklenecekler = yeniPerformerEtiketleri
                .Where(yeni => !mevcutPerformerEtiketleri.Any(mevcut => mevcut.EtiketTipKodu == yeni.EtiketTipKodu))
                .ToList();

            var silinecekler = mevcutPerformerEtiketleri
                .Where(mevcut => !yeniPerformerEtiketleri.Any(yeni => mevcut.EtiketTipKodu == yeni.EtiketTipKodu))
                .ToList();

            if (eklenecekler.Any())
            {
                foreach (var item in eklenecekler)
                {
                    item.EklenmeTarihi = DateTime.Now;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    if (string.IsNullOrEmpty(item.Id))
                        item.Id = Guid.NewGuid().ToString();

                    if (string.IsNullOrEmpty(item.ProjeRolOzellikId))
                        item.ProjeRolOzellikId = projeRolOzellikId;
                }

                await _projeRolDataService.PerformerEtiketleriEkle(eklenecekler);
            }

            if (silinecekler.Any())
                await _projeRolDataService.PerformerEtiketleriSil(silinecekler);

            return true;
        }

        private async Task<bool> UpdateFizikselOzellikler(string projeRolOzellikId, List<RolOzellikFiziksel> yeniFizikselOzellikler, OdiUser user)
        {
            var mevcutFizikselOzellikler = await _projeRolDataService.FizikselOzellikleriGetir(projeRolOzellikId);

            var eklenecekler = yeniFizikselOzellikler
                .Where(yeni => !mevcutFizikselOzellikler.Any(mevcut =>
                    mevcut.FizikselOzellikTipiKodu == yeni.FizikselOzellikTipiKodu &&
                    mevcut.FizikselOzellikAdiKodu == yeni.FizikselOzellikAdiKodu))
                .ToList();

            var silinecekler = mevcutFizikselOzellikler
                .Where(mevcut => !yeniFizikselOzellikler.Any(yeni =>
                    mevcut.FizikselOzellikTipiKodu == yeni.FizikselOzellikTipiKodu &&
                    mevcut.FizikselOzellikAdiKodu == yeni.FizikselOzellikAdiKodu))
                .ToList();

            if (eklenecekler.Any())
            {
                foreach (var item in eklenecekler)
                {
                    item.EklenmeTarihi = DateTime.Now;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    if (string.IsNullOrEmpty(item.Id))
                        item.Id = Guid.NewGuid().ToString();

                    if (string.IsNullOrEmpty(item.ProjeRolOzellikId))
                        item.ProjeRolOzellikId = projeRolOzellikId;
                }

                await _projeRolDataService.FizikselOzellikleriEkle(eklenecekler);
            }

            if (silinecekler.Any())
                await _projeRolDataService.FizikselOzellikleriSil(silinecekler);

            return true;
        }

        private async Task<bool> UpdateDeneyimKodlari(string projeRolOzellikId, List<RolOzellikDeneyim> yeniDeneyimKodlari, OdiUser user)
        {
            var mevcutDeneyimKodlari = await _projeRolDataService.DeneyimKodlariniGetir(projeRolOzellikId);

            var eklenecekler = yeniDeneyimKodlari
                .Where(yeni => !mevcutDeneyimKodlari.Any(mevcut => mevcut.DeneyimKodu == yeni.DeneyimKodu))
                .ToList();

            var silinecekler = mevcutDeneyimKodlari
                .Where(mevcut => !yeniDeneyimKodlari.Any(yeni => mevcut.DeneyimKodu == yeni.DeneyimKodu))
                .ToList();

            if (eklenecekler.Any())
            {
                foreach (var item in eklenecekler)
                {
                    item.EklenmeTarihi = DateTime.Now;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    if (string.IsNullOrEmpty(item.Id))
                        item.Id = Guid.NewGuid().ToString();

                    if (string.IsNullOrEmpty(item.ProjeRolOzellikId))
                        item.ProjeRolOzellikId = projeRolOzellikId;
                }

                await _projeRolDataService.DeneyimKodlariEkle(eklenecekler);
            }

            if (silinecekler.Any())
                await _projeRolDataService.DeneyimKodlariSil(silinecekler);

            return true;
        }

        private async Task<bool> UpdateEgitimTipleri(string projeRolOzellikId, List<RolOzellikEgitim> yeniEgitimTipleri, OdiUser user)
        {
            var mevcutEgitimTipleri = await _projeRolDataService.EgitimTipleriniGetir(projeRolOzellikId);

            var eklenecekler = yeniEgitimTipleri
                .Where(yeni => !mevcutEgitimTipleri.Any(mevcut => mevcut.EgitimTipiId == yeni.EgitimTipiId))
                .ToList();

            var silinecekler = mevcutEgitimTipleri
                .Where(mevcut => !yeniEgitimTipleri.Any(yeni => mevcut.EgitimTipiId == yeni.EgitimTipiId))
                .ToList();

            if (eklenecekler.Any())
            {
                foreach (var item in eklenecekler)
                {
                    item.EklenmeTarihi = DateTime.Now;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    if (string.IsNullOrEmpty(item.Id))
                        item.Id = Guid.NewGuid().ToString();

                    if (string.IsNullOrEmpty(item.ProjeRolOzellikId))
                        item.ProjeRolOzellikId = projeRolOzellikId;
                }

                await _projeRolDataService.EgitimTipleriEkle(eklenecekler);
            }

            if (silinecekler.Any())
                await _projeRolDataService.EgitimTipleriSil(silinecekler);

            return true;
        }

        private async Task<bool> UpdateYetenekTipleri(string projeRolOzellikId, List<RolOzellikYetenek> yeniYetenekTipleri, OdiUser user)
        {
            var mevcutYetenekTipleri = await _projeRolDataService.YetenekTipleriniGetir(projeRolOzellikId);

            var eklenecekler = yeniYetenekTipleri
                .Where(yeni => !mevcutYetenekTipleri.Any(mevcut => mevcut.YetenekTipiKodu == yeni.YetenekTipiKodu))
                .ToList();

            var silinecekler = mevcutYetenekTipleri
                .Where(mevcut => !yeniYetenekTipleri.Any(yeni => mevcut.YetenekTipiKodu == yeni.YetenekTipiKodu))
                .ToList();

            if (eklenecekler.Any())
            {
                foreach (var item in eklenecekler)
                {
                    item.EklenmeTarihi = DateTime.Now;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    if (string.IsNullOrEmpty(item.Id))
                        item.Id = Guid.NewGuid().ToString();

                    if (string.IsNullOrEmpty(item.ProjeRolOzellikId))
                        item.ProjeRolOzellikId = projeRolOzellikId;
                }

                await _projeRolDataService.YetenekTipleriEkle(eklenecekler);
            }

            if (silinecekler.Any())
                await _projeRolDataService.YetenekTipleriSil(silinecekler);

            return true;
        }

        public async Task<OdiResponse<RolOzellikAyarlariDTO>> RolOzellikAyarlari(KayitTuruKodlariDTO kayitTuruKodlariDTO, string jwt)
        {
            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            string url = _configuration.GetSection("GatewayServerUrl").Value + "/servis/performer/rol-ozellik-ayarlari-getir";

            OdiResponse<RolOzellikAyarlariDTO> apiResult = await webApiRequest.Post<RolOzellikAyarlariDTO, KayitTuruKodlariDTO>(url, jwt, kayitTuruKodlariDTO);

            if (!apiResult.IsSuccessful)
                return OdiResponse<RolOzellikAyarlariDTO>.Fail("Bir sorun oluştu.", "Rol özellik ayarları getirilirken sorun oluştu. Hatalar: " + string.Join(",", apiResult.Errors), 500);

            return OdiResponse<RolOzellikAyarlariDTO>.Success("Rol bilgisi ayarları getirildi.", apiResult.Data, 200);
        }

        public async Task<OdiResponse<RolBilgisiAyarlariOutputDTO>> RolBilgisiAyarlari(string jwt)
        {
            RolBilgisiAyarlariOutputDTO resultDTO = new RolBilgisiAyarlariOutputDTO();

            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            string url = _configuration.GetSection("GatewayServerUrl").Value + "/servis/uygulama-bilgileri/rol-turleri-listesi";

            OdiResponse<List<RolTuruDTO>> rolTurListeApiResult = await webApiRequest.Get<List<RolTuruDTO>>(url, jwt);

            if (!rolTurListeApiResult.IsSuccessful)
                return OdiResponse<RolBilgisiAyarlariOutputDTO>.Fail("Bir sorun oluştu.", "Rol türleri listesi getirilirken sorun oluştu. Hatalar: " + string.Join(",", rolTurListeApiResult.Errors), 500);

            resultDTO.KayitTurlariListesi = rolTurListeApiResult.Data;
            resultDTO.RolAgirlikTipleriListesi = _mapper.Map<List<RolAgirlikTipiDTO>>(await _projeRolDataService.RolAgirlikTipiAktifListeGetir());

            return OdiResponse<RolBilgisiAyarlariOutputDTO>.Success("Rol bilgisi ayarları getirildi.", resultDTO, 200);
        }

        public async Task<OdiResponse<ProjeRolOutputDTO>> YeniProjeRol(ProjeRolCreateDTO projeRol, OdiUser user)
        {
            ProjeRol rol = _mapper.Map<ProjeRol>(projeRol);

            rol.EklenmeTarihi = DateTime.Now;
            rol.Ekleyen = user.AdSoyad;
            rol.EkleyenId = user.Id;

            rol.GuncellenmeTarihi = DateTime.Now;
            rol.Guncelleyen = user.AdSoyad;
            rol.GuncelleyenId = user.Id;

            rol = await _projeRolDataService.YeniProjeRol(rol);
            return OdiResponse<ProjeRolOutputDTO>.Success("Proje Rol Eklendi", _mapper.Map<ProjeRolOutputDTO>(rol), 200);
        }

        public async Task<OdiResponse<bool>> ProjeRolKopyala(ProjeRolIdDTO model, OdiUser user)
        {
            ProjeRol projeRol = await _projeRolDataService.ProjeRolGetir(model.ProjeRolId);

            if (projeRol == null) return OdiResponse<bool>.Fail("Bu id'ye ait sahip bir proje rolü bulunamaktadır.", "Not Found", 404);

            ProjeRol yeniProjeRol = await projeRol.DeepCopy();

            yeniProjeRol.Id = Guid.NewGuid().ToString();
            yeniProjeRol.RolAdi = projeRol.RolAdi + " - Kopya";

            //Bu alanlar sonra eklenecek.
            yeniProjeRol.RolOzellik = null;
            yeniProjeRol.AnketSorulari = null;

            yeniProjeRol.SetAuditFields(user);

            yeniProjeRol = await _projeRolDataService.YeniProjeRol(yeniProjeRol);

            if (yeniProjeRol == null || string.IsNullOrEmpty(yeniProjeRol.Id) || yeniProjeRol.Id == projeRol.Id)
                return OdiResponse<bool>.Fail("Yeni proje rol oluşturulurken hata oluştu.", "Bad Request", 400);

            #region Proje Rol Özellik Kopyalama İşlemleri

            List<ProjeRolOzellik> projeRolOzellikListesi = await _projeRolDataService.ProjeRolOzellikListeGetir(model.ProjeRolId);

            if (projeRolOzellikListesi?.Any() == true)
            {
                List<ProjeRolOzellik> newProjeRolOzellikListesi = await projeRolOzellikListesi.DeepCopy();

                foreach (ProjeRolOzellik newProjeRolOzellik in newProjeRolOzellikListesi)
                {
                    newProjeRolOzellik.Id = Guid.NewGuid().ToString();

                    newProjeRolOzellik.ProjeRolId = yeniProjeRol.Id;

                    newProjeRolOzellik.SetAuditFields(user);

                    if (newProjeRolOzellik.FizikselOzellikler?.Any() == true)
                    {
                        foreach (var itemFizikselOzellik in newProjeRolOzellik.FizikselOzellikler)
                        {
                            itemFizikselOzellik.ProjeRolOzellikId = string.Empty;

                            itemFizikselOzellik.Id = Guid.NewGuid().ToString();

                            itemFizikselOzellik.SetAuditFields(user);
                        }
                    }

                    if (newProjeRolOzellik.DeneyimKodlari?.Any() == true)
                    {
                        foreach (var itemDeneyimKodlari in newProjeRolOzellik.DeneyimKodlari)
                        {
                            itemDeneyimKodlari.ProjeRolOzellikId = string.Empty;

                            itemDeneyimKodlari.Id = Guid.NewGuid().ToString();

                            itemDeneyimKodlari.SetAuditFields(user);
                        }
                    }

                    if (newProjeRolOzellik.EgitimTipleri?.Any() == true)
                    {
                        foreach (var itemEgitimTipleri in newProjeRolOzellik.EgitimTipleri)
                        {
                            itemEgitimTipleri.ProjeRolOzellikId = string.Empty;

                            itemEgitimTipleri.Id = Guid.NewGuid().ToString();

                            itemEgitimTipleri.SetAuditFields(user);
                        }
                    }
                }

                await _projeRolDataService.YeniProjeRolOzellik(newProjeRolOzellikListesi);
            }

            #endregion

            #region Proje Rol Anket Soruları Kopyalama İşlemleri

            List<ProjeRolAnketSorusu> projeRolAnketSorusuListesi = await _projeRolDataService.ProjeRolAnketSorusuListeGetir(model.ProjeRolId);

            if (projeRolOzellikListesi?.Any() == true)
            {
                List<ProjeRolAnketSorusu> newProjeRolAnketSorusuListesi = await projeRolAnketSorusuListesi.DeepCopy();

                foreach (var item in newProjeRolAnketSorusuListesi)
                {
                    item.Id = Guid.NewGuid().ToString();

                    item.ProjeRolId = yeniProjeRol.Id;

                    item.SetAuditFields(user);
                }

                await _projeRolDataService.YeniProjeRolAnketSorusu(newProjeRolAnketSorusuListesi);
            }

            #endregion

            #region Proje Rol Odileri Kopyalama İşlemleri

            List<ProjeRolOdi> projeRolOdiListesi = await _projeRolOdiDataService.ProjeRolOdiListeGetir(model.ProjeRolId);

            if (projeRolOdiListesi?.Any() == true)
            {
                foreach (ProjeRolOdi projeRolOdi in projeRolOdiListesi)
                {
                    ProjeRolOdi newProjeRolOdi = await projeRolOdi.DeepCopy();

                    newProjeRolOdi.Id = Guid.NewGuid().ToString();

                    newProjeRolOdi.ProjeRolId = yeniProjeRol.Id;

                    newProjeRolOdi.SetAuditFields(user);

                    newProjeRolOdi = await _projeRolOdiDataService.YeniProjeRolOdi(newProjeRolOdi);

                    #region RolOdiFotoOrnekFotograflar Kopyalama İşlemleri

                    List<RolOdiFotoOrnekFotograf> rolOdiFotoOrnekFotografListesi = await _projeRolOdiDataService.RolOdiFotoOrnekFotoListesiGetir(projeRolOdi.Id);

                    if (rolOdiFotoOrnekFotografListesi?.Any() == true)
                    {
                        List<RolOdiFotoOrnekFotograf> newRolOdiFotoOrnekFotografListesi = await rolOdiFotoOrnekFotografListesi.DeepCopy();

                        foreach (RolOdiFotoOrnekFotograf newRolOdiFotoOrnekFotograf in newRolOdiFotoOrnekFotografListesi)
                        {
                            newRolOdiFotoOrnekFotograf.Id = Guid.NewGuid().ToString();

                            newRolOdiFotoOrnekFotograf.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiFotoOrnekFotograf.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiFotoOrnekFotolar(newRolOdiFotoOrnekFotografListesi);
                    }

                    #endregion

                    #region RolOdiFotoPozlar Kopyalama İşlemleri

                    List<RolOdiFotoPoz> rolOdiFotoPozListesi = await _projeRolOdiDataService.RolOdiFotoPozListesiGetir(projeRolOdi.Id);

                    if (rolOdiFotoPozListesi?.Any() == true)
                    {
                        List<RolOdiFotoPoz> newRolOdiFotoPozListesi = await rolOdiFotoPozListesi.DeepCopy();

                        foreach (RolOdiFotoPoz newRolOdiFotoPoz in newRolOdiFotoPozListesi)
                        {
                            newRolOdiFotoPoz.Id = Guid.NewGuid().ToString();

                            newRolOdiFotoPoz.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiFotoPoz.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiFotoPozList(newRolOdiFotoPozListesi);
                    }

                    #endregion

                    #region RolOdiFotoYonetmenNotlari Kopyalama İşlemleri

                    List<RolOdiFotoYonetmenNotu> rolOdiFotoYonetmenNotListesi = await _projeRolOdiDataService.RolOdiFotoYonetmenNotuListesiGetir(projeRolOdi.Id);

                    if (rolOdiFotoYonetmenNotListesi?.Any() == true)
                    {
                        List<RolOdiFotoYonetmenNotu> newRolOdiFotoYonetmenNotListesi = await rolOdiFotoYonetmenNotListesi.DeepCopy();

                        foreach (RolOdiFotoYonetmenNotu newRolOdiFotoYonetmenNot in newRolOdiFotoYonetmenNotListesi)
                        {
                            newRolOdiFotoYonetmenNot.Id = Guid.NewGuid().ToString();

                            newRolOdiFotoYonetmenNot.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiFotoYonetmenNot.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiFotoYonetmenNotu(newRolOdiFotoYonetmenNotListesi);
                    }

                    #endregion

                    #region RolOdiSesler Kopyalama İşlemleri

                    List<RolOdiSes> rolOdiSesListesi = await _projeRolOdiDataService.RolOdiSesListesiGetir(projeRolOdi.Id);

                    if (rolOdiSesListesi?.Any() == true)
                    {
                        List<RolOdiSes> newRolOdiSesListesi = await rolOdiSesListesi.DeepCopy();

                        foreach (RolOdiSes newRolOdiSes in newRolOdiSesListesi)
                        {
                            newRolOdiSes.Id = Guid.NewGuid().ToString();

                            newRolOdiSes.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiSes.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiSesList(newRolOdiSesListesi);
                    }

                    #endregion

                    #region RolOdiSesSenaryolar Kopyalama İşlemleri

                    List<RolOdiSesSenaryo> rolOdiSesSenaryoListesi = await _projeRolOdiDataService.RolOdiSesSenaryoListesiGetir(projeRolOdi.Id);

                    if (rolOdiSesSenaryoListesi?.Any() == true)
                    {
                        List<RolOdiSesSenaryo> newRolOdiSesSenaryoListesi = await rolOdiSesSenaryoListesi.DeepCopy();

                        foreach (RolOdiSesSenaryo newRolOdiSesSenaryo in newRolOdiSesSenaryoListesi)
                        {
                            newRolOdiSesSenaryo.Id = Guid.NewGuid().ToString();

                            newRolOdiSesSenaryo.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiSesSenaryo.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiSesSenaryo(newRolOdiSesSenaryoListesi);
                    }

                    #endregion

                    #region RolOdiSesYonetmenNotlari Kopyalama İşlemleri

                    List<RolOdiSesYonetmenNotu> rolOdiSesYonetmenNotListesi = await _projeRolOdiDataService.RolOdiSesYonetmenNotuListesiGetir(projeRolOdi.Id);

                    if (rolOdiSesYonetmenNotListesi?.Any() == true)
                    {
                        List<RolOdiSesYonetmenNotu> newRolOdiSesYonetmenNotListesi = await rolOdiSesYonetmenNotListesi.DeepCopy();

                        foreach (RolOdiSesYonetmenNotu newRolOdiSesYonetmenNot in newRolOdiSesYonetmenNotListesi)
                        {
                            newRolOdiSesYonetmenNot.Id = Guid.NewGuid().ToString();

                            newRolOdiSesYonetmenNot.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiSesYonetmenNot.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiSesYonetmenNotu(newRolOdiSesYonetmenNotListesi);
                    }

                    #endregion

                    #region RolOdiSoruAciklamalar Kopyalama İşlemleri

                    List<RolOdiSoruAciklama> rolOdiSoruAciklamaListesi = await _projeRolOdiDataService.RolOdiSoruAciklamaListesiGetir(projeRolOdi.Id);

                    if (rolOdiSoruAciklamaListesi?.Any() == true)
                    {
                        List<RolOdiSoruAciklama> newRolOdiSoruAciklamaListesi = await rolOdiSoruAciklamaListesi.DeepCopy();

                        foreach (RolOdiSoruAciklama newRolOdiSoruAciklama in newRolOdiSoruAciklamaListesi)
                        {
                            newRolOdiSoruAciklama.Id = Guid.NewGuid().ToString();

                            newRolOdiSoruAciklama.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiSoruAciklama.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiSoruAciklama(newRolOdiSoruAciklamaListesi);
                    }

                    #endregion

                    #region RolOdiSorular Kopyalama İşlemleri

                    List<RolOdiSoru> rolOdiSorular = await _projeRolOdiDataService.RolOdiSoruListGetir(projeRolOdi.Id);

                    if (rolOdiSorular?.Any() == true)
                    {
                        List<RolOdiSoru> newRolOdiSorular = await rolOdiSorular.DeepCopy();

                        foreach (RolOdiSoru newRolOdiSoru in newRolOdiSorular)
                        {
                            newRolOdiSoru.Id = Guid.NewGuid().ToString();

                            newRolOdiSoru.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiSoru.SetAuditFields(user);

                            #region RolOdiSoruCevapSecenekleri Kopyalama İşlemleri

                            if (newRolOdiSoru.CevapSecenekleri?.Any() == true)
                            {
                                foreach (RolOdiSoruCevapSecenek cevapSecenek in newRolOdiSoru.CevapSecenekleri)
                                {
                                    cevapSecenek.RolOdiSoruId = string.Empty;

                                    cevapSecenek.Id = Guid.NewGuid().ToString();

                                    cevapSecenek.SetAuditFields(user);
                                }
                            }

                            #endregion
                        }

                        await _projeRolOdiDataService.YeniRolOdiSoruList(newRolOdiSorular);
                    }

                    #endregion

                    #region RolOdiVideolar Kopyalama İşlemleri

                    List<RolOdiVideo> rolOdiVideolar = await _projeRolOdiDataService.RolOdiVideoListesiGetir(projeRolOdi.Id);

                    if (rolOdiVideolar?.Any() == true)
                    {
                        List<RolOdiVideo> newRolOdiVideolar = await rolOdiVideolar.DeepCopy();

                        foreach (RolOdiVideo newRolOdiVideo in newRolOdiVideolar)
                        {
                            newRolOdiVideo.Id = Guid.NewGuid().ToString();

                            newRolOdiVideo.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiVideo.SetAuditFields(user);

                            #region RolOdiVideoDetaylar Kopyalama İşlemleri

                            if (newRolOdiVideo.DetayList?.Any() == true)
                            {
                                foreach (RolOdiVideoDetay videoDetay in newRolOdiVideo.DetayList)
                                {
                                    videoDetay.RolOdiVideoId = string.Empty;

                                    videoDetay.Id = Guid.NewGuid().ToString();

                                    videoDetay.SetAuditFields(user);
                                }
                            }

                            #endregion
                        }

                        await _projeRolOdiDataService.YeniRolOdiVideo(newRolOdiVideolar);
                    }

                    #endregion

                    #region RolOdiVideoOrnekOyunlar Kopyalama İşlemleri

                    List<RolOdiVideoOrnekOyun> rolOdiVideoOrnekOyunlar = await _projeRolOdiDataService.RolOdiVideoOrnekOyunListGetir(projeRolOdi.Id);

                    if (rolOdiVideoOrnekOyunlar?.Any() == true)
                    {
                        List<RolOdiVideoOrnekOyun> newRolOdiVideoOrnekOyunlar = await rolOdiVideoOrnekOyunlar.DeepCopy();

                        foreach (RolOdiVideoOrnekOyun newRolOdiVideoOrnekOyun in newRolOdiVideoOrnekOyunlar)
                        {
                            newRolOdiVideoOrnekOyun.Id = Guid.NewGuid().ToString();

                            newRolOdiVideoOrnekOyun.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiVideoOrnekOyun.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiVideoOrnekOyunList(newRolOdiVideoOrnekOyunlar);
                    }

                    #endregion

                    #region RolOdiVideoSenaryolar Kopyalama İşlemleri

                    List<RolOdiVideoSenaryo> rolOdiVideoSenaryolar = await _projeRolOdiDataService.RolOdiVideoSenaryoListesiGetir(projeRolOdi.Id);

                    if (rolOdiVideoSenaryolar?.Any() == true)
                    {
                        List<RolOdiVideoSenaryo> newRolOdiVideoSenaryolar = await rolOdiVideoSenaryolar.DeepCopy();

                        foreach (RolOdiVideoSenaryo newRolOdiVideoSenaryo in newRolOdiVideoSenaryolar)
                        {
                            newRolOdiVideoSenaryo.Id = Guid.NewGuid().ToString();

                            newRolOdiVideoSenaryo.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiVideoSenaryo.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiVideoSenaryo(newRolOdiVideoSenaryolar);
                    }

                    #endregion

                    #region RolOdiVideoYonetmenNotlari Kopyalama İşlemleri

                    List<RolOdiVideoYonetmenNotu> rolOdiVideoYonetmenNotlari = await _projeRolOdiDataService.RolOdiVideoYonetmenNotuListesiGetir(projeRolOdi.Id);

                    if (rolOdiVideoYonetmenNotlari?.Any() == true)
                    {
                        List<RolOdiVideoYonetmenNotu> newRolOdiVideoYonetmenNotlari = await rolOdiVideoYonetmenNotlari.DeepCopy();

                        foreach (RolOdiVideoYonetmenNotu newRolOdiVideoYonetmenNotu in newRolOdiVideoYonetmenNotlari)
                        {
                            newRolOdiVideoYonetmenNotu.Id = Guid.NewGuid().ToString();

                            newRolOdiVideoYonetmenNotu.ProjeRolOdiId = newProjeRolOdi.Id;

                            newRolOdiVideoYonetmenNotu.SetAuditFields(user);
                        }

                        await _projeRolOdiDataService.YeniRolOdiVideoYonetmenNotu(newRolOdiVideoYonetmenNotlari);
                    }

                    #endregion
                }
            }

            #endregion

            return OdiResponse<bool>.Success("Proje rol kopyalandı.", true, 200);
        }

        public async Task<OdiResponse<ProjeRolOutputDTO>> ProjeRolGuncelle(ProjeRolUpdateDTO rolDTO, OdiUser user)
        {
            ProjeRol rol = await _projeRolDataService.ProjeRolGetir(rolDTO.ProjeRolId);

            //List<ProjeRolAnketSorusu> eskiSoruListesi = rol.AnketSorulari;
            //List<ProjeRolAnketSorusu> guncellenecekSoruListesi = new List<ProjeRolAnketSorusu>();
            //List<ProjeRolAnketSorusu> yeniSoruListesi = new List<ProjeRolAnketSorusu>();

            if (rol == null) return OdiResponse<ProjeRolOutputDTO>.Fail("Bu id'ye ait sahip bir proje rolü bulunamaktadır.", "Bad Request", 400);

            rol = _mapper.Map<ProjeRolUpdateDTO, ProjeRol>(rolDTO);

            rol.GuncellenmeTarihi = DateTime.Now;
            rol.GuncelleyenId = user.Id;
            rol.Guncelleyen = user.AdSoyad;

            //rol.RolOzellik.GuncellenmeTarihi = DateTime.Now;
            //rol.RolOzellik.GuncelleyenId = user.Id;
            //rol.RolOzellik.Guncelleyen = user.AdSoyad;

            //foreach (var soru in rol.AnketSorulari)
            //{
            //    soru.GuncellenmeTarihi = DateTime.Now;
            //    soru.GuncelleyenId = user.Id;
            //    soru.Guncelleyen = user.AdSoyad;
            //}

            rol = await _projeRolDataService.ProjeRolGuncelle(rol);

            //guncellenecekSoruListesi = _mapper.Map<List<ProjeRolAnketSorusu>>(rolDTO.AnketSorulari);
            //List<ProjeRolAnketSorusu> silinenSorular = new List<ProjeRolAnketSorusu>();
            //foreach (var item in eskiSoruListesi)
            //{
            //    if (!guncellenecekSoruListesi.Any(x => x.Id == item.Id)) silinenSorular.Add(item);
            //}
            //if (silinenSorular != null)
            //{
            //    await _projeRolDataService.ProjeRolAnketSorulariSil(silinenSorular);
            //}

            //if (rolDTO.YeniAnketSorulari != null)
            //{
            //    yeniSoruListesi = _mapper.Map<List<ProjeRolAnketSorusu>>(rolDTO.YeniAnketSorulari);
            //    foreach (var soru in yeniSoruListesi)
            //    {
            //        soru.Id = Guid.NewGuid().ToString();
            //        soru.Ekleyen = user.AdSoyad;
            //        soru.EkleyenId = user.Id;
            //        soru.EklenmeTarihi = DateTime.Now;
            //        soru.Guncelleyen = user.AdSoyad;
            //        soru.GuncelleyenId = user.Id;
            //        soru.GuncellenmeTarihi = DateTime.Now;
            //    }
            //    yeniSoruListesi = await _projeRolDataService.YeniProjeRolAnketSorulari(yeniSoruListesi);

            //    return OdiResponse<ProjeRolOutputDTO>.Success("Proje Rol Güncellendi", await this.ProjeRolGetir(rolDTO.ProjeRolId), 200);
            //}

            return OdiResponse<ProjeRolOutputDTO>.Success("Proje Rol Güncellendi.", _mapper.Map<ProjeRolOutputDTO>(rol), 200);
        }

        public async Task<OdiResponse<bool>> YeniProjeRolAnketSorusu(List<ProjeRolAnketSorusuCreateDTO> anketSorusuCreateDTOList, OdiUser user)
        {
            List<ProjeRolAnketSorusu> anketSorusuList = _mapper.Map<List<ProjeRolAnketSorusu>>(anketSorusuCreateDTOList);

            foreach (var soru in anketSorusuList)
            {
                soru.EklenmeTarihi = DateTime.Now;
                soru.EkleyenId = user.Id;
                soru.Ekleyen = user.AdSoyad;

                soru.GuncellenmeTarihi = DateTime.Now;
                soru.GuncelleyenId = user.Id;
                soru.Guncelleyen = user.AdSoyad;
            }

            await _projeRolDataService.YeniProjeRolAnketSorusu(anketSorusuList);

            return OdiResponse<bool>.Success("Proje Rol Anket Sorulari Eklendi", true, 200);
        }

        public async Task<OdiResponse<bool>> ProjeRolAnketSorusuGuncelle(List<ProjeRolAnketSorusuUpdateDTO> anketSorusuUpdateDTOList, OdiUser user)
        {
            List<ProjeRolAnketSorusu> projeRolAnketSorusuList = await _projeRolDataService.ProjeRolAnketSorusuListeGetir(anketSorusuUpdateDTOList.Select(s => s.ProjeRolAnketSorusuId).ToList());

            if (projeRolAnketSorusuList?.Any() == false) return OdiResponse<bool>.Fail("Kayıtlar bulunamadı.", "Bad Request", 400);

            projeRolAnketSorusuList = _mapper.Map<List<ProjeRolAnketSorusuUpdateDTO>, List<ProjeRolAnketSorusu>>(anketSorusuUpdateDTOList);

            foreach (var soru in projeRolAnketSorusuList)
            {
                soru.GuncellenmeTarihi = DateTime.Now;
                soru.GuncelleyenId = user.Id;
                soru.Guncelleyen = user.AdSoyad;
            }

            await _projeRolDataService.ProjeRolAnketSorusuGuncelle(projeRolAnketSorusuList);

            return OdiResponse<bool>.Success("Anket soruları güncellendi.", true, 200);
        }

        public async Task<OdiResponse<bool>> ProjeRolAnketSorusuSil(List<ProjeRolAnketSorusuIdDTO> anketSorusuIdDTOList)
        {
            List<ProjeRolAnketSorusu> projeRolAnketSorusuList = await _projeRolDataService.ProjeRolAnketSorusuListeGetir(anketSorusuIdDTOList.Select(x => x.ProjeRolAnketSorusuId).ToList());

            if (projeRolAnketSorusuList?.Any() == false) return OdiResponse<bool>.Fail("Kayıtlar bulunamadı.", "Bad Request", 400);

            await _projeRolDataService.ProjeRolAnketSorusuSil(projeRolAnketSorusuList);

            return OdiResponse<bool>.Success("Anket soruları silindi.", true, 200);
        }

        public async Task<OdiResponse<List<ProjeRolAnketSorusuOutputDTO>>> ProjeRolAnketSorusuListeGetir(ProjeRolIdDTO model)
        {
            List<ProjeRolAnketSorusu> sorular = await _projeRolDataService.ProjeRolAnketSorusuListeGetir(model.ProjeRolId);

            return OdiResponse<List<ProjeRolAnketSorusuOutputDTO>>.Success("Proje rol performer listesi getirildi.", _mapper.Map<List<ProjeRolAnketSorusuOutputDTO>>(sorular), 200);
        }

        public async Task<List<ProjeRolOutputDTO>> ProjeRolleriGetir(string projeId)
        {
            List<ProjeRolOutputDTO> rolList = new List<ProjeRolOutputDTO>();
            List<ProjeRol> roller = await _projeRolDataService.ProjeRolleriGetir(projeId);
            if (roller != null) rolList = _mapper.Map<List<ProjeRolOutputDTO>>(roller);
            foreach (var item in rolList)
            {
                item.RolOdi = await _projeRolOdiLogicService.ProjeRolOdiGetir(item.ProjeRolId);
            }
            return rolList;
        }

        public async Task<ProjeRolOutputDTO> ProjeRolGetir(string projeRolId)
        {
            ProjeRolOutputDTO rolDTO = new ProjeRolOutputDTO();
            ProjeRol rol = await _projeRolDataService.ProjeRolGetir(projeRolId);
            if (rol != null) rolDTO = _mapper.Map<ProjeRolOutputDTO>(rol);
            return rolDTO;
        }

        public async Task<OdiResponse<AlternatifButceOutputDTO>> AlternatifButceGetir(ProjeRolIdDTO projeRolIdDTO)
        {
            ProjeRol entity = await _projeRolDataService.ProjeRolGetir(projeRolIdDTO.ProjeRolId);

            if (entity == null)
                return OdiResponse<AlternatifButceOutputDTO>.Fail("Proje rol bulunamadı", "Not Found", 404);

            return OdiResponse<AlternatifButceOutputDTO>.Success("Rol getirildi", new AlternatifButceOutputDTO() { AlternatifButce = entity.AlternatifButce }, 200);
        }

        public async Task<OdiResponse<bool>> AlternatifButceGuncelle(AlternatifButceUpdateDTO model, OdiUser user)
        {
            ProjeRol entity = await _projeRolDataService.ProjeRolGetir(model.ProjeRolId);

            if (entity == null)
                return OdiResponse<bool>.Fail("Proje rol bulunamadı", "Not Found", 404);

            entity.GuncellenmeTarihi = DateTime.Now;
            entity.GuncelleyenId = user.Id;
            entity.Guncelleyen = user.AdSoyad;

            entity.AlternatifButce = model.AlternatifButce;

            var result = await _projeRolDataService.ProjeRolGuncelle(entity);

            return !string.IsNullOrEmpty(result.Id) ? OdiResponse<bool>.Success("Rol güncellendi", true, 200)
                          : OdiResponse<bool>.Fail("Rol güncellenemedi", "Error", 500);
        }

        public async Task<OdiResponse<List<ProjeRolPerformerOutputDTO>>> YeniProjeRolPerformer(ProjeRolPerformerCreateDTO model, OdiUser user)
        {
            ProjeRolPerformer projeRolPerformer = _mapper.Map<ProjeRolPerformer>(model);

            projeRolPerformer.EklenmeTarihi = DateTime.Now;
            projeRolPerformer.Ekleyen = user.AdSoyad;
            projeRolPerformer.EkleyenId = user.Id;

            projeRolPerformer.GuncellenmeTarihi = DateTime.Now;
            projeRolPerformer.Guncelleyen = user.AdSoyad;
            projeRolPerformer.GuncelleyenId = user.Id;

            projeRolPerformer = await _projeRolDataService.YeniProjeRolPerformer(projeRolPerformer);

            return OdiResponse<List<ProjeRolPerformerOutputDTO>>.Success("Yeni proje rol performer oluşturuldu.", await _projeRolDataService.ProjeRolPerformerListGetir(projeRolPerformer.ProjeId), 200);
        }

        public async Task<OdiResponse<List<ProjeRolPerformerOutputDTO>>> ProjeRolPerformerListele(ProjeIdDTO model)
        {
            List<ProjeRolPerformerOutputDTO> projeRolPerformer = await _projeRolDataService.ProjeRolPerformerListGetir(model.ProjeId);
            if (projeRolPerformer?.Any() == false) return OdiResponse<List<ProjeRolPerformerOutputDTO>>.Fail("Bu id'ye ait kayıt bulunmamaktadır", "Bad Request", 400);

            return OdiResponse<List<ProjeRolPerformerOutputDTO>>.Success("Proje rol performer listesi getirildi.", projeRolPerformer, 200);
        }

        public async Task<OdiResponse<List<ProjeRolOutputDTO>>> ProjeRolleriGetir(ProjeIdDTO id)
        {
            List<ProjeRolOutputDTO> roller = await this.ProjeRolleriGetir(id.ProjeId);
            if (roller == null) return OdiResponse<List<ProjeRolOutputDTO>>.Fail("Bu id'li projede rol kaydı bulunmamakadır", "Not Found", 404);
            return OdiResponse<List<ProjeRolOutputDTO>>.Success("Proje Rolleri Getirildi", roller, 200);
        }

        public async Task<OdiResponse<ProjeRolOutputDTO>> ProjeRolGetir(ProjeRolIdDTO id)
        {
            ProjeRolOutputDTO rolDTO = await this.ProjeRolGetir(id.ProjeRolId);
            if (rolDTO == null) return OdiResponse<ProjeRolOutputDTO>.Fail("Bu id'ye ait rol kaydı bulunmamaktadır", "Bad Request", 400);
            return OdiResponse<ProjeRolOutputDTO>.Success("Proje Rolü Getirildi", rolDTO, 200);
        }

        public async Task<OdiResponse<List<ProjeRolOpsiyonDetayOutputDTO>>> ProjeRolOpsiyonDetay(ProjeIdDTO id)
        {
            List<ProjeRolOpsiyonDetayOutputDTO> result = new List<ProjeRolOpsiyonDetayOutputDTO>();
            List<ProjeRol> rolList = await _projeRolDataService.ProjeRolleriGetir(id.ProjeId);
            if (rolList?.Any() != true) return OdiResponse<List<ProjeRolOpsiyonDetayOutputDTO>>.Fail("Bu id'ye ait rol kaydı bulunmamaktadır", "Bad Request", 400);
            else result = _mapper.Map<List<ProjeRolOpsiyonDetayOutputDTO>>(rolList);

            return OdiResponse<List<ProjeRolOpsiyonDetayOutputDTO>>.Success("Proje Rol Opsiyon Detay getirildi", result, 200);
        }
    }
}