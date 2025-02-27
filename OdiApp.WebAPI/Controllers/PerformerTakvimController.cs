using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerTakvimler;
using OdiApp.DTOs.PerformerDTOs.PerformerTakvimler;

namespace OdiApp.WebAPI.Controllers;

[Route("api")]
[ApiController]
[AllAuthorize]
public class PerformerTakvimController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerTakvimLogicService _performerTakvimLogicService;

    public PerformerTakvimController(ISharedIdentityService identityService, IPerformerTakvimLogicService performerTakvimLogicService)
    {
        _identityService = identityService;
        _performerTakvimLogicService = performerTakvimLogicService;
    }

    [HttpPost("yeni-tarih-araligi")]
    public async Task<IActionResult> YeniTarihAraligi(PerformerTakvimCreateDTO requestModel)
    {
        return Ok(await _performerTakvimLogicService.YeniTarihAraligi(requestModel, _identityService.GetUser));
    }

    [HttpPost("musaitlik-kontrolu")]
    public async Task<IActionResult> MusaitlikKontrolu(MusaitlikKontroluDTO requestModel)
    {
        return Ok(await _performerTakvimLogicService.MusaitlikKontrolu(requestModel));
    }

    [HttpPost("tarih-araligi-duzenle")]
    public async Task<IActionResult> TarihAraligiDuzenle(PerformerTakvimUpdateDTO requestModel)
    {
        return Ok(await _performerTakvimLogicService.TarihAraligiDuzenle(requestModel, _identityService.GetUser));
    }

    [HttpPost("tarih-araligi-sil")]
    public async Task<IActionResult> TarihAraligiSil(PerformerTakvimIdDTO requestModel)
    {
        return Ok(await _performerTakvimLogicService.TarihAraligiSil(requestModel));
    }

    [HttpPost("zaman-araligi-sorgula")]
    public async Task<IActionResult> ZamanAraligiSorgula(ZamanAraligiSorgulaDTO requestModel)
    {
        return Ok(await _performerTakvimLogicService.ZamanAraligiSorgula(requestModel));
    }

    [HttpPost("aylik-takvim-sorgula")]
    public async Task<IActionResult> AylikTakvimSorgula(AylikTakvimSorgulaDTO requestModel)
    {
        return Ok(await _performerTakvimLogicService.AylikTakvimSorgula(requestModel));
    }
}