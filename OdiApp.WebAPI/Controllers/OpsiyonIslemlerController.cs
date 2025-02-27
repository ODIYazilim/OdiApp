using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OpsiyonIslemler;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [AllAuthorize]
    public class OpsiyonIslemlerController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IOpsiyonLogicService _opsiyonLogicService;

        public OpsiyonIslemlerController(ISharedIdentityService identityService, IOpsiyonLogicService opsiyonLogicService)
        {
            _identityService = identityService;
            _opsiyonLogicService = opsiyonLogicService;
        }

        #region Yapım
        [HttpPost("opsiyon-listesine-ekle")]
        public async Task<IActionResult> OpsiyonListesineEkle(List<OpsiyonListesiCreateDTO> opsList)
        {
            return Ok(await _opsiyonLogicService.OpsiyonListesineEkle(opsList, _identityService.GetUser));
        }
        [HttpPost("opsiyon-listesi-getir")]
        public async Task<IActionResult> OpsiyonListesiGetir(ProjeIdDTO projeId)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _opsiyonLogicService.OpsiyonListesiGetir(projeId, jwtToken));
        }
        [HttpPost("opsiyon-listesinden-cikar")]
        public async Task<IActionResult> OpsiyonListesindenCikar(OpsiyonListesiIdDTO opsiyonListesiId)
        {
            return Ok(await _opsiyonLogicService.OpsiyonListesindenCikar(opsiyonListesiId));
        }
        [HttpPost("yeni-opsiyon")]
        public async Task<IActionResult> YeniOpsiyon(List<OpsiyonCreateDTO> listOpsiyon)
        {
            return Ok(await _opsiyonLogicService.YeniOpsiyon(listOpsiyon, _identityService.GetUser));
        }

        #endregion

        #region Menajer

        [HttpPost("opsiyonu-performera-yonlendir")]
        public async Task<IActionResult> PerformeraYonlendir(OpsiyonIdDTO id)
        {
            return Ok(await _opsiyonLogicService.PerformeraYonlendir(id));
        }

        #endregion

        #region Menajer ve Performer

        [HttpPost("opsiyonu-geri-cevir")]
        public async Task<IActionResult> GeriCevir(OpsiyonGeriCevirDTO dto)
        {
            return Ok(await _opsiyonLogicService.GeriCevir(dto));
        }

        [HttpPost("opsiyonu-yanitla")]
        public async Task<IActionResult> Yanitla(OpsiyonYanitlaDTO dto)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _opsiyonLogicService.Yanitla(dto, _identityService.GetUser, jwtToken));
        }
        [HttpPost("opsiyonu-menajer-listesi")]
        public async Task<IActionResult> OpsiyonMenajerListesi(MenajerOpsiyonListesiInputDTO input)
        {

            return Ok(await _opsiyonLogicService.MenajerOpsiyonListesiGetir(input));
        }
        [HttpPost("opsiyonu-menajer-inceledi")]
        public async Task<IActionResult> MenajerInceledi(OpsiyonIdDTO opsId)
        {

            return Ok(await _opsiyonLogicService.MenajerInceledi(opsId));
        }
        [HttpPost("opsiyon-menajer-onayi")]
        public async Task<IActionResult> MenajerOnayi(OpsiyonIdDTO opsId)
        {

            return Ok(await _opsiyonLogicService.MenajerOnayi(opsId));
        }
        [HttpPost("opsiyonu-performera-ilet")]
        public async Task<IActionResult> OpsiyonuPerformeraIlet(OpsiyonIdDTO opsId)
        {

            return Ok(await _opsiyonLogicService.OpsiyonuPerformeraIlet(opsId));
        }
        [HttpPost("opsiyonu-performer-inceledi")]
        public async Task<IActionResult> PerformerInceledi(OpsiyonIdDTO opsId)
        {

            return Ok(await _opsiyonLogicService.PerformerInceledi(opsId));
        }

        [HttpPost("opsiyon-detay-getir")]
        public async Task<IActionResult> OpsiyonDetayGetir(OpsiyonIdDTO opsId)
        {

            return Ok(await _opsiyonLogicService.OpsiyonGetir(opsId));
        }
        #endregion
    }
}
