using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminPerformerProfilAlanlariLogicServices;
using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/admin-performer-profil-alanlari")]
[ApiController]
public class AdminPerformerProfilAlanlariController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IAdminPerformerProfilAlanlariLogicService _adminPerformerProfilAlanlariLogicService;

    public AdminPerformerProfilAlanlariController(ISharedIdentityService identityService, IAdminPerformerProfilAlanlariLogicService adminPerformerProfilAlanlariLogicService)
    {
        _identityService = identityService;
        _adminPerformerProfilAlanlariLogicService = adminPerformerProfilAlanlariLogicService;
    }

    [HttpPost("performer-profil-alanlari-olustur")]
    public async Task<IActionResult> PerformerProfilAlanlariOlustur(PerformerProfilAlanlari model)
    {
        return Ok(await _adminPerformerProfilAlanlariLogicService.PerformerProfilAlanlariOlustur(model, _identityService.GetUser));
    }

    [HttpPost("performer-profil-alanlari-guncelle")]
    public async Task<IActionResult> PerformerProfilAlanlariGuncelle(PerformerProfilAlanlari model)
    {
        return Ok(await _adminPerformerProfilAlanlariLogicService.PerformerProfilAlanlariGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("performer-profil-alanlari-durum-degistir")]
    public async Task<IActionResult> PerformerProfilAlanlariDurumDegistir(PerformerProfilAlanlariIdDTO model)
    {
        return Ok(await _adminPerformerProfilAlanlariLogicService.PerformerProfilAlanlariDurumDegistir(model, _identityService.GetUser));
    }

    [HttpPost("performer-profil-alanlari-listele")]
    public async Task<IActionResult> PerformerProfilAlanlariListele(PerformerProfilAlanlariListeleInputDTO model)
    {
        return Ok(await _adminPerformerProfilAlanlariLogicService.PerformerProfilAlanlariListele(model));
    }
}