using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.CallbackIslemler;
using OdiApp.DTOs.IslemlerDTOs.CallbackIslemler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/callback")]
    [ApiController]
    [AllAuthorize]
    public class CallbackIslemlerController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly ICallbackLogicService _callbackLogicService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CallbackIslemlerController(ISharedIdentityService identityService, ICallbackLogicService callbackLogicService, ISharedIdentityService sharedIdentityService)
        {
            _identityService = identityService;
            _callbackLogicService = callbackLogicService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost("proje-callback-ayarlari-takvim-olustur")]

        public async Task<IActionResult> YeniProjeCallbackAyarlariTakvim(CallbackAyarlarıTakvimCreateDTO input)
        {

            return Ok(await _callbackLogicService.YeniCallbackAyarlarıTakvimOlustur(input, _sharedIdentityService.GetUser));
        }

        [HttpPost("proje-callback-takvimi-getir")]
        public async Task<IActionResult> CallbackTakvimiGetir(CallbackTakvimiGetirInput input)
        {
            return Ok(await _callbackLogicService.CallbackTakvimiGetir(input));
        }

        [HttpPost("callback-saatleri-kilitle")]
        public async Task<IActionResult> CallbackSaatleriKilitle(CallbackSaatKilitleInputDTO input)
        {

            return Ok(await _callbackLogicService.CallbackSaatleriKilitle(input));
        }

        [HttpPost("callback-saatleri-kilit-Ac")]
        public async Task<IActionResult> CallbackSaatleriKilitAc(CallbackSaatKilitleInputDTO input)
        {

            return Ok(await _callbackLogicService.CallbackSaatleriKilidiAc(input));
        }

        [HttpPost("yeni-callback-tarih-saat-ekle")]
        public async Task<IActionResult> YeniCallbackTarihSaatEkle(CallbackTarihSaatCreateDTO input)
        {

            return Ok(await _callbackLogicService.YeniCalllbackTarihSaatleriEkle(input, _sharedIdentityService.GetUser));
        }

        [HttpPost("callback-tarih-ekleme-ayarlari-getir")]
        public async Task<IActionResult> CallbackTarihEklemeAyarlariGetir(ProjeIdDTO id)
        {

            return Ok(await _callbackLogicService.CallbackTarihEklemeAyarlariGetir(id));
        }

        [HttpPost("callback-saati-notu-guncelle")]
        public async Task<IActionResult> CallbackSaatiNotuGuncelle(CallbackSaatNotuInputDTO input)
        {

            return Ok(await _callbackLogicService.CallbackSaatiNotGuncelle(input, _sharedIdentityService.GetUser));
        }

        [HttpPost("callback-gonderilecek-performer-listesi")]
        public async Task<IActionResult> CallbackGonderilecekPerformerListesi(ProjeIdDTO projeId)
        {
            return Ok(await _callbackLogicService.CallbackGonderilebilecekPerformerListesi(projeId));
        }

        [HttpPost("callback-gonder")]
        public async Task<IActionResult> CallbackGonder(List<CallbackCreateDTO> callbackList)
        {
            return Ok(await _callbackLogicService.CallbackGonder(callbackList, _sharedIdentityService.GetUser));
        }

        [HttpPost("yapim-callback-listesi")]
        public async Task<IActionResult> YapimCallbackListesi(ProjeIdDTO projeId)
        {
            return Ok(await _callbackLogicService.YapimCallbackListesi(projeId));
        }

        [HttpPost("menajer-callback-listesi")]
        public async Task<IActionResult> MenajerCallbackListesi(KullaniciIdDTO menajerId)
        {
            return Ok(await _callbackLogicService.MenajerCallbackListesi(menajerId));
        }

        [HttpPost("callback-onayla")]
        public async Task<IActionResult> CallbackOnayla(CallbackOnaylaDTO onay)
        {
            return Ok(await _callbackLogicService.CallbackOnayla(onay, _sharedIdentityService.GetUser));
        }

        [HttpPost("callback-reddet")]
        public async Task<IActionResult> CallbackRed(CallbackRedDTO red)
        {
            return Ok(await _callbackLogicService.CallbackRed(red, _sharedIdentityService.GetUser));
        }


        [HttpPost("callback-menajer-gordu")]
        public async Task<IActionResult> CallbackMenajerGordu(CallbackIdDTO callbackId)
        {
            return Ok(await _callbackLogicService.CallbackMenajerGordu(callbackId));
        }
        [HttpPost("callback-performer-gordu")]
        public async Task<IActionResult> CallbackPerformerGordu(CallbackIdDTO callbackId)
        {
            return Ok(await _callbackLogicService.CallbackPerformerGordu(callbackId));
        }
        [HttpPost("callback-performer-detaylari")]
        public async Task<IActionResult> CallbackPerformerDetaylari(CallbackPerformerDetaylariInputDTO input)
        {
            return Ok(await _callbackLogicService.CallbackPerformerDetaylari(input));
        }
        [HttpGet("delete-takvim-ayarlar")]
        public async Task<IActionResult> DeleteTakvimAyarlar()
        {
            await _callbackLogicService.DeleteCallbackAyarlariTakvim();

            return Ok("silindi");
        }


    }
}