using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolBilgileri;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjePerformerController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IProjeRolLogicService _projeRolLogicService;

        public ProjePerformerController(ISharedIdentityService identityService, IProjeRolLogicService projeRolLogicService)
        {
            _identityService = identityService;
            _projeRolLogicService = projeRolLogicService;
        }

        [HttpPost("yeni-proje-rol-performer")]
        public async Task<IActionResult> YeniProjeRolPerformer(ProjeRolPerformerCreateDTO model)
        {
            return Ok(await _projeRolLogicService.YeniProjeRolPerformer(model, _identityService.GetUser));
        }

        [HttpPost("proje-rol-performer-listele")]
        public async Task<IActionResult> ProjeRolPerformerListele(ProjeIdDTO model)
        {
            return Ok(await _projeRolLogicService.ProjeRolPerformerListele(model));
        }
    }
}