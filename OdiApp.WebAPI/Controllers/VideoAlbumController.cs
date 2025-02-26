using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odi.Shared.AuthAttribute;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;

namespace OdiApp.WebAPI.Controllers;

[Route("api/video")]
[ApiController]
[AllAuthorizeAttribute]
public class VideoAlbumController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IGecerliDilService _dilService;
    private readonly IPerformerCVLogicService _performerCVService;
    //private readonly IVideoAlbumLogicService _videoAlbumLogicService;

    public VideoAlbumController(IMapper mapper, ISharedIdentityService sharedIdentityService, IGecerliDilService gecerliDilService, IPerformerCVLogicService performerCvService /*IVideoAlbumLogicService videoAlbumLogicService*/)
    {
        _identityService = sharedIdentityService;
        _dilService = gecerliDilService;
        _performerCVService = performerCvService;
        //_videoAlbumLogicService = videoAlbumLogicService;
    }

    //[HttpGet("album-tipi-liste")]
    //public async Task<IActionResult> videoAlbumTipiListe()
    //{
    //    return Ok(await _videoAlbumLogicService.VideoAlbumTipiListesi(await _dilService.GecerliDil()));
    //}

    //[HttpPost("yeni-video-album")]
    //public async Task<IActionResult> YenivideoAlbum(VideoAlbumDTO album)
    //{
    //    return Ok(await _videoAlbumLogicService.YeniVideoAlbum(album, _identityService.GetUser));
    //}

    //[HttpPost("video-album-guncelle")]
    //public async Task<IActionResult> VideoAlbumGuncel(VideoAlbumDTO album)
    //{
    //    return Ok(await _videoAlbumLogicService.VideoAlbumGuncel(album, _identityService.GetUser));
    //}

    //[HttpPost("video-album-sil")]
    //public async Task<IActionResult> VideoAlbumSil(VideoAlbumIdDTO videoAlbumId)
    //{
    //    return Ok(await _videoAlbumLogicService.VideoAlbumSil(videoAlbumId));
    //}

    //[HttpPost("video-album-liste")]
    //public async Task<IActionResult> VideoAlbumListesi(KullaniciIdDTO kullaniciId)
    //{
    //    return Ok(await _videoAlbumLogicService.VideoAlbumListesi(kullaniciId));
    //}

    //[HttpPost("yeni-video")]
    //public async Task<IActionResult> YeniAlbumVideo(VideoAlbumVideoDTO video)
    //{
    //    return Ok(await _videoAlbumLogicService.YeniAlbumVideo(video, _identityService.GetUser));
    //}

    //[HttpPost("video-guncelle")]
    //public async Task<IActionResult> AlbumVideoGuncelle(VideoAlbumVideoDTO video)
    //{
    //    return Ok(await _videoAlbumLogicService.AlbumVideoGuncelle(video, _identityService.GetUser));
    //}

    //[HttpPost("video-sil")]
    //public async Task<IActionResult> AlbumVideoSil(VideoAlbumVideoIdDTO videoId)
    //{
    //    return Ok(await _videoAlbumLogicService.AlbumVideoSil(videoId));
    //}

    //[HttpPost("video-listesi")]
    //public async Task<IActionResult> AlbumVideolari(VideoAlbumIdDTO videoAlbumId)
    //{
    //    return Ok(await _videoAlbumLogicService.AlbumVideolari(videoAlbumId));
    //}
}
