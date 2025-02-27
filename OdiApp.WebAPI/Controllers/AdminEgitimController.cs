using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.Egitim;
using OdiApp.EntityLayer.PerformerModels.Egitim;

namespace OdiApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[SuperAdminAuthorize]
public class AdminEgitimController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IAdminEgitimLogicService _egitimService;

    public AdminEgitimController(ISharedIdentityService sharedIdentityService, IAdminEgitimLogicService egitimService)
    {
        _identityService = sharedIdentityService;
        _egitimService = egitimService;
    }

    [HttpPost("yeni-egitim-tipi")]
    public async Task<IActionResult> YeniEgitimTipi(EgitimTipi egitimTipi)
    {
        return Ok(await _egitimService.YeniEgitimTipi(egitimTipi, _identityService.GetUser));
    }

    [HttpPost("yeni-okul")]
    public async Task<IActionResult> YeniOkul(Okul okul)
    {
        return Ok(await _egitimService.YeniOkul(okul, _identityService.GetUser));
    }

    [HttpPost("yeni-okul-bolum")]
    public async Task<IActionResult> YeniOkulBolum(OkulBolum bolum)
    {
        return Ok(await _egitimService.YeniOkulBolum(bolum, _identityService.GetUser));
    }
}
