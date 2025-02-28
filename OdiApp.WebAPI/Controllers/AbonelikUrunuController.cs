using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.OdemeLogicServices.AbonelikUrunuLogicServices;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AbonelikUrunuController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IAbonelikUrunuLogicService _abonelikUrunuLogicService;

        public AbonelikUrunuController(ISharedIdentityService identityService, IAbonelikUrunuLogicService abonelikUrunuLogicService)
        {
            _identityService = identityService;
            _abonelikUrunuLogicService = abonelikUrunuLogicService;
        }

        [HttpPost("odeme-yontemi-abonelik-urunu-olusturma")]
        public async Task<IActionResult> OdemeYontemiAbonelikUrunuOlusturma(OdemeYontemiPerformerAbonelikUrunuCreateDTO model)
        {
            return Ok(await _abonelikUrunuLogicService.OdemeYontemiAbonelikUrunuOlusturma(model, _identityService.GetUser));
        }

        [HttpGet("abonelik-urunleri-listeleme")]
        public async Task<IActionResult> AbonelikUrunleriListeleme()
        {
            return Ok(await _abonelikUrunuLogicService.AbonelikUrunleriListeleme());
        }

        [HttpPost("abonelik-odeme-plani-olustur")]
        public async Task<IActionResult> AbonelikOdemePlaniOlustur(AbonelikUrunuOdemePlaniCreateDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.AbonelikOdemePlaniOlustur(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("odeme-planı-silme")]
        public async Task<IActionResult> OdemePlaniSilme(ReferenceCodeDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.OdemePlaniSil(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("abonelik-urunu-silme")]
        public async Task<IActionResult> AbonelikUrunuSilme(ReferenceCodeDTO model)
        {
            return Ok(await _abonelikUrunuLogicService.AbonelikUrunuSil(model, _identityService.GetUser));
        }

        [HttpPost("abonelik-paketi-satin-alma")]
        public async Task<IActionResult> AbonelikPaketiSatinAlma(AbonelikPaketiSatinAlmaInputDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.AbonelikPaketiSatinAlma(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("abonelik-paketi-abonelik-baslatma")]
        public async Task<IActionResult> AbonelikPaketiAbonelikBaslatma(AbonelikPaketiAbonelikBaslatmaInputDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.AbonelikPaketiAbonelikBaslatma(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("paket-abonelik-odeme-listesi")]
        public async Task<IActionResult> PaketAbonelikOdemeListesi(KullaniciIdDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.PaketAbonelikOdemeListesi(model, jwtToken));
        }

        [HttpPost("paket-satin-alma-odeme-listesi")]
        public async Task<IActionResult> PaketSatinAlmaOdemeListesi(KullaniciIdDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.PaketSatinAlmaOdemeListesi(model, jwtToken));
        }

        [HttpPost("abonelik-kart-bilgisi-getir")]
        public async Task<IActionResult> AbonelikKartBilgisiGetir(AbonelikReferenceCodeDTO model)
        {
            return Ok(await _abonelikUrunuLogicService.AbonelikKartBilgisiGetir(model));
        }

        [HttpPost("aboneligi-sonlandir")]
        public async Task<IActionResult> AboneligiSonlandir(AboneligiSonlandirInputDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.AboneligiSonlandir(model, jwtToken));
        }

        [HttpPost("abinelik-iptal-talebinin-alinmasi")]
        public async Task<IActionResult> AbonelikIptalTalebininAlinmasi(AbonelikYukseltmeTalepCreateDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.AbonelikIptalTalebininAlinmasi(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("abonelik-yukseltme")]
        public async Task<IActionResult> AbonelikYukseltme(AbonelikYukseltmeInputDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _abonelikUrunuLogicService.AbonelikYukseltme(model, _identityService.GetUser, jwtToken));
        }
    }
}