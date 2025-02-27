using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikLogicServices;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/performer-abonelik")]
[ApiController]
public class PerformerAbonelikController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerAbonelikLogicService _performerAbonelikLogicService;

    public PerformerAbonelikController(ISharedIdentityService identityService, IPerformerAbonelikLogicService performerAbonelikLogicService)
    {
        _identityService = identityService;
        _performerAbonelikLogicService = performerAbonelikLogicService;
    }

    [HttpPost("performer-abonelik-bitis-tarihi-guncelle")]
    public async Task<IActionResult> PerformerAbonelikBitisTarihiGuncelle(PerformerAbonelikTarihDTO model)
    {
        return Ok(await _performerAbonelikLogicService.PerformerAbonelikBitisTarihiGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("performer-abonelik-olustur")]
    public async Task<IActionResult> PerformerAbonelikOlustur(PerformerAbonelikCreateDTO model)
    {
        return Ok(await _performerAbonelikLogicService.PerformerAbonelikOlustur(model, _identityService.GetUser));
    }

    [HttpPost("performer-abonelik-bitir")]
    public async Task<IActionResult> PerformerAbonelikBitir(AboneligiSonlandirInputDTO model)
    {
        return Ok(await _performerAbonelikLogicService.PerformerAbonelikBitir(model, _identityService.GetUser));
    }

    [HttpPost("performer-abonelik-ozeti-getir")]
    public async Task<IActionResult> PerformerAbonelikOzetiGetir(KullaniciIdDTO model)
    {
        string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerAbonelikLogicService.PerformerAbonelikOzetiGetir(model, jwtToken));
    }

    [HttpPost("performer-abonelik-yenilemeyi-kapat")]
    public async Task<IActionResult> PerformerAbonelikYenilemeyiKapat(AbonelikIdDTO model)
    {
        return Ok(await _performerAbonelikLogicService.PerformerAbonelikYenilemeyiKapat(model, _identityService.GetUser));
    }
}