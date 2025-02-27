using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerListeler;
using OdiApp.DTOs.IslemlerDTOs.PerformerListeler;
using OdiApp.DTOs.Kullanici;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [AllAuthorize]
    public class PerformerListeController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IPerformerListeLogicService _performerListeLogicService;

        public PerformerListeController(ISharedIdentityService identityService, IPerformerListeLogicService performerListeLogicService)
        {
            _identityService = identityService;
            _performerListeLogicService = performerListeLogicService;
        }

        [HttpPost("performer-liste-listesi")]
        public async Task<IActionResult> PerformerListeListesi(KullaniciIdDTO requestModel)
        {
            return Ok(await _performerListeLogicService.PerformerListeListesi(requestModel));
        }

        [HttpPost("performer-liste-getir")]
        public async Task<IActionResult> PerformerListeGetir(PerformerListeIdDTO requestModel)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _performerListeLogicService.PerformerListeWithPerformerDetay(requestModel, jwtToken));
        }

        [HttpPost("yeni-performer-liste")]
        public async Task<IActionResult> YeniPerformerListe(PerformerListeCreateDTO performerListe)
        {
            return Ok(await _performerListeLogicService.YeniPerformerListe(performerListe, _identityService.GetUser));
        }

        [HttpPost("performer-liste-guncelle")]
        public async Task<IActionResult> PerformerListeGuncelle(PerformerListeUpdateDTO performerListe)
        {
            return Ok(await _performerListeLogicService.PerformerListeGuncelle(performerListe, _identityService.GetUser));
        }

        [HttpPost("performer-liste-sil")]
        public async Task<IActionResult> PerformerListeSil(PerformerListeIdDTO requestModel)
        {
            return Ok(await _performerListeLogicService.PerformerListeSil(requestModel));
        }

        [HttpPost("yeni-performer-liste-detay")]
        public async Task<IActionResult> YeniPerformerListeDetay(List<PerformerListeDetayCreateDTO> performerListeDetayList)
        {
            return Ok(await _performerListeLogicService.YeniPerformerListeDetay(performerListeDetayList, _identityService.GetUser));
        }

        [HttpPost("performer-liste-detay-sil")]
        public async Task<IActionResult> PerformerListeDetaySil(List<PerformerListeDetayIdDTO> requestModel)
        {
            return Ok(await _performerListeLogicService.PerformerListeDetaySil(requestModel));
        }
    }
}
