using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Services.OdemeLogicServices.IyzicoLogicServices;
using OdiApp.DTOs.OdemeDTOs.IyzicoDtos;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IIyzicoLogicService _iyzicoLogicService;

        public TestController(IIyzicoLogicService iyzicoLogicService)
        {
            _iyzicoLogicService = iyzicoLogicService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("test");
        }

        [HttpPost("OdemeYapTest")]
        public async Task<IActionResult> OdemeYapTest(OdemeYapTestInputDTO model)
        {
            return Ok(await _iyzicoLogicService.OdemeYapTest(model));
        }

        [HttpPost("TestAbonelikUrunVePlanOlusturma")]
        public async Task<IActionResult> TestAbonelikUrunVePlanOlusturma()
        {
            return Ok(await _iyzicoLogicService.TestAbonelikUrunVePlanOlusturma());
        }

        [HttpPost("AbonelikUrunListeleme")]
        public async Task<IActionResult> AbonelikUrunListeleme()
        {
            return Ok(await _iyzicoLogicService.AbonelikUrunListeleme());
        }

        [HttpPost("UrunPlanListeleme")]
        public async Task<IActionResult> UrunPlanListeleme()
        {
            return Ok(await _iyzicoLogicService.UrunPlanListeleme());
        }

        [HttpPost("AbonelikBaslatma")]
        public async Task<IActionResult> AbonelikBaslatma()
        {
            return Ok(await _iyzicoLogicService.AbonelikBaslatma());
        }

        [HttpPost("AbonelikUpgrade")]
        public async Task<IActionResult> AbonelikUpgrade()
        {
            return Ok(await _iyzicoLogicService.AbonelikUpgrade());
        }

        [HttpPost("KartKaydet")]
        public async Task<IActionResult> KartKaydet()
        {
            return Ok(await _iyzicoLogicService.KartKaydet());
        }

        [HttpPost("VarolanKullaniciyaKartEkle")]
        public async Task<IActionResult> VarolanKullaniciyaKartEkle()
        {
            return Ok(await _iyzicoLogicService.VarolanKullaniciyaKartEkle());
        }

        [HttpPost("KullaniciKartListesi")]
        public async Task<IActionResult> KullaniciKartListesi()
        {
            return Ok(await _iyzicoLogicService.KullaniciKartListesi());
        }
    }
}