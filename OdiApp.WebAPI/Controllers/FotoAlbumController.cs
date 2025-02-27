using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/fotograf")]
[ApiController]
[AllAuthorizeAttribute]

public class FotoAlbumController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IGecerliDilService _dilService;
    private readonly IPerformerCVLogicService _performerCVService;
    private readonly IFotoAlbumLogicService _fotoAlbumLogicService;

    public FotoAlbumController(IMapper mapper, ISharedIdentityService sharedIdentityService, IGecerliDilService gecerliDilService, IPerformerCVLogicService performerCvService, IFotoAlbumLogicService fotoAlbumLogicService)
    {

        _identityService = sharedIdentityService;
        _dilService = gecerliDilService;
        _performerCVService = performerCvService;
        _fotoAlbumLogicService = fotoAlbumLogicService;
    }

    [HttpGet("album-tipi-liste")]
    public async Task<IActionResult> FotoAlbumTipiListe()
    {
        return Ok(await _fotoAlbumLogicService.FotoAlbumTipiListesi());
    }

    [HttpPost("yeni-foto-album")]
    public async Task<IActionResult> YeniFotoAlbum(FotoAlbumDTO album)
    {
        return Ok(await _fotoAlbumLogicService.YeniFotoAlbum(album, _identityService.GetUser));
    }

    [HttpPost("foto-album-guncelle")]
    public async Task<IActionResult> FotoAlbumGuncel(FotoAlbumDTO album)
    {
        return Ok(await _fotoAlbumLogicService.FotoAlbumGuncel(album, _identityService.GetUser));
    }

    [HttpPost("foto-album-sil")]
    public async Task<IActionResult> FotoAlbumSil(FotoAlbumIdDTO fotoAlbumId)
    {
        return Ok(await _fotoAlbumLogicService.FotoAlbumSil(fotoAlbumId));
    }

    [HttpPost("foto-album-liste")]
    public async Task<IActionResult> FotoAlbumListesi(KullaniciIdDTO kullaniciId)
    {
        return Ok(await _fotoAlbumLogicService.FotoAlbumListesi(kullaniciId));
    }

    [HttpPost("toplu-foto-album-liste")]
    public async Task<IActionResult> TopluFotoAlbumListesi(List<KullaniciIdDTO> kullaniciIdList)
    {
        return Ok(await _fotoAlbumLogicService.TopluFotoAlbumListesi(kullaniciIdList));
    }

    [HttpPost("yeni-fotograf")]
    public async Task<IActionResult> YeniAlbumFotografi(FotoAlbumFotografDTO foto)
    {
        return Ok(await _fotoAlbumLogicService.YeniAlbumFotografi(foto, _identityService.GetUser));
    }

    [HttpPost("fotograf-guncelle")]
    public async Task<IActionResult> AlbumFotografiGuncelle(FotoAlbumFotografDTO foto)
    {
        return Ok(await _fotoAlbumLogicService.AlbumFotografiGuncelle(foto, _identityService.GetUser));
    }

    [HttpPost("fotograf-sil")]
    public async Task<IActionResult> AlbumFotografiSil(FotoAlbumFotografIdDTO fotoId)
    {
        return Ok(await _fotoAlbumLogicService.AlbumFotografiSil(fotoId));
    }

    [HttpPost("fotograf-listesi")]
    public async Task<IActionResult> AlbumFotograflari(FotoAlbumIdDTO fotoAlbumId)
    {
        return Ok(await _fotoAlbumLogicService.AlbumFotograflari(fotoAlbumId));
    }
}