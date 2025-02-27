using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OdiBildirimLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OneSignalLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.PushNotificationServices;
using OdiApp.DTOs.BildirimDTOs.OdiBildirimDTOS;
using OdiApp.DTOs.BildirimDTOs.OneSignalDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/PushBildirim")]
    [ApiController]
    //[AllAuthorize]
    public class PushBildirimController : ControllerBase
    {
        private readonly IGecerliDilService _dilService;
        private readonly ISharedIdentityService _identityService;
        private readonly IOneSignalLogicService _oneSignalLogicService;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IOdiBildirimLogicService _odiBildirimLogicService;
        public PushBildirimController(IGecerliDilService gecerliDilService, ISharedIdentityService sharedIdentityService, IOneSignalLogicService oneSignalLogicService, IPushNotificationService pushNotificationService, IOdiBildirimLogicService odiBildirimLogicService)
        {
            _dilService = gecerliDilService;
            _identityService = sharedIdentityService;
            _oneSignalLogicService = oneSignalLogicService;
            _pushNotificationService = pushNotificationService;
            _odiBildirimLogicService = odiBildirimLogicService;
        }

        [HttpPost("onesignal-yeni-kullanici")]
        public async Task<IActionResult> OneSignalYeniKullanici(OneSignalUserCreateDTO oneSignalUser)
        {
            return Ok(await _oneSignalLogicService.YeniOneSignalUser(oneSignalUser, _identityService.GetUser));
        }
        [HttpPost("onesignal-yeni-subscription")]
        public async Task<IActionResult> OneSignalYeniSubscription(OneSignalUserSubscriptionCreateDTO oneSignalUserSubscription)
        {
            return Ok(await _oneSignalLogicService.YeniOneSignalUserSubscribe(oneSignalUserSubscription, _identityService.GetUser));
        }
        //[HttpGet("send-push")]
        //public async Task<IActionResult> SendPush()
        //{
        //   //await _pushNotificationService.SendPushAll();
        //    return Ok();
        //}
        //[HttpGet("send-push-all")]
        //public async Task<IActionResult> SendPushAll()
        //{


        //    return Ok();
        //}

        [HttpPost("bildirim-listesi")]
        public async Task<IActionResult> Bildirimlistesi(OdiBildirimListInputDTO bildirimListInputDTO)
        {
            return Ok(await _odiBildirimLogicService.OdiBildirimListesi(bildirimListInputDTO));

        }

        [HttpPost("bildirim-oku")]
        public async Task<IActionResult> BildirimOkundu(OdiBildirimIdDTO bildirimId)
        {
            return Ok(await _odiBildirimLogicService.OdiBildirimOkundu(bildirimId.OdiBildirimId));

        }
        [HttpPost("bildirim-sil")]
        public async Task<IActionResult> BildirimSilindi(OdiBildirimIdDTO bildirimId)
        {
            return Ok(await _odiBildirimLogicService.OdiBildirimSil(bildirimId.OdiBildirimId));

        }

        [HttpPost("yeni-bildirim")]//Arayüzlerde kullanılmayacak,sadece api den gereken yerde bildirm kaydetmek için
        public async Task<IActionResult> YeniBildirim(OdiBildirimCreateDTO bildirim)
        {
            return Ok(await _odiBildirimLogicService.YeniOdiBildirim(bildirim, _identityService.GetUser));
        }

        [HttpPost("yeni-toplu-bildirim")]
        public async Task<IActionResult> YeniTopluBildirim(OdiTopluBildirimCreateDTO model)
        {
            return Ok(await _odiBildirimLogicService.YeniTopluBildirim(model, _identityService.GetUser));
        }
    }
}
