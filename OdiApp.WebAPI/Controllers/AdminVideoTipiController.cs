using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;

namespace OdiApp.WebAPI.Controllers;

[Route("api/admin-video-tipi")]
[ApiController]
public class AdminVideoTipiController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    //private readonly IAdminVideoTipiLogicService _adminVideoTipiLogicService;

    public AdminVideoTipiController(ISharedIdentityService identityService /* ,IAdminVideoTipiLogicService adminVideoTipiLogicService*/)
    {
        _identityService = identityService;
        //_adminVideoTipiLogicService = adminVideoTipiLogicService;
    }

    //[HttpPost("video-tipi-olustur")]
    //public async Task<IActionResult> VideoTipiOlustur(VideoTipi model)
    //{
    //    return Ok(await _adminVideoTipiLogicService.VideoTipiOlustur(model, _identityService.GetUser));
    //}

    //[HttpPost("video-tipi-guncelle")]
    //public async Task<IActionResult> VideoTipiGuncelle(VideoTipi model)
    //{
    //    return Ok(await _adminVideoTipiLogicService.VideoTipiGuncelle(model, _identityService.GetUser));
    //}

    //[HttpPost("video-tipi-durum-degistir")]
    //public async Task<IActionResult> VideoTipiDurumDegistir(VideoTipiIdDTO model)
    //{
    //    return Ok(await _adminVideoTipiLogicService.VideoTipiDurumDegistir(model, _identityService.GetUser));
    //}

    //[HttpPost("video-tipi-listele")]
    //public async Task<IActionResult> VideoTipiListele(DilIdDTO model)
    //{
    //    return Ok(await _adminVideoTipiLogicService.VideoTipiListele(model));
    //}

    //[HttpPost("video-tipi-getir")]
    //public async Task<IActionResult> VideoTipiGetir(VideoTipiIdDTO model)
    //{
    //    return Ok(await _adminVideoTipiLogicService.VideoTipiGetir(model));
    //}
}
