using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.OdemeLogicServices.OdicikIslemleriLogicServices;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.OdemeDTOs.OdicikİslemleriDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class OdicikIslemleriController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IOdicikIslemleriLogicService _odicikIslemleriLogicService;

        public OdicikIslemleriController(ISharedIdentityService identityService, IOdicikIslemleriLogicService odicikIslemleriLogicService)
        {
            _identityService = identityService;
            _odicikIslemleriLogicService = odicikIslemleriLogicService;
        }

        [HttpPost("odicik-ekleme")]
        public async Task<IActionResult> OdicikEkleme(OdicikEklemeDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _odicikIslemleriLogicService.OdicikEkleme(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("odicik-harcama")]
        public async Task<IActionResult> OdicikHarcama(OdicikHarcamaDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _odicikIslemleriLogicService.OdicikHarcama(model, _identityService.GetUser, jwtToken));
        }

        [HttpPost("odicik-hareketleri")]
        public async Task<IActionResult> OdicikHareketleri(KullaniciIdDTO model)
        {
            return Ok(await _odicikIslemleriLogicService.OdicikHareketleri(model));
        }

        [HttpPost("odicik-bakiye")]
        public async Task<IActionResult> OdicikBakiye(KullaniciIdDTO model)
        {
            return Ok(await _odicikIslemleriLogicService.OdicikBakiye(model));
        }
    }
}