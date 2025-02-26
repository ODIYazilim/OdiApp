using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerMenajerLogicServices;
using OdiApp.DTOs.PerformerDTOs.MenajerAbonelikDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/performer-menajer")]
[ApiController]
public class PerformerMenajerController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerMenajerLogicService _performerMenajerLogicService;

    public PerformerMenajerController(ISharedIdentityService identityService, IPerformerMenajerLogicService performerMenajerLogicService)
    {
        _identityService = identityService;
        _performerMenajerLogicService = performerMenajerLogicService;
    }

    [HttpPost("menajer-abonelik-kalan-kullanim-sayilari")]
    public async Task<IActionResult> MenajerAbonelikKalanKullanimSayilari(YetenekTemsilcisiIdDTO model)
    {
        string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerMenajerLogicService.MenajerAbonelikKalanKullanimSayilariGetir(model, jwtToken));
    }

    [HttpPost("menajer-abonelik-performer-premium-dagitma")]
    public async Task<IActionResult> MenajerAbonelikPerformerPremiumDagitma(PerformerPremiumDagitmaInputDTO model)
    {
        string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerMenajerLogicService.MenajerAbonelikPerformerPremiumDagitma(model, jwtToken, _identityService.GetUser));
    }

    #region Performer Menajer Sözleşme

    [HttpPost("performer-menajer-sozlesme-ekle")]
    public async Task<IActionResult> PerformerMenajerSozlesmeEkle(PerformerMenajerSozlesmeCreateDTO model)
    {
        string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerMenajerLogicService.PerformerMenajerSozlesmeEkle(model, _identityService.GetUser, jwtToken));
    }

    [HttpPost("performer-menajer-sozlesme-guncelle")]
    public async Task<IActionResult> PerformerMenajerSozlesmeGuncelle(PerformerMenajerSozlesmeUpdateDTO model)
    {
        string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerMenajerLogicService.PerformerMenajerSozlesmeGuncelle(model, _identityService.GetUser, jwtToken));
    }

    //Bu endpoint menajer kullanımı için sözleşme getirir.
    //[HttpPost("performer-menajer-sozlesme-getir")]
    //public async Task<IActionResult> PerformerMenajerSozlesmeGetir(PerformerMenajerSozlesmeGetirInputDTO model)
    //{
    //    string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
    //    return Ok(await _performerMenajerLogicService.PerformerMenajerSozlesmeGetir(model, jwtToken));
    //}

    //Bu endpoint performer kullanımı için sözleşme getirir.
    //[HttpPost("menajer-performer-sozlesme-getir")]
    //public async Task<IActionResult> MenajerPerformerSozlesmeGetir(MenajerPerformerSozlesmeGetirInputDTO model)
    //{
    //    return Ok(await _performerMenajerLogicService.MenajerPerformerSozlesmeGetir(model));
    //}

    //Bu endpoint performer kullanımı için sözleşme getirir. 2
    [HttpPost("menajer-performer-sozlesme-listesi-getir")]
    public async Task<IActionResult> MenajerPerformerSozlesmeListesiGetir(MenajerPerformerSozlesmeGetirInputDTO model)
    {
        return Ok(await _performerMenajerLogicService.MenajerPerformerSozlesmeListesiGetir(model));
    }

    //[HttpPost("performer-menajer-sozlesme-listesi-getir")]
    //public async Task<IActionResult> PerformerMenajerSozlesmeListesiGetir(MenajerIdDTO model)
    //{
    //    string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
    //    return Ok(await _performerMenajerLogicService.PerformerMenajerSozlesmeListesiGetir(model, jwtToken));
    //}

    #endregion
}