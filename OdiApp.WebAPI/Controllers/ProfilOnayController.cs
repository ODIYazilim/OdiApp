using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices;
using OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/profil-onay")]
[ApiController]
[AllAuthorize]
public class ProfilOnayController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IGecerliDilService _dilService;
    private readonly IProfilOnayLogicService _onayService;

    public ProfilOnayController(ISharedIdentityService sharedIdentityService, IGecerliDilService dilService, IProfilOnayLogicService onayService)
    {
        _dilService = dilService;
        _identityService = sharedIdentityService;
        _onayService = onayService;
    }

    #region Admin

    [HttpPost("admin-yeni-profil-onay-red-nedeni-tanimi")]
    public async Task<IActionResult> YeniProfilOnayRedNedeniTanimi(ProfilOnayRedNedeniTanimi tip)
    {
        return Ok(await _onayService.ProfilOnayRedNedeniTanimiEkle(tip, _identityService.GetUser));
    }

    [HttpPost("admin-profil-onay-red-nedeni-tanimi-guncelle")]
    public async Task<IActionResult> ProfilOnayRedNedeniTanimiGuncelle(ProfilOnayRedNedeniTanimi tip)
    {
        return Ok(await _onayService.ProfilOnayRedNedeniTanimiGuncelle(tip, _identityService.GetUser));
    }

    [HttpPost("admin-profil-onay-red-nedeni-tanimi-sil")]
    public async Task<IActionResult> ProfilOnayRedNedeniTanimiSil(ProfilOnayRedNedeniTanimiIdDTO id)
    {
        return Ok(await _onayService.ProfilOnayRedNedeniTanimiSil(id));
    }

    [HttpPost("admin-profil-onay-red-nedeni-tanimi-durum-degistir")]
    public async Task<IActionResult> ProfilOnayRedNedeniTanimiDurumDegistir(ProfilOnayRedNedeniTanimiIdDTO id)
    {
        return Ok(await _onayService.ProfilOnayRedNedeniTanimiDurumDegistir(id, _identityService.GetUser));
    }

    #endregion

    #region Performer

    [HttpPost("onay-talebi")]
    public async Task<IActionResult> OnayTalebi(ProfilOnayGonderDTO onayDto)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _onayService.ProfilOnayaGonder(onayDto, _identityService.GetUser, jwt));
    }

    [HttpPost("onay-sureci")]
    public async Task<IActionResult> OnaySureci(PerformerIdDTO id)
    {
        return Ok(await _onayService.ProfilOnaySureci(id));
    }

    [HttpPost("sorgula")]
    public async Task<IActionResult> OnaySorgula(ProfilOnayIdDTO id)
    {
        return Ok(await _onayService.ProfilOnayDurumSorgula(id));
    }

    [HttpPost("profil-onay-son-durum-getir")]
    public async Task<IActionResult> ProfilOnaySonDurumGetir(PerformerIdDTO id)
    {
        return Ok(await _onayService.ProfilOnaySonDurumGetir(id));
    }

    #endregion

    #region Menajer

    //[HttpPost("talep-listesi")]
    //public async Task<IActionResult> TalepListesi(TalepListesiInputDTO dto)
    //{
    //    string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
    //    return Ok(await _onayService.TalepListesi(dto, jwt));
    //}

    //[HttpPost("talep-sayisi")]
    //public async Task<IActionResult> TalepSayisi(MenajerIdDTO dto)
    //{
    //    string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
    //    return Ok(await _onayService.TalepSayisi(dto, jwt));
    //}

    [HttpPost("onay-inceleme")]
    public async Task<IActionResult> OnayInceleme(ProfilOnayIdDTO onayId)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _onayService.ProfilOnayIncele(onayId, _identityService.GetUser, jwt));
    }

    [HttpPost("profil-onayla")]
    public async Task<IActionResult> ProfilOnayla(ProfilOnayIdDTO onayId)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _onayService.ProfilOnayOnayla(onayId, _identityService.GetUser, jwt));
    }

    [HttpPost("profil-onay-geri-al")]
    public async Task<IActionResult> ProfilOnayGeriAl(ProfilOnayIdDTO onayId)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _onayService.ProfilOnayGeriAl(onayId, _identityService.GetUser, jwt));
    }

    [HttpPost("onay-red")]
    public async Task<IActionResult> OnayRed(ProfilOnayRedInputDTO red)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _onayService.ProfilOnayRed(red, _identityService.GetUser, jwt));
    }

    [HttpPost("profil-onay-red-nedeni-liste")]
    public async Task<IActionResult> ProfilOnayRedNedeniTanimiListe()
    {
        return Ok(await _onayService.ProfilOnayRedNedeniTanimiListe());
    }

    #endregion
}