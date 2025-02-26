using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.MenajerPerformerLogicServices;
using OdiApp.DTOs.PerformerDTOs.MenajerPerformerDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/menajer-performer")]
[ApiController]
public class MenajerPerformerController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IMenajerPerformerLogicService _menajerPerformerLogicService;

    public MenajerPerformerController(ISharedIdentityService identityService, IMenajerPerformerLogicService menajerPerformerLogicService)
    {
        _identityService = identityService;
        _menajerPerformerLogicService = menajerPerformerLogicService;
    }

    [HttpPost("menajer-performer-not-kaydet")]
    public async Task<IActionResult> MenajerPerformerNotKaydet(MenajerPerformerNotCreateOrUpdateDTO model)
    {
        return Ok(await _menajerPerformerLogicService.MenajerPerformerNotEkleGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("menajer-performer-not-getir")]
    public async Task<IActionResult> MenajerPerformerNotGetir(MenajerPerformerNotGetirInputDTO model)
    {
        return Ok(await _menajerPerformerLogicService.MenajerPerformerNotGetir(model));
    }
}