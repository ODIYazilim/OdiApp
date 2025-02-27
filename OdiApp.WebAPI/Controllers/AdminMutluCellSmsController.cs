using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MutluCellSmsLogicServices;
using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/mutlucell")]
    [ApiController]
    [SuperAdminAuthorize]
    public class AdminMutluCellSmsController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IMutluCellSmsLogicService _mutluCellSmsLogicService;

        public AdminMutluCellSmsController(ISharedIdentityService identityService, IMutluCellSmsLogicService mutluCellSmsLogicService)
        {
            _identityService = identityService;
            _mutluCellSmsLogicService = mutluCellSmsLogicService;
        }

        [HttpPost("mutlucell-sms-ayarlari-guncelle")]
        public async Task<IActionResult> MutluCellSmsAyarlariGuncelle(MutluCellSmsAyarlari model)
        {
            return Ok(await _mutluCellSmsLogicService.AyarlariGuncelle(model, _identityService.GetUser));
        }

        [HttpGet("mutlucell-sms-ayarlari-getir")]
        public async Task<IActionResult> MutluCellSmsAyarlariGetir()
        {
            return Ok(await _mutluCellSmsLogicService.AyarlariGetir());
        }
    }
}