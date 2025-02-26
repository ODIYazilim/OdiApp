using Microsoft.AspNetCore.Mvc;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTagLogicServices;

namespace OdiApp.WebAPI.Controllers;

[Route("api/admin-video-tag")]
[ApiController]
public class AdminVideoTagController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    //private readonly IAdminVideoTagLogicService _adminVideoTagLogicService;>

    public AdminVideoTagController(ISharedIdentityService identityService, IAdminVideoTagLogicService adminVideoTagLogicService)
    {
        _identityService = identityService;
        //_adminVideoTagLogicService = adminVideoTagLogicService;
    }

    //[HttpPost("video-tag-olustur")]
    //public async Task<IActionResult> VideoTagOlustur(VideoTag model)
    //{
    //    return Ok(await _adminVideoTagLogicService.VideoTagOlustur(model, _identityService.GetUser));
    //}

    //[HttpPost("video-tag-guncelle")]
    //public async Task<IActionResult> VideoTagGuncelle(VideoTag model)
    //{
    //    return Ok(await _adminVideoTagLogicService.VideoTagGuncelle(model, _identityService.GetUser));
    //}

    //[HttpPost("video-tag-durum-degistir")]
    //public async Task<IActionResult> VideoTagDurumDegistir(VideoTagIdDTO model)
    //{
    //    return Ok(await _adminVideoTagLogicService.VideoTagDurumDegistir(model, _identityService.GetUser));
    //}

    //[HttpPost("video-tag-listele")]
    //public async Task<IActionResult> VideoTagListele(DilIdDTO model)
    //{
    //    return Ok(await _adminVideoTagLogicService.VideoTagListele(model));
    //}

    //[HttpPost("video-tag-getir")]
    //public async Task<IActionResult> VideoTagGetir(VideoTagIdDTO model)
    //{
    //    return Ok(await _adminVideoTagLogicService.VideoTagGetir(model));
    //}
}