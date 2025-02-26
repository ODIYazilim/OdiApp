using Microsoft.AspNetCore.Mvc;
using Odi.Shared.AuthAttribute;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekLogic;
using OdiApp.EntityLayer.PerformerModels.YetenekModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[SuperAdminAuthorize]
public class AdminYetenekController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IAdminYetenekLogicService _yetenekService;
    public AdminYetenekController(ISharedIdentityService identityService, IAdminYetenekLogicService adminyetenekLogicService)
    {
        _identityService = identityService;
        _yetenekService = adminyetenekLogicService;
    }

    [HttpPost("yeni-yetenek-tipi")]
    public async Task<IActionResult> YeniYetenekTipi(YetenekTipi yetenekTipi)
    {
        return Ok(await _yetenekService.YeniYetenekTipi(yetenekTipi, _identityService.GetUser));
    }
    [HttpPost("yeni-yetenek")]
    public async Task<IActionResult> YeniYetenek(Yetenek yetenek)
    {
        return Ok(await _yetenekService.YeniYetenek(yetenek, _identityService.GetUser));
    }
}
