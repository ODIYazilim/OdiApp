using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.BankaDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.DilDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.KayitTuruDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SabitMetinDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SehirDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SosyalMedyaDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SSSDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.TelefonKoduDataServices;
using OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.UlkeServices;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.UygulamaBilgileriDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.BankaDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.DilDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.KayitGrubuDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.KayitTuruDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SabitMetinDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SosyalMedyaDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SSSDTOs;
using OdiApp.DTOs.UygulamaBilgileriDTOs.TelefonKoduDtos;
using OdiApp.DTOs.UygulamaBilgileriDTOs.UlkeDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/uygulama-bilgileri")]
    [ApiController]
    public class UygulamaBilgileriController : ControllerBase
    {
        private readonly ISehirService _sehirServ;
        private readonly IDilService _dilServ;
        private readonly IKayitTuruService _kayitTuruServ;
        private readonly ITelefonKoduService _telefonKoduServ;
        private readonly IGecerliDilService _dilService;
        private readonly IMapper _mapper;
        private readonly ISosyalMedyaService _sosyalMedyaService;
        private readonly IBankaService _bankaService;
        private readonly ISabitMetinService _sabitMetinService;
        private readonly ISSSService _sssService;
        private readonly IUlkeService _ulkeService;

        public UygulamaBilgileriController(IMapper mapper, ISehirService sehirServ, IDilService dilServ, IKayitTuruService kayitTuruServ, ITelefonKoduService telefonKoduServ, IGecerliDilService gecerliDilService, ISosyalMedyaService sosyalMedyaService, IBankaService bankaService, ISabitMetinService sabitMetinService, ISSSService sssService, IUlkeService ulkeService)
        {
            _sehirServ = sehirServ;
            _dilServ = dilServ;
            _mapper = mapper;
            _kayitTuruServ = kayitTuruServ;
            _telefonKoduServ = telefonKoduServ;
            _dilService = gecerliDilService;
            _sosyalMedyaService = sosyalMedyaService;
            _bankaService = bankaService;
            _sabitMetinService = sabitMetinService;
            _sssService = sssService;
            _ulkeService = ulkeService;
        }

        [HttpPost("sabit-metin-listesi")]
        public async Task<IActionResult> BasitMetinListesiGetir(SabitMetinListeInputDTO model)
        {
            int dilId = await _dilService.GecerliDil();
            List<SabitMetinOutputDTO> list = await _sabitMetinService.SabitMetinOutputListesi(model, dilId);
            return Ok(OdiResponse<List<SabitMetinOutputDTO>>.Success("Basit metin listesi getirildi", list, 200));
        }

        [HttpPost("sss-listesi")]
        public async Task<IActionResult> SSSListesiGetir(SSSListeInputDTO model)
        {
            int dilId = await _dilService.GecerliDil();
            List<SSSOutputDTO> list = await _sssService.SSSOutputListesi(model, dilId);
            return Ok(OdiResponse<List<SSSOutputDTO>>.Success("SSS listesi getirildi", list, 200));
        }

        [HttpGet("sosyal-medya-listesi-getir")]
        public async Task<IActionResult> SosyalMedyaListesiGetir()
        {
            List<SosyalMedyaOutputDTO> list = await _sosyalMedyaService.SosyalMedyaOutputListesi();
            return Ok(OdiResponse<List<SosyalMedyaOutputDTO>>.Success("Sosyal medya listesi getirildi", list, 200));
        }

        [HttpGet("banka-listesi")]
        public async Task<IActionResult> BankaListesi()
        {
            List<BankaOutputDTO> list = await _bankaService.BankaOutputListesi();
            return Ok(OdiResponse<List<BankaOutputDTO>>.Success("Banka listesi getirildi", list, 200));
        }

        [HttpGet("sehir-listesi")]
        public async Task<IActionResult> SehirListesi()
        {
            List<Sehir> list = await _sehirServ.SehirListesi();
            return Ok(OdiResponse<List<SehirDTo>>.Success("Şehir listesi getirildi", _mapper.Map<List<SehirDTo>>(list), 200));
        }

        [HttpGet("aktif-dil-listesi")]
        public async Task<IActionResult> DilListesi()
        {
            List<Dil> list = await _dilServ.AktifDilListesi();
            return Ok(OdiResponse<List<DilDTO>>.Success("Dil listesi getirildi", _mapper.Map<List<DilDTO>>(list), 200));
        }

        [HttpGet("kayit-turu-listesi")]
        public async Task<IActionResult> KayitTuruListesi()
        {
            //Fonksiyonlar.WriteTXT("KayitTuruListesi controller");
            List<KayitGrubuDTO> list = _mapper.Map<List<KayitGrubuDTO>>(await _kayitTuruServ.AktifKayitGrubuListesi(await _dilService.GecerliDil()));
            foreach (var item in list)
            {
                List<KayitTuru> ktList = await _kayitTuruServ.AktifKayitTuruListesi(await _dilService.GecerliDil(), item.GrupKodu);
                if (ktList != null) item.KayitTurleri = _mapper.Map<List<KayitTuruDTO>>(ktList);
            }

            List<KayitGrubuDTO> gecicilist = new List<KayitGrubuDTO>();
            foreach (var item in list)
            {
                if (!(item.GrupKodu == KayitGrupKodlari.YetenekTemsilcisi || item.GrupKodu == KayitGrupKodlari.HizmetSaglayici || item.GrupKodu == KayitGrupKodlari.OdiYoneticisi))
                {
                    gecicilist.Add(item);
                }
            }

            return Ok(OdiResponse<List<KayitGrubuDTO>>.Success("Kayıt türü listesi getirildi", gecicilist, 200));
        }

        [HttpGet("rol-turleri-listesi")]
        public async Task<IActionResult> RolTurleriListesi()
        {
            List<KayitTuru> list = await _kayitTuruServ.AktifKayitTuruListesi(await _dilService.GecerliDil(), KayitGrupKodlari.Yetenek);

            List<RolTuruDTO> resultList = list.Select(x => new RolTuruDTO() { RolTuru = x.Tur, RolTuruKodu = x.TurKodu }).ToList();

            return Ok(OdiResponse<List<RolTuruDTO>>.Success("Rol türü listesi getirildi", resultList, 200));
        }

        [HttpGet("telefon-kodu-listesi")]
        public async Task<IActionResult> TelefonKoduListesi()
        {
            List<TelefonKodu> list = await _telefonKoduServ.AktifTelefonKodlariListesi();
            return Ok(OdiResponse<List<TelefonKoduDTO>>.Success("Telefon kodu listesi getirildi", _mapper.Map<List<TelefonKoduDTO>>(list), 200));
        }

        [HttpGet("ulke-listesi")]
        public async Task<IActionResult> UlkeListesi()
        {
            List<UlkeDTO> list = await _ulkeService.UlkeListesi();
            return Ok(OdiResponse<List<UlkeDTO>>.Success("Ülke listesi getirildi", list, 200));
        }
    }
}