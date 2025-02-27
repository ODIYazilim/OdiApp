using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.ProjeMesajlasmaLogicServices;
using OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/proje-mesajlasma")]
    [ApiController]
    [AllAuthorize]
    public class ProjeMesajlasmaController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IProjeMesajLogicService _projeMesajLogicService;

        public ProjeMesajlasmaController(ISharedIdentityService identityService, IProjeMesajLogicService projeMesajLogicService)
        {
            _identityService = identityService;
            _projeMesajLogicService = projeMesajLogicService;
        }

        [HttpPost("yeni-proje-mesaj")]
        public async Task<IActionResult> YeniProjeMesaj(ProjeMesajCreateDTO projeMesaj)
        {
            string jtwToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeMesajLogicService.YeniProjeMesaj(projeMesaj, _identityService.GetUser, jtwToken));
        }

        [HttpPost("yeni-proje-mesaj-detay")]
        public async Task<IActionResult> YeniProjeMesajDetay(ProjeMesajDetayCreateDTO projeMesajDetay)
        {
            string jtwToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeMesajLogicService.YeniProjeMesajDetay(projeMesajDetay, _identityService.GetUser, jtwToken));
        }

        [HttpPost("proje-mesaj-listesi")]
        public async Task<IActionResult> ProjeMesajListesi(ProjeMesajListesiInputDTO requestModel)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajListesi(requestModel));
        }

        [HttpPost("proje-mesaj-detay-listesi")]
        public async Task<IActionResult> ProjeMesajDetayListesi(ProjeMesajDetayListesiInputDTO projeMesajDetayListesiInputDTO)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajDetayListesi(projeMesajDetayListesiInputDTO));
        }

        [HttpPost("proje-mesaj-goruldu")]
        public async Task<IActionResult> ProjeMesajGoruldu(ProjeMesajDetayIdDTO projeMesajDetayId)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajGoruldu(projeMesajDetayId));
        }

        [HttpPost("proje-mesaj-goruldu-liste")]
        public async Task<IActionResult> ProjeMesajGorulduListe(List<ProjeMesajDetayIdDTO> projeMesajDetayIdListe)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajGorulduListe(projeMesajDetayIdListe));
        }

        [HttpPost("proje-mesaj-silme")]
        public async Task<IActionResult> ProjeMesajSilme(ProjeMesajIdDTO projeMesajId)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajSilme(projeMesajId));
        }

        [HttpPost("proje-mesaj-silme-liste")]
        public async Task<IActionResult> ProjeMesajSilmeListe(List<ProjeMesajIdDTO> projeMesajIdList)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajSilmeListe(projeMesajIdList));
        }

        [HttpPost("proje-mesaj-detay-silme")]
        public async Task<IActionResult> ProjeMesajDetaySilme(ProjeMesajDetayIdDTO projeMesajDetayId)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajDetaySilme(projeMesajDetayId));
        }

        [HttpPost("proje-mesaj-detay-silme-liste")]
        public async Task<IActionResult> ProjeMesajDetaySilmeListe(List<ProjeMesajDetayIdDTO> projeMesajDetayIdList)
        {
            return Ok(await _projeMesajLogicService.ProjeMesajDetaySilmeListe(projeMesajDetayIdList));
        }
    }
}