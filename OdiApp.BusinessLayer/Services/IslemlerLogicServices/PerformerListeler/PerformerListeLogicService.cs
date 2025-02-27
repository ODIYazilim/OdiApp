using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.IslemlerDataServices.PerformerListeler;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.IslemlerDTOs.PerformerListeler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.IslemlerModels.PerformerListeler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerListeler
{
    public class PerformerListeLogicService : IPerformerListeLogicService
    {
        private readonly IPerformerListeDataService _performerListeDataService;
        private readonly IMapper _mapper;
        private readonly IUseOtherService _useOtherService;
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IHttpClientFactory _httpClientFactory;

        public PerformerListeLogicService(IPerformerListeDataService performerListeDataService, IMapper mapper, IUseOtherService useOtherService, IConfiguration configuration, IAmazonS3Service amazonS3Service, IHttpClientFactory httpClientFactory)
        {
            _performerListeDataService = performerListeDataService;
            _mapper = mapper;
            _useOtherService = useOtherService;
            _configuration = configuration;
            _amazonS3Service = amazonS3Service;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OdiResponse<PerformerListeListesiOutputDTO>> PerformerListeListesi(KullaniciIdDTO kullaniciIdDTO)
        {
            PerformerListeListesiOutputDTO result = new PerformerListeListesiOutputDTO();
            List<PerformerListe> liste = await _performerListeDataService.PerformerListeListesi(kullaniciIdDTO.KullaniciId);
            result.PerformerListeleri = _mapper.Map<List<PerformerListeDTO>>(liste);
            return OdiResponse<PerformerListeListesiOutputDTO>.Success("Liste getirildi.", result, 200);
        }

        public async Task<OdiResponse<PerformerListeWithDetayOutputDTO>> PerformerListeWithPerformerDetay(PerformerListeIdDTO performerListeIdDTO, string jwt)
        {
            PerformerListe performerListe = await _performerListeDataService.PerformerListeGetirWithDetay(performerListeIdDTO.PerformerListeId);
            if (performerListe == null) return OdiResponse<PerformerListeWithDetayOutputDTO>.Fail("Bu id ile bir liste bulunamadı", "Not Found", 404);

            List<string> performerIdList = performerListe.Performerlar.Select(s => s.PerformerId).ToList();

            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/performer-bilgileri-liste";
            OdiResponse<List<KullaniciBilgileriDTO>> apiResult = await webApiRequest.Post<List<KullaniciBilgileriDTO>, List<string>>(url, jwt, performerIdList);

            PerformerListeWithDetayOutputDTO result = new PerformerListeWithDetayOutputDTO();
            result.ListeId = performerListe.Id;
            result.ListeAdi = performerListe.ListeAdi;
            result.Performerlar = new List<PerformerListeDetayDisplayInfoDTO>();

            if (apiResult.IsSuccessful && apiResult.Data?.Any() == true)
            {
                List<KullaniciIdDTO> kullaniciIdDTOList = performerIdList.Select(s => new KullaniciIdDTO() { KullaniciId = s }).ToList();

                url = _configuration.GetSection("PerformerServerUrl").Value + "/yetenek-temsilcisi/performer-menajer-listesi-getir";
                OdiResponse<List<PerformerMenajerListItemOutputDTO>> performerMenajerIdListResult = await webApiRequest.Post<List<PerformerMenajerListItemOutputDTO>, List<KullaniciIdDTO>>(url, jwt, kullaniciIdDTOList);

                foreach (var item in performerListe.Performerlar)
                {
                    KullaniciBilgileriDTO? kullaniciBilgileriDTO = apiResult?.Data?.FirstOrDefault(f => f.Id == item.PerformerId);
                    if (kullaniciBilgileriDTO == null) continue;

                    PerformerListeDetayDisplayInfoDTO performerListeDetayDisplayInfoDTO = _mapper.Map<PerformerListeDetayDisplayInfoDTO>(kullaniciBilgileriDTO);
                    performerListeDetayDisplayInfoDTO.PerformerListeDetayId = item.Id;

                    performerListeDetayDisplayInfoDTO.ProfilFotografi = string.IsNullOrEmpty(performerListeDetayDisplayInfoDTO.ProfilFotografiDosyaYolu) ? "" : _amazonS3Service.GetPreSignedUrl(performerListeDetayDisplayInfoDTO.ProfilFotografiDosyaYolu);

                    if (performerMenajerIdListResult.IsSuccessful && performerListeDetayDisplayInfoDTO.KayitGrubuKoduListesi.Contains(KayitGrupKodlari.Yetenek))
                    {
                        performerListeDetayDisplayInfoDTO.MenajerId = performerMenajerIdListResult.Data.Where(w => w.PerformerId == performerListeDetayDisplayInfoDTO.Id).FirstOrDefault()?.MenajerId ?? string.Empty;
                        performerListeDetayDisplayInfoDTO.MenajerAdSoyad = performerMenajerIdListResult.Data.Where(w => w.PerformerId == performerListeDetayDisplayInfoDTO.Id).FirstOrDefault()?.MenajerAdSoyad ?? string.Empty;
                    }

                    result.Performerlar.Add(performerListeDetayDisplayInfoDTO);
                }
            }
            else
            {
                return OdiResponse<PerformerListeWithDetayOutputDTO>.Fail("Kullanıcı bilgileri alınamadı.", apiResult.Errors, 400);
            }

            return OdiResponse<PerformerListeWithDetayOutputDTO>.Success("Liste getirildi.", result, 200);
        }

        public async Task<OdiResponse<PerformerListeOutputDTO>> YeniPerformerListe(PerformerListeCreateDTO performerListeDTO, OdiUser user)
        {
            PerformerListe performerListe = _mapper.Map<PerformerListe>(performerListeDTO);

            DateTime date = DateTime.Now;

            performerListe.EklenmeTarihi = date;
            performerListe.Ekleyen = user.AdSoyad;
            performerListe.EkleyenId = user.Id;

            performerListe.GuncellenmeTarihi = date;
            performerListe.Guncelleyen = user.AdSoyad;
            performerListe.GuncelleyenId = user.Id;

            if (performerListe?.Performerlar?.Any() == true)
            {
                foreach (var item in performerListe.Performerlar)
                {
                    item.EklenmeTarihi = date;
                    item.Ekleyen = user.AdSoyad;
                    item.EkleyenId = user.Id;

                    item.GuncellenmeTarihi = date;
                    item.Guncelleyen = user.AdSoyad;
                    item.GuncelleyenId = user.Id;
                }
            }

            performerListe = await _performerListeDataService.YeniPerformerListe(performerListe);

            return OdiResponse<PerformerListeOutputDTO>.Success("Yeni liste oluşturuldu.", _mapper.Map<PerformerListeOutputDTO>(performerListe), 200);
        }

        public async Task<OdiResponse<PerformerListeOutputDTO>> PerformerListeGuncelle(PerformerListeUpdateDTO yeniperformerList, OdiUser user)
        {
            PerformerListe performerListe = await _performerListeDataService.PerformerListeGetir(yeniperformerList.PerformerListeId);
            if (performerListe == null) return OdiResponse<PerformerListeOutputDTO>.Fail("Bu id ile bir liste bulunamadı", "Not Found", 404);

            performerListe = _mapper.Map(yeniperformerList, performerListe);

            performerListe.GuncellenmeTarihi = DateTime.Now;
            performerListe.Guncelleyen = user.AdSoyad;
            performerListe.GuncelleyenId = user.Id;

            performerListe = await _performerListeDataService.PerformerListeGuncelle(performerListe);

            performerListe = await _performerListeDataService.PerformerListeGetirWithDetay(performerListe.Id);

            return OdiResponse<PerformerListeOutputDTO>.Success("Liste güncellendi.", _mapper.Map<PerformerListeOutputDTO>(performerListe), 200);
        }

        public async Task<OdiResponse<NoContent>> PerformerListeSil(PerformerListeIdDTO performerListeId)
        {
            bool result = await _performerListeDataService.PerformerListeSil(performerListeId.PerformerListeId);
            if (result) return OdiResponse<NoContent>.Success("Liste silindi.", 200);
            else return OdiResponse<NoContent>.Fail("Liste bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }

        public async Task<OdiResponse<PerformerListeOutputDTO>> YeniPerformerListeDetay(List<PerformerListeDetayCreateDTO> performerListeDetayListDTO, OdiUser user)
        {
            List<PerformerListeDetay> performerListeDetayList = _mapper.Map<List<PerformerListeDetay>>(performerListeDetayListDTO);

            foreach (var performerListeDetay in performerListeDetayList)
            {
                performerListeDetay.EklenmeTarihi = DateTime.Now;
                performerListeDetay.Ekleyen = user.AdSoyad;
                performerListeDetay.EkleyenId = user.Id;

                performerListeDetay.GuncellenmeTarihi = DateTime.Now;
                performerListeDetay.Guncelleyen = user.AdSoyad;
                performerListeDetay.GuncelleyenId = user.Id;
            }

            performerListeDetayList = await _performerListeDataService.YeniPerformerListeDetay(performerListeDetayList);

            PerformerListe performerListe = await _performerListeDataService.PerformerListeGetirWithDetay(performerListeDetayList.FirstOrDefault().PerformerListeId);

            return OdiResponse<PerformerListeOutputDTO>.Success("Listeye eklendi.", _mapper.Map<PerformerListeOutputDTO>(performerListe), 200);
        }

        public async Task<OdiResponse<PerformerListeOutputDTO>> PerformerListeDetaySil(List<PerformerListeDetayIdDTO> performerListDetayIdList)
        {
            List<PerformerListeDetay> performerListeDetayList = await _performerListeDataService.PerformerListeDetayGetir(performerListDetayIdList.Select(s => s.PerformerListeDetayId).ToList());

            bool result = false;

            if (performerListeDetayList?.Any() == true)
                result = await _performerListeDataService.PerformerListeDetaySil(performerListeDetayList);

            PerformerListe performerListe = await _performerListeDataService.PerformerListeGetirWithDetay(performerListeDetayList.FirstOrDefault().PerformerListeId);

            if (result) return OdiResponse<PerformerListeOutputDTO>.Success("Liste silindi.", _mapper.Map<PerformerListeOutputDTO>(performerListe), 200);
            else return OdiResponse<PerformerListeOutputDTO>.Fail("Kayıtlar bulunamadı. Silme işlemi başarısız.", "Not Found", 404);
        }
    }
}