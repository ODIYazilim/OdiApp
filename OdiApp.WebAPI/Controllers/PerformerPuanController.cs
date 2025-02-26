using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerPuanLogicServices;
using OdiApp.DTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/performer-puan")]
[ApiController]
public class PerformerPuanController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerPuanLogicService _performerPuanLogicService;

    public PerformerPuanController(ISharedIdentityService identityService, IPerformerPuanLogicService performerPuanLogicService)
    {
        _identityService = identityService;
        _performerPuanLogicService = performerPuanLogicService;
    }

    #region Admin

    [HttpPost("admin-oy-veren-oy-listesi")]
    public async Task<IActionResult> AdminOyVerenOyListesiGetir(OyverenOyListesiGetirInputDTO model)
    {
        return Ok(await _performerPuanLogicService.AdminOyVerenOyListesiGetir(model));
    }

    [HttpPost("admin-performer-oy-listesi")]
    public async Task<IActionResult> AdminPerformerOyListesiGetir(PerformerOyListesiGetirInputDTO model)
    {
        return Ok(await _performerPuanLogicService.AdminPerformerOyListesiGetir(model));
    }

    #endregion

    [HttpPost("performer-icin-puan-ver")]
    public async Task<IActionResult> PerformerIcinPuanVer(PerformerIcinPuanVerInputDTO model)
    {
        return Ok(await _performerPuanLogicService.PerformerIcinPuanVer(model, _identityService.GetUser));
    }

    [HttpPost("performer-puan-getir")]
    public async Task<IActionResult> PerformerPuanGetir(PerformerIdDTO model)
    {
        return Ok(await _performerPuanLogicService.PerformerPuanGetir(model));
    }

    [HttpPost("performer-listesi-puan-getir")]
    public async Task<IActionResult> PerformerListesiPuanGetir(List<PerformerIdDTO> model)
    {
        return Ok(await _performerPuanLogicService.PerformerListesiPuanGetir(model));
    }
}