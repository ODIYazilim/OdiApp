using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.KullaniciBasicLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;
using OdiApp.BusinessLayer.Services.ShareWithOtherServicess;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api")]
[ApiController]
public class ShareWithOtherServicesController : ControllerBase
{
    private readonly IShareWithOtherServices _shareWithOtherServices;
    private readonly IKullaniciBasicLogicService _kullaniciBasicLogicService;
    private readonly ISharedIdentityService _identityService;
    private readonly IPerformerCVLogicService _performerCVService;
    private readonly IGecerliDilService _dilService;

    public ShareWithOtherServicesController(IKullaniciBasicLogicService kullaniciBasicLogicService, ISharedIdentityService identityService, IPerformerCVLogicService performerCVService, IGecerliDilService dilService, IShareWithOtherServices shareWithOtherServices)
    {
        _kullaniciBasicLogicService = kullaniciBasicLogicService;
        _identityService = identityService;
        _performerCVService = performerCVService;
        _dilService = dilService;
        _shareWithOtherServices = shareWithOtherServices;
    }

    [HttpPost("kullanici-ekle")]
    public async Task<IActionResult> KullaniciEkle(KullaniciBasicEkleDTO model)
    {
        return Ok(await _kullaniciBasicLogicService.KullaniciEkle(model, _identityService.GetUser));
    }

    [HttpPost("kullanici-profil-fotografi-guncelle")]
    public async Task<IActionResult> KullaniciProfilFotografiGuncelle(ProfilFotoPostDTO model)
    {
        return Ok(await _kullaniciBasicLogicService.ProfilFotografiGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("kullanici-ad-soyad-guncelle")]
    public async Task<IActionResult> KullaniciAdSoyadGuncelle(KullaniciCVAdSoyadDTO model)
    {
        return Ok(await _kullaniciBasicLogicService.AdSoyadGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("kullanici-e-mail-guncelle")]
    public async Task<IActionResult> KullaniciEmailGuncelle(KullaniciEmailDTO model)
    {
        return Ok(await _kullaniciBasicLogicService.EmailGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("kullanici-telefon-numarasi-guncelle")]
    public async Task<IActionResult> KullaniciTelefonNumarasiGuncelle(KullaniciTelefonNumarasiDTO model)
    {
        return Ok(await _kullaniciBasicLogicService.TelefonNumarasiGuncelle(model, _identityService.GetUser));
    }

    [HttpPost("proje-yetkili-listesi")]
    public async Task<IActionResult> ProjeDetayGetir(ProjeIdDTO projeId)
    {
        return Ok(await _shareWithOtherServices.ProjeYetkilileriGetir(projeId));
    }

    #region Rol Özellik Ayarları

    [HttpPost("rol-ozellik-ayarlari-getir")]
    public async Task<IActionResult> RolOzellikAyarlariGetir(KayitTuruKodlariDTO kayitTuruKodlariDTO)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.RolOzellikAyarlariGetir(kayitTuruKodlariDTO, await _dilService.GecerliDil(), jwt));
    }

    #endregion
}