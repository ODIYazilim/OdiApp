using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.ProjePerformer;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiIzlemeListesiDTO;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;
using OdiApp.DTOs.IslemlerDTOs.ProjePerformer;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]

    [AllAuthorize]
    public class OdiIslemlerController : ControllerBase
    {
        private readonly IProjePerformerLogicService _projePerformerLogicService;
        private readonly IOdiIslemLogicService _odiIslemLogicService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IPerformerOdiLogicService _performerOdiLogicService;
        public OdiIslemlerController(IProjePerformerLogicService projePerformerLogicService, IOdiIslemLogicService odiIslemLogicService, ISharedIdentityService sharedIdentityService, IPerformerOdiLogicService performerOdiLogicService)
        {
            _projePerformerLogicService = projePerformerLogicService;
            _odiIslemLogicService = odiIslemLogicService;
            _sharedIdentityService = sharedIdentityService;
            _performerOdiLogicService = performerOdiLogicService;
        }


        [HttpPost("proje-onerilen-performer-list")]
        //[YapimAuthorize]
        public async Task<IActionResult> ProjeOnerilenPerformerList(OnerilenPerformerInputDTO input)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projePerformerLogicService.ProjeOnerilenPerformerList(jwtToken));
        }
        #region Izleme Listeleri

        [HttpPost("menajer-odi-izleme-listesi")]
        public async Task<IActionResult> MenajerOdiIzlemeListesi(MenajerIdDTO menajerId)
        {
            return Ok(await _odiIslemLogicService.MenajerIzlemeListesi(menajerId));
        }

        [HttpPost("performer-odi-izleme-listesi")]
        public async Task<IActionResult> PerformerOdiIzlemeListesi(PerformerIdDTO model)
        {
            return Ok(await _odiIslemLogicService.PerformerOdiIzlemeListesi(model));
        }

        [HttpPost("yapim-odi-izleme-listesi")]
        public async Task<IActionResult> YapimOdiIzlemeListesi(ProjeIdDTO projeId)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _odiIslemLogicService.YapımIzlemeListesi(projeId, jwtToken));
        }
        #endregion

        #region Odi Talep
        [HttpPost("yeni-odi-talep")]
        public async Task<IActionResult> YeniOdiTalebi(List<OdiTalepCreateDTO> odiTalepList)
        {
            return Ok(await _odiIslemLogicService.YeniOdiTalep(odiTalepList, _sharedIdentityService.GetUser));
        }

        [HttpPost("yapim-odi-talep-listesi")]
        public async Task<IActionResult> YapimOdiTalepListesi(KullaniciIdDTO id)
        {
            return Ok(await _odiIslemLogicService.YapimOdiTalepListesi(id));
        }

        [HttpPost("menajer-odi-talep-listesi")]
        public async Task<IActionResult> MenajerOdiTalepListesi(KullaniciIdDTO id)
        {
            return Ok(await _odiIslemLogicService.MenajerOdiTalepListesi(id));
        }

        [HttpPost("odi-talep-menajer-goruldu")]
        public async Task<IActionResult> OdiTalepMenajerGoruldu(OdiTalepIdDTO talepId)
        {
            return Ok(await _odiIslemLogicService.OdiTalepMenajerGoruldu(talepId, _sharedIdentityService.GetUser));
        }


        [HttpPost("odi-talep-menajer-red")]
        public async Task<IActionResult> OdiTalepMenajerRed(OdiTalepRedInputDTO red)
        {
            return Ok(await _odiIslemLogicService.OdiTalepMenajerRed(red, _sharedIdentityService.GetUser));
        }


        [HttpPost("odi-talep-performera-ilet")]
        public async Task<IActionResult> OdiTalepPerformeraIlet(OdiTalepPerformeraIletInput ilet)
        {
            return Ok(await _odiIslemLogicService.OdiTalepPerformeraIlet(ilet, _sharedIdentityService.GetUser));
        }

        [HttpPost("odi-talep-performer-goruldu")]
        public async Task<IActionResult> OdiTalepToPerformerGoruldu(OdiTalepIdDTO odiTalepId)
        {
            return Ok(await _odiIslemLogicService.OdiTalepPerformerGoruldu(odiTalepId, _sharedIdentityService.GetUser));
        }
        [HttpPost("odi-talep-performer-red")]
        public async Task<IActionResult> OdiTalepPerformerRed(OdiTalepRedInputDTO red)
        {
            return Ok(await _odiIslemLogicService.OdiTalepPerformerRed(red, _sharedIdentityService.GetUser));
        }
        [HttpPost("odi-talep-menajer-performer-red-onayi")]
        public async Task<IActionResult> OdiTalepMenajerPerformerRedOnayi(OdiTalepIdDTO talepId)
        {
            return Ok(await _odiIslemLogicService.OdiTalepMenajerPerformerRedOnayi(talepId, _sharedIdentityService.GetUser));
        }
        [HttpPost("odi-talep-menajer-red-iptal")]
        public async Task<IActionResult> OdiTalepMenajerRedIptal(OdiTalepIdDTO talepId)
        {
            return Ok(await _odiIslemLogicService.OdiTalepMenajerRedIptal(talepId, _sharedIdentityService.GetUser));
        }
        [HttpPost("odi-talep-menajer-performer-red-iptal")]
        public async Task<IActionResult> OdiTalepMenajerPerformerRedIptal(OdiTalepIdDTO talepId)
        {
            return Ok(await _odiIslemLogicService.OdiTalepMenajerPerformerRedIptal(talepId, _sharedIdentityService.GetUser));
        }
        #endregion

        #region Performer Odi
        [HttpPost("performer-odi-yukleme")]
        public async Task<IActionResult> YeniPerformerOdi(PerformerOdiCreateDTO odi)
        {
            return Ok(await _performerOdiLogicService.YeniPerformerOdi(odi, _sharedIdentityService.GetUser));
        }

        [HttpPost("performer-odi-guncelle")]
        public async Task<IActionResult> PerformerOdiGuncelle(PerformerOdiUpdateDTO odi)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiGuncelle(odi, _sharedIdentityService.GetUser));
        }

        [HttpPost("performer-odi-menajer-onayina-gonder")]
        public async Task<IActionResult> PerformerOdiMenajerOnayinaGonder(PerformerOdiIdDTO performerOdiId)
        {
            return Ok(await _performerOdiLogicService.MenajerOnayinaGonder(performerOdiId.ToString()));
        }

        [HttpPost("performer-odi-detay-getir")]
        public async Task<IActionResult> PerformerOdiDetayGetir(OdiTalepIdDTO odiTalepId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiDetayGetir(odiTalepId));
        }

        [HttpPost("performer-odi-menajer-inceledi")]
        public async Task<IActionResult> PerformerOdiMenajerInceledi(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiMenajerInceledi(performerId));
        }

        [HttpPost("performer-odi-menajer-aktif-pasif")]
        public async Task<IActionResult> PerformerOdiMenajerAktifPasif(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiMenajerAktifPasif(performerId));
        }

        [HttpPost("performer-odi-menajer-gizle")]
        public async Task<IActionResult> PerformerOdiMenajerGizle(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiMenajerGizle(performerId));
        }

        [HttpPost("performer-odi-menajer-onayi")]
        public async Task<IActionResult> PerformerOdiMenajerOnayi(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiMenajerOnayi(performerId));
        }

        [HttpPost("performer-odi-menajer-tekrar-cek-talebi")]
        public async Task<IActionResult> PerformerOdiMenajerTekrarCekTalebi(PerformerOdiMenajerTekrarCekInputDTO tekrarcek)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiMenajerTekrarCekTalebi(tekrarcek));
        }
        //
        [HttpPost("performer-odi-yapim-inceledi")]
        public async Task<IActionResult> PerformerOdiYapimInceledi(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiYapimInceledi(performerId));
        }

        [HttpPost("performer-odi-yapim-aktif-pasif")]
        public async Task<IActionResult> PerformerOdiYapimAktifPasif(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiYapimAktifPasif(performerId));
        }

        [HttpPost("performer-odi-yapim-gizle")]
        public async Task<IActionResult> PerformerOdiYapimGizle(PerformerOdiIdDTO performerId)
        {
            return Ok(await _performerOdiLogicService.PerformerOdiYapimGizle(performerId));
        }

        #endregion

    }
}
