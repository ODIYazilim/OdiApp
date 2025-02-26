using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerEtiketleriLogicServices;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerEtiketleriDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/performer-etiketleri")]
[ApiController]
public class PerformerEtiketleriController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerEtiketleriLogicService _performerEtiketleriLogicService;
    private readonly IGecerliDilService _dilService;

    public PerformerEtiketleriController(ISharedIdentityService identityService, IPerformerEtiketleriLogicService performerEtiketleriLogicService, IGecerliDilService dilService)
    {
        _identityService = identityService;
        _performerEtiketleriLogicService = performerEtiketleriLogicService;
        _dilService = dilService;
    }

    #region Yetenek Temsilcisi Performer Etiket Tipi (Admin)

    [HttpPost("performer-etiket-tipi-ekle")]
    public async Task<IActionResult> YetenekTemsilcisiPerformerEtiketTipiEkle(YetenekTemsilcisiPerformerEtiketTipi model)
    {
        return Ok(await _performerEtiketleriLogicService.YetenekTemsilcisiPerformerEtiketTipiEkle(model, _identityService.GetUser));
    }

    [HttpPost("performer-etiket-tipi-guncelle")]
    public async Task<IActionResult> YetenekTemsilcisiPerformerEtiketTipiGuncelle(YetenekTemsilcisiPerformerEtiketTipi model)
    {
        return Ok(await _performerEtiketleriLogicService.YetenekTemsilcisiPerformerEtiketTipiGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("performer-etiket-tipi-listesi")]
    public async Task<IActionResult> YetenekTemsilcisiPerformerEtiketTipiListesiGetir(DilIdDTO model)
    {
        return Ok(await _performerEtiketleriLogicService.YetenekTemsilcisiPerformerEtiketTipiListesiGetir(model.DilId));
    }

    [HttpPost("performer-etiket-tipi-sil")]
    public async Task<IActionResult> YetenekTemsilcisiPerformerEtiketTipiSil(YetenekTemsilcisiPerformerEtiketTipiIdDTO model)
    {
        return Ok(await _performerEtiketleriLogicService.YetenekTemsilcisiPerformerEtiketTipiSil(model));
    }

    #endregion

    #region Performer Etiket (Admin)


    [HttpPost("performer-etiket-ekle")]
    public async Task<IActionResult> PerformerEtiketEkle(PerformerEtiket model)
    {
        return Ok(await _performerEtiketleriLogicService.PerformerEtiketEkle(model, _identityService.GetUser));
    }

    [HttpPost("performer-etiket-guncelle")]
    public async Task<IActionResult> PerformerEtiketGuncelle(PerformerEtiket model)
    {
        return Ok(await _performerEtiketleriLogicService.PerformerEtiketGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("performer-etiket-listesi")]
    public async Task<IActionResult> PerformerEtiketListesiGetir(DilIdDTO model)
    {
        return Ok(await _performerEtiketleriLogicService.PerformerEtiketListesiGetir(model.DilId));
    }

    [HttpPost("performer-etiket-sil")]
    public async Task<IActionResult> PerformerEtiketSil(PerformerEtiketIdDTO model)
    {
        return Ok(await _performerEtiketleriLogicService.PerformerEtiketSil(model));
    }

    #endregion

    #region Yetenek Temsilcisi Performer Etiketi (Yetenek Temsilcisi)

    [HttpPost("yetenek-temsilcisi-performer-etiket-guncelle")]
    public async Task<IActionResult> YetenekTemsilcisiPerformerEtiketiGuncelle(YetenekTemsilcisiPerformerEtiketiUpdateDTO model)
    {
        return Ok(await _performerEtiketleriLogicService.YetenekTemsilcisiPerformerEtiketiGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("yetenek-temsilcisi-performer-etiket-listesi")]
    public async Task<IActionResult> YetenekTemsilcisiPerformerEtiketiListesiGetir(YetenekTemsilcisiPerformerEtiketiListesiDTO? model)
    {
        return Ok(await _performerEtiketleriLogicService.YetenekTemsilcisiPerformerEtiketiListesiGetir(model, await _dilService.GecerliDil()));
    }

    #endregion
}