using Microsoft.AspNetCore.Mvc;
using Odi.Shared.AuthAttribute;
using Odi.Shared.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.SetCard;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api/profil")]
[ApiController]
[AllAuthorize]
public class ProfilController : ControllerBase
{
    private readonly IProfilLogicService _profilLogicService;
    private readonly IGecerliDilService _dilService;
    private readonly ISharedIdentityService _identityService;
    private readonly ISetCardLogicService _setCardLogicService;

    public ProfilController(IProfilLogicService profilLogicService, ISharedIdentityService identityService, IGecerliDilService dilService, ISetCardLogicService setCardLogicService)
    {
        _profilLogicService = profilLogicService;
        _identityService = identityService;
        _dilService = dilService;
        _setCardLogicService = setCardLogicService;
    }

    [HttpPost("profil-getir")]
    public async Task<IActionResult> ProfilGetir(PerformerIdDTO id)
    {
        string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _profilLogicService.PerformerProfiliGetir(id, jwtToken));
    }

    [HttpPost("set-card-getir")]
    public async Task<IActionResult> SetCardGetir(List<KullaniciIdDTO> idList)
    {
        return Ok(await _setCardLogicService.SetCardGetir(idList, await _dilService.GecerliDil()));
    }
}