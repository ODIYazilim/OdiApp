using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSesRengiLogicServices;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.SesRengiDTOs;
using OdiApp.EntityLayer.PerformerModels.SesRengiModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/admin-ses-rengi")]
[ApiController]
public class AdminSesRengiController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IAdminSesRengiLogicService _adminSesRengiLogicService;

    public AdminSesRengiController(ISharedIdentityService identityService, IAdminSesRengiLogicService adminSesRengiLogicService)
    {
        _identityService = identityService;
        _adminSesRengiLogicService = adminSesRengiLogicService;
    }

    [HttpPost("ses-rengi-olustur")]
    public async Task<IActionResult> SesRengiOlustur(SesRengi model)
    {
        return Ok(await _adminSesRengiLogicService.SesRengiOlustur(model, _identityService.GetUser));
    }

    [HttpPost("ses-rengi-guncelle")]
    public async Task<IActionResult> SesRengiGuncelle(SesRengi model)
    {
        return Ok(await _adminSesRengiLogicService.SesRengiGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("ses-rengi-durum-degistir")]
    public async Task<IActionResult> SesRengiDurumDegistir(SesRengiIdDTO model)
    {
        return Ok(await _adminSesRengiLogicService.SesRengiDurumDegistir(model, _identityService.GetUser));
    }

    [HttpPost("ses-rengi-listele")]
    public async Task<IActionResult> SesRengiListele(DilIdDTO model)
    {
        return Ok(await _adminSesRengiLogicService.SesRengiListele(model));
    }

    [HttpPost("ses-rengi-getir")]
    public async Task<IActionResult> SesRengiGetir(SesRengiIdDTO model)
    {
        return Ok(await _adminSesRengiLogicService.SesRengiGetir(model));
    }
}