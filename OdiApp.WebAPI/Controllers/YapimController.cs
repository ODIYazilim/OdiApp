using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.OnerilerLogicServices;
using OdiApp.DTOs.PerformerDTOs.OnerilerDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/yapim")]
[ApiController]
public class YapimController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IOnerilerLogicService _onerilerLogicService;

    public YapimController(ISharedIdentityService identityService, IOnerilerLogicService onerilerLogicService)
    {
        _identityService = identityService;
        _onerilerLogicService = onerilerLogicService;
    }

    [HttpPost("oneri-talep-et")]
    public async Task<IActionResult> OneriTalepEt(OneriTalepEtDTO model)
    {
        return Ok(await _onerilerLogicService.OneriTalepEt(model, _identityService.GetUser));
    }

    [HttpPost("menajer-performer-oneri-listele")]
    public async Task<IActionResult> MenajerPerformerOneriListele(MenajerPerformerOneriListeleInputDTO model)
    {
        return Ok(await _onerilerLogicService.MenajerPerformerOneriListele(model));
    }
}