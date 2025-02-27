using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MesajlasmaLogicServices;
using OdiApp.DTOs.BildirimDTOs.Mesajlasma;
using OdiApp.DTOs.Kullanici;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/mesajlasma")]
    [ApiController]
    [AllAuthorize]
    public class MesajlasmaController : ControllerBase
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IMesajLogicService _mesajLogicService;

        public MesajlasmaController(ISharedIdentityService identityService, IMesajLogicService mesajLogicService)
        {
            _identityService = identityService;
            _mesajLogicService = mesajLogicService;
        }

        [HttpPost("yeni-mesaj")]
        public async Task<IActionResult> YeniMesaj(MesajCreateDTO mesaj)
        {
            return Ok(await _mesajLogicService.YeniMesaj(mesaj, _identityService.GetUser));
        }

        [HttpPost("yeni-mesaj-detay")]
        public async Task<IActionResult> YeniMesajDetay(MesajDetayCreateDTO mesajDetay)
        {
            return Ok(await _mesajLogicService.YeniMesajDetay(mesajDetay, _identityService.GetUser));
        }

        [HttpPost("mesaj-listesi")]
        public async Task<IActionResult> MesajListesi(KullaniciIdDTO kullaniciId)
        {
            return Ok(await _mesajLogicService.MesajListesi(kullaniciId));
        }

        [HttpPost("mesaj-detay-listesi")]
        public async Task<IActionResult> MesajDetayListesi(MesajDetayListesiInputDTO mesajDetayListesiInputDTO)
        {
            return Ok(await _mesajLogicService.MesajDetayListesi(mesajDetayListesiInputDTO));
        }

        [HttpPost("mesaj-goruldu")]
        public async Task<IActionResult> MesajGoruldu(MesajDetayIdDTO mesajDetayId)
        {
            return Ok(await _mesajLogicService.MesajGoruldu(mesajDetayId));
        }

        [HttpPost("mesaj-goruldu-liste")]
        public async Task<IActionResult> MesajGorulduListe(List<MesajDetayIdDTO> mesajDetayIdListe)
        {
            return Ok(await _mesajLogicService.MesajGorulduListe(mesajDetayIdListe));
        }

        [HttpPost("mesaj-silme")]
        public async Task<IActionResult> MesajSilme(MesajIdDTO mesajId)
        {
            return Ok(await _mesajLogicService.MesajSilme(mesajId));
        }

        [HttpPost("mesaj-silme-liste")]
        public async Task<IActionResult> MesajSilmeListe(List<MesajIdDTO> mesajIdList)
        {
            return Ok(await _mesajLogicService.MesajSilmeListe(mesajIdList));
        }

        [HttpPost("mesaj-detay-silme")]
        public async Task<IActionResult> MesajDetaySilme(MesajDetayIdDTO mesajDetayId)
        {
            return Ok(await _mesajLogicService.MesajDetaySilme(mesajDetayId));
        }

        [HttpPost("mesaj-detay-silme-liste")]
        public async Task<IActionResult> MesajDetaySilmeListe(List<MesajDetayIdDTO> mesajDetayIdList)
        {
            return Ok(await _mesajLogicService.MesajDetaySilmeListe(mesajDetayIdList));
        }
    }
}