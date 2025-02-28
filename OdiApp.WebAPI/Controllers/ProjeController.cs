using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.KayitliRolLogicServices;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeBilgileri;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolBilgileri;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolOdiBilgileri;
using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.ProjelerDTOs.OdiFotograf;
using OdiApp.DTOs.ProjelerDTOs.OdiSes;
using OdiApp.DTOs.ProjelerDTOs.OdiSoru;
using OdiApp.DTOs.ProjelerDTOs.OdiVideo;
using OdiApp.DTOs.ProjelerDTOs.PerformerProje;
using OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolAnketSorusuDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolOzellikDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolOdiBilgisi;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    //[YapimAuthorize]
    public class ProjeController : ControllerBase
    {
        private readonly IGecerliDilService _dilService;
        private readonly ISharedIdentityService _identityService;
        private readonly IProjeLogicService _projeService;
        private readonly IProjeRolOdiLogicService _projeRolOdiLogicService;
        private readonly IProjeRolLogicService _projeRolLogicService;
        private readonly IKayitliRolLogicService _kayitliRolLogicService;

        public ProjeController(IGecerliDilService dilService, ISharedIdentityService identityService, IProjeLogicService projeLogicService, IProjeRolLogicService projeRolLogicService, IProjeRolOdiLogicService projeRolOdiService, IKayitliRolLogicService kayitliRolLogicService)
        {
            _dilService = dilService;
            _identityService = identityService;
            _projeService = projeLogicService;
            _projeRolLogicService = projeRolLogicService;
            _projeRolOdiLogicService = projeRolOdiService;
            _kayitliRolLogicService = kayitliRolLogicService;
        }

        #region Rol Özellikleri

        [HttpPost("proje-rol-filtre-getir")]
        public async Task<IActionResult> ProjeRolFiltreGetir(ProjeRolIdDTO model)
        {
            return Ok(await _projeRolLogicService.ProjeRolFiltreGetir(model));
        }

        [HttpPost("rol-ozellik-ayarlari")]
        public async Task<IActionResult> RolOzellikAyarlari(KayitTuruKodlariDTO kayitTuruKodlariDTO)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeRolLogicService.RolOzellikAyarlari(kayitTuruKodlariDTO, jwtToken));
        }

        [HttpPost("yeni-proje-rol-ozellik")]
        public async Task<IActionResult> YeniProjeRolOzellik(ProjeRolOzellikCreateDTO ozellikDTO)
        {
            return Ok(await _projeRolLogicService.YeniProjeRolOzellik(ozellikDTO, _identityService.GetUser));
        }

        [HttpPost("proje-rol-ozellik-guncelle")]
        public async Task<IActionResult> ProjeRolOzellikGuncelle(ProjeRolOzellikUpdateDTO ozellikDTO)
        {
            return Ok(await _projeRolLogicService.ProjeRolOzellikGuncelle(ozellikDTO, _identityService.GetUser));
        }

        [HttpPost("proje-rol-ozellik-sil")]
        public async Task<IActionResult> ProjeRolOzellikSil(ProjeRolIdDTO projeRolIdDTO)
        {
            return Ok(await _projeRolLogicService.ProjeRolOzellikSil(projeRolIdDTO));
        }

        [HttpPost("proje-rol-ozellik-getir")]
        public async Task<IActionResult> ProjeRolOzellikGetir(ProjeRolIdDTO projeRolIdDTO)
        {
            return Ok(await _projeRolLogicService.ProjeRolOzellikGetir(projeRolIdDTO));
        }

        #endregion

        #region Proje Bilgileri

        [HttpPost("proje-detay-getir")]
        public async Task<IActionResult> ProjeDetayGetir(ProjeIdDTO id)
        {
            return Ok(await _projeService.ProjeGetir(id));
        }

        [HttpGet("proje-ayarlari-getir")]
        public async Task<IActionResult> ProjeAyarlariGetir()
        {
            int dil = await _dilService.GecerliDil();
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

            return Ok(await _projeService.ProjeAyarlariGetir(dil, jwtToken));
        }

        [HttpPost("yeni-proje")]
        public async Task<IActionResult> YeniProje(ProjeCreateDTO proje)
        {
            int dil = await _dilService.GecerliDil();
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeService.YeniProje(proje, _identityService.GetUser, dil, jwtToken));
        }

        [HttpPost("proje-guncelle")]
        public async Task<IActionResult> ProjeGuncelle(ProjeUpdateDTO proje)
        {
            int dil = await _dilService.GecerliDil();
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeService.ProjeGuncelle(proje, _identityService.GetUser, jwtToken));
        }

        [HttpPost("proje-kayit-ayarlari")]
        public async Task<IActionResult> ProjeKayitAyarlari(ProjeKayitAyarlariDTO model)
        {
            return Ok(await _projeService.ProjeKayitAyarlari(model, _identityService.GetUser));
        }

        [HttpPost("proje-yayina-al")]
        public async Task<IActionResult> ProjeYayinaAl(ProjeIdDTO model)
        {
            return Ok(await _projeService.ProjeYayinaAl(model, _identityService.GetUser));
        }

        [HttpPost("proje-yayini-durdurma")]
        public async Task<IActionResult> ProjeYayiniDurdurma(ProjeIdDTO model)
        {
            return Ok(await _projeService.ProjeYayiniDurdurma(model, _identityService.GetUser));
        }

        [HttpGet("tum-projeler")]
        public async Task<IActionResult> TumProjeler()
        {
            int dil = await _dilService.GecerliDil();
            return Ok(await _projeService.TumAcikProjeler(dil));
        }

        [HttpPost("proje-fotografi-arama")]
        public async Task<IActionResult> ProjeFotografiArama(ProjeFotografAramaDto ara)
        {
            return Ok(await _projeService.ProjeFotografArama(ara));
        }

        [HttpPost("proje-fotografi-guncelle")]
        public async Task<IActionResult> ProjeFotografiGuncelle(ProjeFotografiUpdateDTO foto)
        {
            return Ok(await _projeService.ProjeFotografiGuncelle(foto));
        }

        #endregion

        #region Kayıtlı Rol

        [HttpPost("rol-kaydet")]
        public async Task<IActionResult> RolKaydet(ProjeRolIdDTO projeRolIdDTO)
        {
            return Ok(await _kayitliRolLogicService.RolKaydet(projeRolIdDTO, _identityService.GetUser));
        }

        #endregion

        #region Proje Rol

        [HttpGet("rol-bilgisi-ayarlari")]
        public async Task<IActionResult> RolBilgisiAyarlari()
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeRolLogicService.RolBilgisiAyarlari(jwtToken));
        }

        [HttpPost("yeni-proje-rol")]
        public async Task<IActionResult> YeniProjeRol(ProjeRolCreateDTO projeRol)
        {
            return Ok(await _projeRolLogicService.YeniProjeRol(projeRol, _identityService.GetUser));
        }

        [HttpPost("proje-rol-kopyala")]
        public async Task<IActionResult> ProjeRolKopyala(ProjeRolIdDTO model)
        {
            return Ok(await _projeRolLogicService.ProjeRolKopyala(model, _identityService.GetUser));
        }

        [HttpPost("proje-rol-getir")]
        public async Task<IActionResult> ProjeRolGetir(ProjeRolIdDTO id)
        {
            return Ok(await _projeRolLogicService.ProjeRolGetir(id));
        }

        [HttpPost("alternatif-butce-getir")]
        public async Task<IActionResult> AlternatifButceGetir(ProjeRolIdDTO model)
        {
            return Ok(await _projeRolLogicService.AlternatifButceGetir(model));
        }

        [HttpPost("alternatif-butce-guncelle")]
        public async Task<IActionResult> AlternatifButceGuncelle(AlternatifButceUpdateDTO model)
        {
            return Ok(await _projeRolLogicService.AlternatifButceGuncelle(model, _identityService.GetUser));
        }

        [HttpPost("proje-rol-listesi-getir")]
        public async Task<IActionResult> ProjeRolListesiGetir(ProjeIdDTO id)
        {
            return Ok(await _projeRolLogicService.ProjeRolleriGetir(id));
        }

        [HttpPost("proje-rol-guncelle")]
        public async Task<IActionResult> ProjeRolGuncelle(ProjeRolUpdateDTO rol)
        {
            return Ok(await _projeRolLogicService.ProjeRolGuncelle(rol, _identityService.GetUser));
        }

        [HttpPost("proje-rol-opsiyon-detay")]
        public async Task<IActionResult> ProjeRolOpsiyonDetay(ProjeIdDTO model)
        {
            return Ok(await _projeRolLogicService.ProjeRolOpsiyonDetay(model));
        }

        #endregion

        #region Proje Rol Anket Sorusu

        [HttpPost("proje-rol-anket-sorusu-guncelle")]
        public async Task<IActionResult> ProjeRolAnketSorusuGuncelle(List<ProjeRolAnketSorusuUpdateDTO> model)
        {
            return Ok(await _projeRolLogicService.ProjeRolAnketSorusuGuncelle(model, _identityService.GetUser));
        }

        [HttpPost("yeni-proje-rol-anket-sorusu")]
        public async Task<IActionResult> YeniProjeRolAnketSorusu(List<ProjeRolAnketSorusuCreateDTO> model)
        {
            return Ok(await _projeRolLogicService.YeniProjeRolAnketSorusu(model, _identityService.GetUser));
        }

        [HttpPost("proje-rol-anket-sorusu-sil")]
        public async Task<IActionResult> ProjeRolAnketSorusuSil(List<ProjeRolAnketSorusuIdDTO> model)
        {
            return Ok(await _projeRolLogicService.ProjeRolAnketSorusuSil(model));
        }

        [HttpPost("proje-anket-sorusu-liste")]
        public async Task<IActionResult> ProjeRolAnketSorusuListeGetir(ProjeRolIdDTO id)
        {
            return Ok(await _projeRolLogicService.ProjeRolAnketSorusuListeGetir(id));
        }

        #endregion

        #region Proje Rol Odi

        [HttpPost("yeni-proje-rol-odi")]
        public async Task<IActionResult> YeniProjeRolOdi(ProjeRolOdiCreateDTO odi)
        {
            return Ok(await _projeRolOdiLogicService.YeniProjeRolOdi(odi, _identityService.GetUser));
        }

        [HttpPost("proje-rol-odi-guncelle")]
        public async Task<IActionResult> ProjeRolOdiGuncelle(ProjeRolOdiUpdateDTO odi)
        {
            return Ok(await _projeRolOdiLogicService.ProjeRolOdiGuncelle(odi, _identityService.GetUser));
        }

        [HttpPost("rol-odi-bilgilerini-getir")]
        public async Task<IActionResult> RolOdiBilgileriniGetir(ProjeRolIdDTO projeRolId)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiBilgileriniGetir(projeRolId));
        }

        #region Fotomatik

        [HttpPost("rol-odi-yeni-fotomatik")]
        public async Task<IActionResult> YeniProjeRolOdiFotomatik(RolOdiFotoCreateDTO fotomatik)
        {
            return Ok(await _projeRolOdiLogicService.YeniRolOdiFoto(fotomatik, _identityService.GetUser));
        }

        [HttpPost("rol-odi-fotomatik-poz-listesi-guncelle")]
        public async Task<IActionResult> RolOdiFotomatikPozListesiGuncelle(RolOdiFotoPozUpdateListDTO updateDTO)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiFotoPozGuncelle(updateDTO, _identityService.GetUser));
        }

        [HttpPost("rol-odi-fotomatik-yonetmen-notu-guncelle")]
        public async Task<IActionResult> RolOdiFotomatikYonetmenNotuGuncelle(RolOdiFotoYonetmenNotuUpdateDTO not)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiFotoYonetmenNotGuncelle(not, _identityService.GetUser));
        }

        [HttpPost("rol-odi-fotomatik-ornek-foto-listesi-guncelle")]
        public async Task<IActionResult> RolOdiFotomatikYonetmenNotuGuncelle(RolOdiFotoOrnekFotografUpdateListDTO updateDTO)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiFotoOrnekFotoListesiGuncelle(updateDTO, _identityService.GetUser));
        }

        #endregion

        #region Sesmatik

        [HttpPost("rol-odi-yeni-sesmatik")]
        public async Task<IActionResult> YeniProjeRolOdiSesmatik(RolOdiSesmatikCreateDTO sesmatik)
        {
            return Ok(await _projeRolOdiLogicService.YeniRolOdiSes(sesmatik, _identityService.GetUser));
        }

        [HttpPost("rol-odi-ses-yonetmen-notu-guncelle")]
        public async Task<IActionResult> RolOdiSesmatikYonetmenNotuGuncelle(RolOdiSesYonetmenNotuUpdateDTO update)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiSesYonetmenNotuGuncelle(update, _identityService.GetUser));
        }

        [HttpPost("rol-odi-ses-senaryo-guncelle")]
        public async Task<IActionResult> RolOdiSesmatikSenaryoGuncelle(RolOdiSesSenaryoUpdateDTO update)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiSesSenaryoGuncelle(update, _identityService.GetUser));
        }

        [HttpPost("rol-odi-ses-listesi-guncelle")]
        public async Task<IActionResult> RolOdiSesmatikSesListesiGuncelle(RolOdiSesUpdateListDTO update)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiSesListesiGuncelle(update, _identityService.GetUser));
        }

        #endregion

        #region Videomatik

        [HttpPost("rol-odi-yeni-videomatik")]
        public async Task<IActionResult> RolOdiYeniVideomatik(RolOdiVideomatikCreateDTO videomatik)
        {
            return Ok(await _projeRolOdiLogicService.YeniRolOdiVideomatik(videomatik, _identityService.GetUser));
        }
        [HttpPost("rol-odi-videomatik-video-guncelle")]
        public async Task<IActionResult> RolOdiVideoGuncelle(RolOdiVideoUpdateDTO video)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiVideoGuncelle(video, _identityService.GetUser));
        }
        [HttpPost("rol-odi-videomatik-video-detay-guncelle")]
        public async Task<IActionResult> RolOdiVideoDetayGuncelle(RolOdiVideoDetayUpdateListDTO detayUpdate)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiVideoDetayGuncelle(detayUpdate, _identityService.GetUser));
        }
        [HttpPost("rol-odi-videomatik-ornek-oyun-guncelle")]
        public async Task<IActionResult> RolOdiVideoOrnekOyunGuncelle(RolOdiVideoOrnekOyunUpdateDTO videoOrnekOyun)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiVideoOrnekOyunGuncelle(videoOrnekOyun, _identityService.GetUser));
        }
        [HttpPost("rol-odi-videomatik-yeni-ornek-oyun")]
        public async Task<IActionResult> YeniRolOdiVideoOrnekOyu(RolOdiVideoOrnekOyunCreateDTO videoOrnekOyun)
        {
            return Ok(await _projeRolOdiLogicService.YeniRolOdiVideoOrnek(videoOrnekOyun, _identityService.GetUser));
        }
        [HttpPost("rol-odi-videomatik-ornek-oyun-sil")]
        public async Task<IActionResult> RolOdiVideoOrnekOyunSil(RolOdiVideoOrnekOyunIdDTO id)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiVideoOrnekOyunSil(id));
        }
        [HttpPost("rol-odi-videomatik-senaryo-guncelle")]
        public async Task<IActionResult> RolOdiVideoSenaryoGuncelle(RolOdiVideoSenaryoUpdateDTO senaryo)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiVideoSenaryoGuncelle(senaryo, _identityService.GetUser));
        }

        [HttpPost("rol-odi-videomatik-yonetmen-notu-guncelle")]
        public async Task<IActionResult> RolOdiVideoYonetmenNotuGuncelle(RolOdiVideoYonetmenNotuUpdateDTO not)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiVideoYonetmenNotuGuncelle(not, _identityService.GetUser));
        }
        #endregion

        #region Sorumatik

        [HttpPost("rol-odi-yeni-sorumatik")]
        public async Task<IActionResult> RolOdiYeniSorumatik(RolOdiSorumatikCreateDTO sorumatik)
        {
            return Ok(await _projeRolOdiLogicService.YeniRolOdiSorumatik(sorumatik, _identityService.GetUser));
        }
        [HttpPost("rol-odi-soru-guncelle")]
        public async Task<IActionResult> RolOdiSoruGuncelle(RolOdiSoruUpdateDTO sorumatik)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiSoruGuncelle(sorumatik, _identityService.GetUser));
        }

        [HttpPost("rol-odi-soru-sil")]
        public async Task<IActionResult> RolOdiSoruSil(RolOdiSoruIdDTO id)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiSoruSil(id));
        }

        [HttpPost("rol-odi-soru-aciklama-guncelle")]
        public async Task<IActionResult> RolOdiSoruAciklamaGuncelle(RolOdiSoruAciklamaUpdateDTO aciklama)
        {
            return Ok(await _projeRolOdiLogicService.RolOdiSoruAciklamaGuncelle(aciklama, _identityService.GetUser));
        }

        #endregion

        #endregion

        #region Oyuncu Proje Listesi

        [HttpPost("performer-projeler")]
        public async Task<IActionResult> PerformerProjeler(PerformerIdDTO id)
        {
            //var path = Path.Combine(_env.ContentRootPath, @"MyTest.txt");

            //using (FileStream fs = System.IO.File.Create(path))
            //{
            //    byte[] content = new UTF8Encoding(true).GetBytes("Hello World");

            //    fs.Write(content, 0, content.Length);
            //}
            int dil = await _dilService.GecerliDil();
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeService.PerformerProjeListesi(id, jwtToken, dil));
        }

        #endregion

        #region Menajer Proje İşlemleri

        //Menajerin odi yüklemesi için projeodi bilgilerini getirir
        [HttpPost("menajer-proje-odi-detay-getir")]
        public async Task<IActionResult> MenajerProjeOdiDetayGetir(MenajerIslemInputDTO input)
        {
            int dil = await _dilService.GecerliDil();
            string jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return Ok(await _projeService.MenajerProjeOdiDetayGetir(input, jwtToken, dil));
        }

        #endregion
    }
}