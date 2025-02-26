using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.OnerilerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekTemsilcisiLogicServices;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.OnerilerDTOs;
using OdiApp.DTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/yetenek-temsilcisi")]
[ApiController]
public class YetenekTemsilcisiController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IOnerilerLogicService _onerilerLogicService;
    private readonly IYetenekTemsilcisiLogicService _yetenekTemsilcisiLogicService;

    public YetenekTemsilcisiController(ISharedIdentityService identityService, IOnerilerLogicService onerilerLogicService, IYetenekTemsilcisiLogicService yetenekTemsilcisiLogicService)
    {
        _identityService = identityService;
        _onerilerLogicService = onerilerLogicService;
        _yetenekTemsilcisiLogicService = yetenekTemsilcisiLogicService;
    }

    [HttpPost("performer-listesi")]
    public async Task<IActionResult> PerformerListesi(PerformerListesiInputDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _yetenekTemsilcisiLogicService.PerformerListesi(model, jwt));
    }

    [HttpPost("performer-listesi-sayilari")]
    public async Task<IActionResult> PerformerListesiSayilari(MenajerIdDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _yetenekTemsilcisiLogicService.PerformerListesiSayilari(model, jwt));
    }

    [HttpPost("oneri-gonder")]
    public async Task<IActionResult> OneriGonder(OneriGonderDTO model)
    {
        return Ok(await _onerilerLogicService.OneriGonder(model, _identityService.GetUser));
    }

    [HttpPost("performer-yetenek-temsilcisi-atama")]
    public async Task<IActionResult> PerformerYetenekTemsilcisiAtama(PerformerYetenekTemsilcisiAtamaDTO model)
    {
        return Ok(await _yetenekTemsilcisiLogicService.PerformerYetenekTemsilcisiAtama(model, _identityService.GetUser));
    }

    [HttpPost("performer-menajer-getir")]
    public async Task<IActionResult> PerformerMenajerGetir(KullaniciIdDTO model)
    {
        return Ok(await _yetenekTemsilcisiLogicService.PerformerMenajerGetir(model));
    }

    [HttpPost("performer-menajer-listesi-getir")]
    public async Task<IActionResult> PerformerMenajerIdListesiGetir(List<KullaniciIdDTO> model)
    {
        return Ok(await _yetenekTemsilcisiLogicService.PerformerMenajerListesiGetir(model));
    }

    [HttpPost("menajer-performer-listesi-getir")]
    public async Task<IActionResult> MenajerPerformerIdListesiGetir(List<KullaniciIdDTO> model)
    {
        return Ok(await _yetenekTemsilcisiLogicService.MenajerPerformerListesiGetir(model));
    }
}