using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Filters;
using OdiApp.BusinessLayer.Services.Kullanici;
using OdiApp.DTOs.IdentityDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("kullanici")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    public class KullaniciController : ControllerBase
    {
        private readonly ILogger<KullaniciController> _logger;
        private readonly IKullaniciLogicService _kullaniciService;


        public KullaniciController(ILogger<KullaniciController> logger, IKullaniciLogicService kullaniciService)
        {
            _logger = logger;
            _kullaniciService = kullaniciService;
        }
        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            throw new Exception("Test exception");
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync(SignupDTO signUpDTO)
        {
            return Ok(await _kullaniciService.SignUp(signUpDTO));
        }
    }
}
