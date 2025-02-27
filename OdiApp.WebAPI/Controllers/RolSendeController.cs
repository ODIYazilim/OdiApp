using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.RolSendeLogicServices;
using OdiApp.DTOs.IslemlerDTOs.RolSendeDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/rol-sende")]
    [ApiController]
    [AllAuthorize]
    public class RolSendeController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IRolSendeLogicService _rolSendeLogicService;

        public RolSendeController(ISharedIdentityService identityService, IRolSendeLogicService rolSendeLogicService)
        {
            _identityService = identityService;
            _rolSendeLogicService = rolSendeLogicService;
        }

        [HttpPost("rol-sende")]
        public async Task<IActionResult> RolSende(RolSendeCreateDTO model)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _rolSendeLogicService.YeniRolSende(model, _identityService.GetUser, jwtToken));
        }
    }
}