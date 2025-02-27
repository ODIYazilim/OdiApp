using AutoMapper;
using OdiApp.DataAccessLayer.BildirimDataServices.OneSignalDataServices;
using OdiApp.DTOs.BildirimDTOs.OneSignalDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.OneSignalLogicServices
{
    public class OneSignalLogicService : IOneSignalLogicService
    {
        private readonly IOneSignalUserDataService _oneSignalDataService;

        private readonly IMapper _mapper;

        public OneSignalLogicService(IOneSignalUserDataService oneSignalDataService, IMapper mapper)
        {
            _oneSignalDataService = oneSignalDataService;
            _mapper = mapper;
        }

        public async Task<OneSignalUser> OneSignalUserGetir(string kullaniciId)
        {
            OneSignalUser user = await _oneSignalDataService.OneSignalUserGetir(kullaniciId);
            return user;
        }

        public async Task<OdiResponse<OneSignalUserOutputDTO>> YeniOneSignalUser(OneSignalUserCreateDTO oneSignalUser, OdiUser user)
        {
            OneSignalUser osUser = await _oneSignalDataService.OneSignalUserGetir(oneSignalUser.KullaniciId);
            if (osUser != null) return OdiResponse<OneSignalUserOutputDTO>.Fail("Bu Kullanıcı id si ile daha önce one signal user  oluşturulmuş", "Bad Request", 400);
            osUser = _mapper.Map<OneSignalUser>(oneSignalUser);

            osUser.EklenmeTarihi = DateTime.Now;
            osUser.Ekleyen = user.AdSoyad;
            osUser.EkleyenId = user.Id;

            osUser.GuncellenmeTarihi = DateTime.Now;
            osUser.Guncelleyen = user.AdSoyad;
            osUser.GuncelleyenId = user.Id;

            foreach (var item in osUser.Subscriptions)
            {
                item.EklenmeTarihi = DateTime.Now;
                item.Ekleyen = user.AdSoyad;
                item.EkleyenId = user.Id;

                item.GuncellenmeTarihi = DateTime.Now;
                item.Guncelleyen = user.AdSoyad;
                item.GuncelleyenId = user.Id;
            }

            osUser = await _oneSignalDataService.YeniOneSignalUser(osUser);

            osUser = await _oneSignalDataService.OneSignalUserGetir(osUser.KullaniciId);
            return OdiResponse<OneSignalUserOutputDTO>.Success("Yeni OneSignalUser oluşturuldu", _mapper.Map<OneSignalUserOutputDTO>(osUser), 200);
        }

        public async Task<OdiResponse<OneSignalUserOutputDTO>> YeniOneSignalUserSubscribe(OneSignalUserSubscriptionCreateDTO oneSignalUserSubscribe, OdiUser user)
        {
            OneSignalUser osUser = await _oneSignalDataService.OneSignalUserGetir(oneSignalUserSubscribe.KullaniciId);

            if (!osUser.Subscriptions.Any(x => x.OneSignalSubscribeId == oneSignalUserSubscribe.OneSignalSubscribeId))
            {
                OneSignalUserSubscription sub = _mapper.Map<OneSignalUserSubscription>(oneSignalUserSubscribe);
                sub.OneSignalUserId = osUser.Id;
                sub.EkleyenId = user.Id;
                sub.GuncelleyenId = user.Id;
                sub.EklenmeTarihi = DateTime.Now;
                sub.GuncellenmeTarihi = DateTime.Now;
                sub.Ekleyen = user.AdSoyad;
                sub.Guncelleyen = user.AdSoyad;

                _oneSignalDataService.YeniOneSignalUserSubscription(sub);

                osUser = await _oneSignalDataService.OneSignalUserGetir(osUser.KullaniciId);


                return OdiResponse<OneSignalUserOutputDTO>.Success("Yeni OneSignalUserSubscription oluşturuldu", _mapper.Map<OneSignalUserOutputDTO>(osUser), 200);
            }

            return OdiResponse<OneSignalUserOutputDTO>.Success("Bu subscribeId zaten kayıtlı.", _mapper.Map<OneSignalUserOutputDTO>(osUser), 200);

        }
    }
}
