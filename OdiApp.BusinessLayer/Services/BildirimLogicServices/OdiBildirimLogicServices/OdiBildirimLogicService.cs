using AutoMapper;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OneSignalLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.PushNotificationServices;
using OdiApp.DataAccessLayer.BildirimDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.BildirimDataServices.OdiBildirimDataServices;
using OdiApp.DTOs.BildirimDTOs.OdiBildirimDTOS;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs;
using OdiApp.EntityLayer.BildirimModels;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.OdiBildirimLogicServices
{
    public class OdiBildirimLogicService : IOdiBildirimLogicService
    {
        private readonly IOdiBildirimDataService _bildirimDataService;
        private readonly IPushNotificationService _pushNoNotificationService;
        private readonly IOneSignalLogicService _oneSignalLogicService;
        private readonly IMapper _mapper;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IKullaniciBasicDataService _kullaniciBasicDataService;

        public OdiBildirimLogicService(IMapper mapper, IOdiBildirimDataService bildirimDataService, IPushNotificationService pushNoNotificationService, IOneSignalLogicService oneSignalLogicService, IAmazonS3Service amazonS3Service, IKullaniciBasicDataService kullaniciBasicDataService)
        {
            _bildirimDataService = bildirimDataService;
            _pushNoNotificationService = pushNoNotificationService;
            _oneSignalLogicService = oneSignalLogicService;
            _mapper = mapper;
            _amazonS3Service = amazonS3Service;
            _kullaniciBasicDataService = kullaniciBasicDataService;
        }

        public async Task<OdiResponse<bool>> OdiBildirimHepsiOkundu(KullaniciIdDTO idDTO)
        {
            bool result = await _bildirimDataService.OdiBildirimHepsiOkundu(idDTO.KullaniciId);
            if (result) return OdiResponse<bool>.Success("Tüm Bildirimler Okundu", true, 200);
            else return OdiResponse<bool>.Fail("İşlem yapılırken bir sorun oluştu", "Bir Sorun oluştu", 400);
        }

        public async Task<OdiResponse<List<OdiBildirimTumOutputDTO>>> OdiBildirimListesi(OdiBildirimListInputDTO bildirimListInputDTO)
        {
            List<OdiBildirim> bildirimList = await _bildirimDataService.OdiBildirimListesi(bildirimListInputDTO.KullaniciId, bildirimListInputDTO.SonBildirimTarihi);
            List<OdiBildirimHerkes> bildirimHerkesList = await _bildirimDataService.OdiBildirimHerkesListesi(bildirimListInputDTO.SonBildirimTarihi);
            List<OdiBildirimTumOutputDTO> list = _mapper.Map<List<OdiBildirimTumOutputDTO>>(bildirimList).Concat(_mapper.Map<List<OdiBildirimTumOutputDTO>>(bildirimHerkesList)).ToList();

            foreach (var item in list)
            {
                item.DosyaYolu = _amazonS3Service.GetPreSignedUrl(item.DosyaYolu);
            }

            return OdiResponse<List<OdiBildirimTumOutputDTO>>.Success("Bildirim Listesi Getirildi", list, 200);
        }

        public async Task<OdiResponse<bool>> OdiBildirimOkundu(string odiBildirimId)
        {
            OdiBildirim bildirim = await _bildirimDataService.OdiBildirimGetir(odiBildirimId);
            bildirim.Okundu = true;
            await _bildirimDataService.OdiBildirimGuncelle(bildirim);
            return OdiResponse<bool>.Success("Bildirim Okundu", true, 200);
        }
        public async Task<OdiResponse<bool>> OdiBildirimSil(string OdiBildirimId)
        {
            OdiBildirim bildirim = await _bildirimDataService.OdiBildirimGetir(OdiBildirimId);
            if (bildirim == null) return OdiResponse<bool>.Fail("Bu id ile bildirim bulunamadı", "Bad Request", 400);
            await _bildirimDataService.OdiBildirimSil(bildirim);
            return OdiResponse<bool>.Success("Bildirim silindi", true, 200);
        }

        public async Task<OdiResponse<OdiBildirimOutputDTO>> YeniOdiBildirim(OdiBildirimCreateDTO bildirim, OdiUser user)
        {
            OdiBildirim bild = new OdiBildirim();

            if (bildirim.KullaniciId == "ODI")
            {
                //Kullanici id ODI ise ODIY kayit gruplu kullanıcıların hepsine gönderilecek.

                List<KullaniciBasic> kullaniciList = await _kullaniciBasicDataService.KullaniciListesiGetirByKayitGrubu(KayitGrupKodlari.OdiYoneticisi);

                foreach (var item in kullaniciList)
                {
                    bild = _mapper.Map<OdiBildirim>(bildirim);

                    bild.KullaniciId = item.KullaniciId;
                    bild.KullaniciAdSoyad = item.KullaniciAdSoyad;

                    bild.EklenmeTarihi = DateTime.Now;
                    bild.EkleyenId = user.Id;
                    bild.Ekleyen = user.AdSoyad;

                    bild.GuncellenmeTarihi = DateTime.Now;
                    bild.GuncelleyenId = user.Id;
                    bild.Guncelleyen = user.AdSoyad;

                    bild = await _bildirimDataService.YeniOdiBildirim(bild);
                }
            }
            else
            {
                bild = _mapper.Map<OdiBildirim>(bildirim);

                bild.EklenmeTarihi = DateTime.Now;
                bild.EkleyenId = user.Id;
                bild.Ekleyen = user.AdSoyad;

                bild.GuncellenmeTarihi = DateTime.Now;
                bild.GuncelleyenId = user.Id;
                bild.Guncelleyen = user.AdSoyad;

                bild = await _bildirimDataService.YeniOdiBildirim(bild);

                List<string> osusers = new List<string>();
                OneSignalUser osuser = await _oneSignalLogicService.OneSignalUserGetir(bild.KullaniciId);

                if (osuser != null)
                {
                    osusers.Add(osuser.OneSignalExternalId);
                    await _pushNoNotificationService.SendPushClient(bild, osusers);
                }
            }

            return OdiResponse<OdiBildirimOutputDTO>.Success("Bildirim Gönderildi", _mapper.Map<OdiBildirimOutputDTO>(bild), 200);
        }

        public async Task<OdiResponse<bool>> YeniTopluBildirim(OdiTopluBildirimCreateDTO model, OdiUser user)
        {
            foreach (var item in model.GonderilecekKullanicilar)
            {
                OdiBildirim bild = _mapper.Map<OdiBildirim>(model);

                bild.EklenmeTarihi = DateTime.Now;
                bild.EkleyenId = user.Id;
                bild.Ekleyen = user.AdSoyad;

                bild.GuncellenmeTarihi = DateTime.Now;
                bild.GuncelleyenId = user.Id;
                bild.Guncelleyen = user.AdSoyad;

                bild.KullaniciId = item.KullaniciId;
                bild.KullaniciAdSoyad = item.KullaniciAdSoyad;

                bild = await _bildirimDataService.YeniOdiBildirim(bild);

                if (model.OneSignalBildirimGonder)
                {
                    List<string> osusers = new List<string>();
                    OneSignalUser osuser = await _oneSignalLogicService.OneSignalUserGetir(bild.KullaniciId);

                    if (osuser != null)
                    {
                        osusers.Add(osuser.OneSignalExternalId);
                        await _pushNoNotificationService.SendPushClient(bild, osusers);
                    }
                }
            }

            return OdiResponse<bool>.Success("Bildirimler Gönderildi", true, 200);
        }

        public async Task<OdiResponse<OdiBildirimHerkesOutputDTO>> YeniOdiBildirimHerkes(OdiBildirimHerkesCreateDTO bildirim, OdiUser user)
        {
            OdiBildirimHerkes bild = _mapper.Map<OdiBildirimHerkes>(bildirim);

            bild.EklenmeTarihi = DateTime.Now;
            bild.EkleyenId = user.Id;
            bild.Ekleyen = user.AdSoyad;

            bild.GuncellenmeTarihi = DateTime.Now;
            bild.GuncelleyenId = user.Id;
            bild.Guncelleyen = user.AdSoyad;

            bild = await _bildirimDataService.YeniOdiBildirimHerkes(bild);
            return OdiResponse<OdiBildirimHerkesOutputDTO>.Success("Bildirim Gönderildi", _mapper.Map<OdiBildirimHerkesOutputDTO>(bild), 200);
        }
    }
}