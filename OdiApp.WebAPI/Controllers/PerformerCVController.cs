using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.AuthAttribute;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.MenajerPerformerGuncellenenAlanlarDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVEgitim;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVYetenek;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.GuncellenenAlanDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.WebAPI.Controllers;

[Route("api/performer-cv")]
[ApiController]
[AllAuthorize]

public class PerformerCVController : ControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IGecerliDilService _dilService;
    private readonly IPerformerCVLogicService _performerCVService;

    public PerformerCVController(IMapper mapper, ISharedIdentityService sharedIdentityService, IGecerliDilService gecerliDilService, IPerformerCVLogicService performerCvService)
    {
        _identityService = sharedIdentityService;
        _dilService = gecerliDilService;
        _performerCVService = performerCvService;
    }

    //[HttpPost("cv-ayarlari-getir")]
    //public async Task<IActionResult> CVAyarlariGetir(CVAyarlariGetirInputModel model)
    //{
    //    return Ok(await _performerCVService.CVAyarlariGetir(model, await _dilService.GecerliDil()));
    //}

    [HttpPost("filtrelenen-performerlar")]
    public async Task<IActionResult> FiltrelenenPerformerlar(FiltrelenenPerformerlarInputDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.FiltrelenenPerformerlar(model, jwt));
    }

    [HttpPost("filtrelenen-performerlar-filtre-ayarlari")]
    public async Task<IActionResult> FiltrelenenPerformerlarFiltreAyarlari(FiltreAyarlariInputDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.FiltrelenenPerformerlarFiltreAyarlari(model, jwt, await _dilService.GecerliDil()));
    }

    [HttpPost("projeye-gore-onerilen-oyuncular")]
    public async Task<IActionResult> ProjeyeGoreOnerilenOyuncular(ProjeyeGoreOnerilenOyuncularInputDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.ProjeyeGoreOnerilenOyuncular(model, jwt));
    }

    #region Sektör İşlemleri

    [HttpGet("sektor-listesi")]
    public async Task<IActionResult> SektorListesi()
    {
        return Ok(await _performerCVService.SektorListesi(await _dilService.GecerliDil()));
    }

    #endregion

    #region Güncellenen Alanlar Listesi

    //[HttpPost("menajer-guncellenen-performerlar-listesi")]
    //public async Task<IActionResult> MenajerGuncellenenPerformerlarListesi(MenajerGuncellenenPerformerlarListesiInputDTO model)
    //{
    //    string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
    //    return Ok(await _performerCVService.MenajerGuncellenenPerformerlarListesi(model, jwt));
    //}

    [HttpPost("menajer-performer-guncellenen-alanlar-listesi")]
    public async Task<IActionResult> MenajerPerformerGuncellenenAlanlarListesi(MenajerPerformerGuncellenenAlanlarListesiInputDTO model)
    {
        return Ok(await _performerCVService.MenajerPerformerGuncellenenAlanlarListesi(model));
    }

    [HttpPost("menajer-performer-guncellenen-alanlar-goruldu")]
    public async Task<IActionResult> MenajerPerformerGuncellenenAlanlarGoruldu(MenajerPerformerGuncellenenAlaniIdDTO model)
    {
        return Ok(await _performerCVService.MenajerPerformerGuncellenenAlanlarGoruldu(model, _identityService.GetUser));
    }

    //Profil fotoğrafı değişikliği için kullanılıyor.
    [HttpPost("guncellenen-alan-ekle")]
    public async Task<IActionResult> GuncellenenAlanEkle(GuncelenenAlanEkleInputDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.GuncellenenAlanEkle(model, _identityService.GetUser, jwt));
    }

    #endregion

    #region CV EĞİTİM

    [HttpGet("okullar-listesi")]
    public async Task<IActionResult> OkullarListesi()
    {
        return Ok(await _performerCVService.OkullarListesi());
    }

    [HttpPost("yeni-cv-egitim")]
    public async Task<IActionResult> YeniCVEgitim(CVEgitimCreateDTO cvEgitim)
    {
        return Ok(await _performerCVService.YeniCVEgitim(cvEgitim, _identityService.GetUser));
    }
    [HttpPost("cv-egitim-listesi")]
    public async Task<IActionResult> CVEgitimListesi(CVIdDTO cvId)
    {
        return Ok(await _performerCVService.CVEgitimListesi(cvId));
    }
    [HttpPost("cv-egitim-sil")]
    public async Task<IActionResult> CVEgitimGuncelle(CVEgitimDeleteDTO deleteDto)
    {
        return Ok(await _performerCVService.CVEgitimSil(deleteDto));
    }
    #endregion

    #region CV YETENEK
    [HttpGet("yetenek-listesi")]
    public async Task<IActionResult> YetnenekListesi()
    {
        return Ok(await _performerCVService.YetenekListesi(await _dilService.GecerliDil()));
    }
    [HttpPost("cv-yetenek-listesi")]
    public async Task<IActionResult> CVYetnenekListesi(CVIdDTO cvId)
    {
        return Ok(await _performerCVService.CVYetenekListesi(cvId.ToString(), await _dilService.GecerliDil()));
    }


    [HttpPost("yeni-cv-yetenek")]
    public async Task<IActionResult> YeniCVYetenek(CVYetenekCreateDTO cvYetenek)
    {
        return Ok(await _performerCVService.YeniCVYetenek(cvYetenek, _identityService.GetUser, await _dilService.GecerliDil()));
    }
    //[HttpPost("cv-yetenek-guncelle")]
    //public async Task<IActionResult> CVYetenekGuncelle(CVYetenekDTO cvYetenek)
    //{
    //    return Ok(await _performerCVService.CVYetenekGuncelle(cvYetenek, _identityService.GetUser));
    //}
    [HttpPost("cv-yetenek-sil")]
    public async Task<IActionResult> CVYetenekGuncelle(CVYetenekDeleteDTO cvYetenekdelete)
    {
        return Ok(await _performerCVService.CVYetenekSil(cvYetenekdelete, await _dilService.GecerliDil()));
    }

    [HttpPost("yeni-cv-yetenek-video")]
    public async Task<IActionResult> YeniCVYetenekVideo(CVYetenekVideoCreateDTO cvYetenekVideo)
    {
        return Ok(await _performerCVService.YeniCVYetenekVideo(cvYetenekVideo, _identityService.GetUser));
    }

    [HttpPost("cv-yetenek-video-sil")]
    public async Task<IActionResult> CVYetenekVideoSil(CVYetenekVideoIdDTO cvYetenekVideoId)
    {
        return Ok(await _performerCVService.CVYetenekVideoSil(cvYetenekVideoId));
    }
    #endregion

    #region CV DENEYİM
    [HttpPost("yeni-cv-Deneyim")]
    public async Task<IActionResult> YeniCVDeneyim(CVDeneyimCreateDTO cvDeneyim)
    {
        return Ok(await _performerCVService.YeniCvDeneyim(cvDeneyim, _identityService.GetUser, await _dilService.GecerliDil()));
    }
    //[HttpPost("cv-Deneyim-guncelle")]
    //public async Task<IActionResult> CVDeneyimGuncelle(CVDeneyimDTO cvDeneyim)
    //{
    //    return Ok(await _performerCVService.CVDeneyimGuncelle(cvDeneyim, _identityService.GetUser));
    //}
    [HttpPost("cv-deneyim-sil")]
    public async Task<IActionResult> CVDeneyimGuncelle(CVDeneyimDeleteDTO deneyimSil)
    {
        return Ok(await _performerCVService.CVDeneyimSil(deneyimSil, await _dilService.GecerliDil()));
    }
    [HttpGet("deneyim-listesi")]
    public async Task<IActionResult> DeneyimListesi()
    {
        return Ok(await _performerCVService.DeneyimListesi(await _dilService.GecerliDil()));
    }
    #endregion

    #region PERFORMER CV

    [HttpPost("yeni-cv")]
    public async Task<IActionResult> YeniCV(CVCreateDTO model)
    {

        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.YeniPerformerCV(model, _identityService.GetUser, jwt, await _dilService.GecerliDil()));
    }

    [HttpPost("cv-guncelle")]
    public async Task<IActionResult> CVGuncelle(CVUpdateDTO model)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.PerformerCVGuncelle(model, _identityService.GetUser, jwt, await _dilService.GecerliDil()));
    }

    [HttpPost("cv-getir")]
    public async Task<IActionResult> CVGetir(PerformerIdDTO model)
    {
        return Ok(await _performerCVService.PerformerCVGetir(model, await _dilService.GecerliDil()));
    }

    [HttpPost("cv-deneyim-listesi")]
    public async Task<IActionResult> CVDeneyimListesi(CVIdDTO cvId)
    {
        return Ok(await _performerCVService.CVDeneyimListesi(cvId, await _dilService.GecerliDil()));
    }

    #endregion

    #region Profil Videoları

    [HttpGet("showreel-hashtag-listesi")]
    public async Task<IActionResult> ShowreelHashtagListesi()
    {
        return Ok(_performerCVService.ShowreelsHashTags());
    }

    [HttpGet("profil-video-tipleri-getir")]
    public async Task<IActionResult> ProfilVideoTipleriGetir()
    {
        return Ok(await _performerCVService.ProfilVideoTipleri(await _dilService.GecerliDil()));
    }

    [HttpPost("profil-video-listesi")]
    public async Task<IActionResult> ProfilVideoListesi(PerformerIdDTO id)
    {
        return Ok(await _performerCVService.ProfilVideoListesi(id, await _dilService.GecerliDil()));
    }

    [HttpPost("toplu-profil-video-listesi")]
    public async Task<IActionResult> TopluProfilVideoListesi(List<PerformerIdDTO> idList)
    {
        return Ok(await _performerCVService.TopluProfilVideoListesi(idList, await _dilService.GecerliDil()));
    }

    [HttpPost("yeni-profil-videosu")]
    public async Task<IActionResult> YeniProfilVideosu(ProfilVideoCreateDTO video)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.YeniProfilVideo(video, _identityService.GetUser, jwt));
    }

    [HttpPost("profil-videosu-tag-guncelle")]
    public async Task<IActionResult> ProfilVideosuTagsGuncelle(ProfilVideosuTagsUpdateDTO tagsUpdate)
    {
        return Ok(await _performerCVService.ProfilVideosuTagsGuncelle(tagsUpdate, _identityService.GetUser));
    }

    [HttpPost("profil-videosu-sil")]
    public async Task<IActionResult> ProfilVideosuSil(ProfilVideoDeleteDTO deleteDTO)
    {
        string jwt = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        return Ok(await _performerCVService.ProfilVideosuSil(deleteDTO, _identityService.GetUser, jwt));
    }

    #endregion

    #region Proje Ayarlari Bilgi



    #endregion

    #region Performer Profil

    [HttpPost("profil-doluluk-orani")]
    public async Task<IActionResult> ProfilDolulukOrani(PerformerIdDTO model)
    {
        return Ok(await _performerCVService.ProfilDolulukOrani(model));
    }

    #endregion
}