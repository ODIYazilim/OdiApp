using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSeslendirmeDiliLogicServices;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.SeslendirmeDiliDTOs;
using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/admin-seslendirme-dili")]
[ApiController]
public class AdminSeslendirmeDiliController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IAdminSeslendirmeDiliLogicService _adminSeslendirmeDiliLogicService;

    public AdminSeslendirmeDiliController(ISharedIdentityService identityService, IAdminSeslendirmeDiliLogicService adminSeslendirmeDiliLogicService)
    {
        _identityService = identityService;
        _adminSeslendirmeDiliLogicService = adminSeslendirmeDiliLogicService;
    }

    [HttpPost("seslendirme-dili-olustur")]
    public async Task<IActionResult> SeslendirmeDiliOlustur(SeslendirmeDili model)
    {
        return Ok(await _adminSeslendirmeDiliLogicService.SeslendirmeDiliOlustur(model, _identityService.GetUser));
    }

    [HttpPost("seslendirme-dili-guncelle")]
    public async Task<IActionResult> SeslendirmeDiliGuncelle(SeslendirmeDili model)
    {
        return Ok(await _adminSeslendirmeDiliLogicService.SeslendirmeDiliGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("seslendirme-dili-durum-degistir")]
    public async Task<IActionResult> SeslendirmeDiliDurumDegistir(SeslendirmeDiliIdDTO model)
    {
        return Ok(await _adminSeslendirmeDiliLogicService.SeslendirmeDiliDurumDegistir(model, _identityService.GetUser));
    }

    [HttpPost("seslendirme-dili-listele")]
    public async Task<IActionResult> SeslendirmeDiliListele(DilIdDTO model)
    {
        return Ok(await _adminSeslendirmeDiliLogicService.SeslendirmeDiliListele(model));
    }

    [HttpPost("seslendirme-dili-getir")]
    public async Task<IActionResult> SeslendirmeDiliGetir(SeslendirmeDiliIdDTO model)
    {
        return Ok(await _adminSeslendirmeDiliLogicService.SeslendirmeDiliGetir(model));
    }
}