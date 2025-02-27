using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiListeler;
using OdiApp.DTOs.IslemlerDTOs.OdiListeler;
using OdiApp.DTOs.Kullanici;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class OdiListeController : ControllerBase
    {
        IOdiListeLogicService _odiListeLogicService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public OdiListeController(IOdiListeLogicService odiListeLogicService, ISharedIdentityService sharedIdentityService)
        {
            _odiListeLogicService = odiListeLogicService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost("odi-liste-adlari-getir")]
        public async Task<IActionResult> OdiListeAdlariGetir(KullaniciIdDTO kullaniciId)
        {
            return Ok(await _odiListeLogicService.OdiListeleriGetir(kullaniciId));
        }

        [HttpPost("yeni-odi-liste")]
        public async Task<IActionResult> YeniOdiListe(OdiListeCreateDTO odiListe)
        {
            return Ok(await _odiListeLogicService.YeniOdiListe(odiListe, _sharedIdentityService.GetUser));
        }
        [HttpPost("odi-liste-odi-ekle")]
        public async Task<IActionResult> OdiListeOdiEkle(List<OdiListeDetayCreateDTO> detayList)
        {
            return Ok(await _odiListeLogicService.YeniOdiListeDetay(detayList, _sharedIdentityService.GetUser));
        }
        [HttpPost("odi-liste-odileri-getir")]
        public async Task<IActionResult> OdiListeOdileriGetir(OdiListeIdDTO listeId)
        {
            return Ok(await _odiListeLogicService.OdiListeDetayGetir(listeId));
        }
        [HttpPost("odi-liste-sil")]
        public async Task<IActionResult> OdiListeSil(OdiListeIdDTO listeId)
        {
            return Ok(await _odiListeLogicService.OdiListeSil(listeId));
        }
        [HttpPost("odi-liste-odileri-sil")]
        public async Task<IActionResult> OdiListeOdileriSil(List<OdiListeDetayIdDTO> detayIdList)
        {
            return Ok(await _odiListeLogicService.OdiListeDetaySil(detayIdList));
        }
    }
}
