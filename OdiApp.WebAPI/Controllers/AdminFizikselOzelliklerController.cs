using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler.Interfaces;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.FizikselOzellikler;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

namespace OdiApp.WebAPI.Controllers;

[Route("api")]
[ApiController]
//[SuperAdminAuthorize]
public class AdminFizikselOzelliklerController : ControllerBase
{
    private readonly IAdminFizikselOzellikLogicService _fizikselOzelliklerLogicService;
    private readonly ISharedIdentityService _identityService;

    public AdminFizikselOzelliklerController(ISharedIdentityService sharedIdentityService, IAdminFizikselOzellikLogicService fizikselOzelliklerLogicService)
    {
        _identityService = sharedIdentityService;
        _fizikselOzelliklerLogicService = fizikselOzelliklerLogicService;
    }

    [HttpPost("admin-yeni-fiziksel-ozellik-tipi")]
    public async Task<IActionResult> YeniFizikselOzellikTipi(FizikselOzellikTipi tip)
    {
        return Ok(await _fizikselOzelliklerLogicService.FizikselOzellikTipiEkle(tip, _identityService.GetUser));
    }

    [HttpPost("admin-fiziksel-ozellik-tipi-guncelle")]
    public async Task<IActionResult> FizikselOzellikGuncelle(FizikselOzellikTipi tip)
    {

        return Ok(await _fizikselOzelliklerLogicService.FizikselOzellikTipiGuncelle(tip, _identityService.GetUser));
    }

    [HttpPost("admin-fiziksel-ozellik-tipi-liste")]
    public async Task<IActionResult> FizikselOzellikTipiListe(DilIdDTO dilId)
    {
        return Ok(await _fizikselOzelliklerLogicService.FizikselOzellikTipiListe(dilId));
    }

    [HttpPost("admin-fiziksel-ozellik-tipi-sil")]
    public async Task<IActionResult> FizikselOzellikTipiSil(FizikselOzellikTipiIdDTO id)
    {
        return Ok(await _fizikselOzelliklerLogicService.FizikselOzellikTipiSil(id));
    }

    [HttpPost("admin-fiziksel-ozellik-tipi-durum-degistir")]
    public async Task<IActionResult> FizikselOzellikTipiDurumDegistir(FizikselOzellikTipiIdDTO id)
    {
        return Ok(await _fizikselOzelliklerLogicService.FizikselOzellikTipiDurumDegistir(id, _identityService.GetUser));
    }
}
