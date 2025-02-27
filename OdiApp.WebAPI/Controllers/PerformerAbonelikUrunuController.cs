using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikUrunuLogicServices;
using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikUrunDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/abonelik-urunu")]
[ApiController]
public class PerformerAbonelikUrunuController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerAbonelikUrunuLogicService _performerAbonelikUrunuLogicService;

    public PerformerAbonelikUrunuController(ISharedIdentityService identityService, IPerformerAbonelikUrunuLogicService performerAbonelikUrunuLogicService)
    {
        _identityService = identityService;
        _performerAbonelikUrunuLogicService = performerAbonelikUrunuLogicService;
    }

    [HttpPost("yeni-performer-abonelik-urunu")]
    public async Task<IActionResult> YeniPerformerAbonelikUrunu(PerformerAbonelikUrunuCreateDTO model)
    {
        return Ok(await _performerAbonelikUrunuLogicService.YeniPerformerAbonelikUrunu(model, _identityService.GetUser));
    }

    [HttpPost("performer-abonelik-urunu-guncelle")]
    public async Task<IActionResult> PerformerAbonelikUrunuGuncelle(PerformerAbonelikUrunuUpdateDTO model)
    {
        return Ok(await _performerAbonelikUrunuLogicService.PerformerAbonelikUrunuGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("performer-abonelik-urun-durum-guncelle")]
    public async Task<IActionResult> PerformerAbonelikUrunDurumGuncelle(PerformerAbonelikUrunuIdDTO model)
    {
        return Ok(await _performerAbonelikUrunuLogicService.PerformerAbonelikUrunDurumGuncelle(model, _identityService.GetUser));
    }

    [HttpGet("performer-abonelik-urunu-liste")]
    public async Task<IActionResult> PerformerAbonelikUrunuListe()
    {
        return Ok(await _performerAbonelikUrunuLogicService.PerformerAbonelikUrunuListele());
    }

    [HttpPost("performer-abonelik-urunu-isim-getir")]
    public async Task<IActionResult> PerformerAbonelikUrunuIsimGetir(AbonelikUrunuIdDTO model)
    {
        return Ok(await _performerAbonelikUrunuLogicService.PerformerAbonelikUrunuIsimGetir(model));
    }
}