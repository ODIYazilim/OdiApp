using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerYorumLogicServices;
using OdiApp.DTOs.PerformerDTOs.PerformerYorumDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/performer-yorumlari")]
[ApiController]
public class PerformerYorumController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerYorumLogicService _performerYorumLogicService;

    public PerformerYorumController(ISharedIdentityService identityService, IPerformerYorumLogicService performerYorumLogicService)
    {
        _identityService = identityService;
        _performerYorumLogicService = performerYorumLogicService;
    }

    #region Admin

    [HttpPost("admin-performer-yorum-sil")]
    public async Task<IActionResult> PerformerYorumSil(PerformerYorumIdDTO model)
    {
        return Ok(await _performerYorumLogicService.PerformerYorumSil(model));
    }

    #endregion

    [HttpPost("performer-yorum-ekle")]
    public async Task<IActionResult> PerformerYorumEkle(PerformerYorumCreateDTO model)
    {
        return Ok(await _performerYorumLogicService.PerformerYorumEkle(model, _identityService.GetUser));
    }

    [HttpPost("performer-yorum-listesi")]
    public async Task<IActionResult> PerformerYorumListesiGetir(PerformerIdDTO model)
    {
        return Ok(await _performerYorumLogicService.PerformerYorumListesiGetir(model.PerformerId));
    }
}