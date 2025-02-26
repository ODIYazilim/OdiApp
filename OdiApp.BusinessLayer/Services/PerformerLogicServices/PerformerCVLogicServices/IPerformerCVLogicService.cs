using OdiApp.EntityLayer.Base;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.MenajerPerformerGuncellenenAlanlarDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVEgitim;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVYetenek;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;
using OdiApp.DTOs.PerformerDTOs.PerformerProfilAlanlariDTOs;
using OdiApp.DTOs.PerformerDTOs.SektorDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.DeneyimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.GuncellenenAlanDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;

//Yeni
public interface IPerformerCVLogicService
{
    Task<OdiResponse<PagedData<KullaniciBilgileriDTO>>> FiltrelenenPerformerlar(FiltrelenenPerformerlarInputDTO model, string jwt);
    Task<OdiResponse<FiltrelenenPerformerlarOutputDTO>> FiltrelenenPerformerlarFiltreAyarlari(FiltreAyarlariInputDTO model, string jwt, int dilId);
    Task<OdiResponse<ProjeyeGoreOnerilenOyuncularOutputDTO>> ProjeyeGoreOnerilenOyuncular(ProjeyeGoreOnerilenOyuncularInputDTO model, string jwt);

    #region Sektör İşlemleri

    Task<OdiResponse<List<SektorOutputDTO>>> SektorListesi(int dilId);

    #endregion

    #region Performer CV

    Task<OdiResponse<CVAyarlariDTO>> CVAyarlariGetir(CVAyarlariGetirInputModel model, int dilId);

    Task<OdiResponse<CVOutputDTO>> YeniPerformerCV(CVCreateDTO cvDTO, OdiUser user, string jwt, int dilId);
    Task<OdiResponse<CVOutputDTO>> PerformerCVGetir(PerformerIdDTO id, int dilId);
    Task<OdiResponse<CVOutputDTO>> PerformerCVGuncelle(CVUpdateDTO cvDTO, OdiUser user, string jwt, int dilId);

    //Task<OdiResponse<List<KullaniciBilgileriDTO>>> MenajerGuncellenenPerformerlarListesi(MenajerGuncellenenPerformerlarListesiInputDTO model, string jwt);
    Task<OdiResponse<List<MenajerPerformerGuncellenenAlaniOutputDTO>>> MenajerPerformerGuncellenenAlanlarListesi(MenajerPerformerGuncellenenAlanlarListesiInputDTO model);
    Task<OdiResponse<bool>> MenajerPerformerGuncellenenAlanlarGoruldu(MenajerPerformerGuncellenenAlaniIdDTO model, OdiUser user);
    Task<OdiResponse<bool>> GuncellenenAlanEkle(GuncelenenAlanEkleInputDTO model, OdiUser user, string jwt);

    #endregion

    #region Rol Özellik Ayarları

    Task<OdiResponse<RolOzellikAyarlariDTO>> RolOzellikAyarlariGetir(KayitTuruKodlariDTO kayitTuruKodlariDTO, int dilId, string jwt);

    #endregion

    #region CV Deneyimler

    Task<OdiResponse<List<CVDeneyimOutputDTO>>> YeniCvDeneyim(CVDeneyimCreateDTO yeniDeneyim, OdiUser user, int dilId);
    Task<OdiResponse<List<CVDeneyimOutputDTO>>> CVDeneyimSil(CVDeneyimDeleteDTO deneyimSil, int dilId);
    Task<OdiResponse<List<DeneyimDTO>>> DeneyimListesi(int dilId);
    Task<OdiResponse<List<CVDeneyimOutputDTO>>> CVDeneyimListesi(CVIdDTO cvId, int dilId);
    #endregion

    #region CV Yetenek
    Task<OdiResponse<List<YetenekTipiDTO>>> YetenekListesi(int dilId);
    Task<OdiResponse<List<CVYetenekOutputDTO>>> CVYetenekListesi(string cvId, int dilId);
    Task<OdiResponse<List<CVYetenekOutputDTO>>> YeniCVYetenek(CVYetenekCreateDTO cvYetenek, OdiUser user, int dilId);
    Task<OdiResponse<List<CVYetenekOutputDTO>>> CVYetenekSil(CVYetenekDeleteDTO cvYetenek, int dilId);
    Task<OdiResponse<CVYetenekVideoOutputDTO>> YeniCVYetenekVideo(CVYetenekVideoCreateDTO cvYetenekVideo, OdiUser user);
    Task<OdiResponse<bool>> CVYetenekVideoSil(CVYetenekVideoIdDTO cvYetenekVideoId);
    #endregion

    #region CV Egitim
    Task<OdiResponse<List<EgitimTipiDTO>>> OkullarListesi();
    Task<OdiResponse<List<CVEgitimOutputDTO>>> CVEgitimListesi(CVIdDTO cvId);
    Task<OdiResponse<List<CVEgitimOutputDTO>>> YeniCVEgitim(CVEgitimCreateDTO cvEgitim, OdiUser user);
    Task<OdiResponse<List<CVEgitimOutputDTO>>> CVEgitimSil(CVEgitimDeleteDTO cvDelete);
    #endregion

    #region Profil Video

    Task<OdiResponse<List<ProfilVideoOutputDTO>>> YeniProfilVideo(ProfilVideoCreateDTO video, OdiUser user, string jwt);
    Task<OdiResponse<List<ProfilVideoOutputDTO>>> ProfilVideosuSil(ProfilVideoDeleteDTO profilVideoId, OdiUser user, string jwt);
    Task<OdiResponse<List<ProfilVideoOutputDTO>>> ProfilVideosuTagsGuncelle(ProfilVideosuTagsUpdateDTO tagsUpdate, OdiUser user);
    Task<OdiResponse<List<ProfilVideoAlbumDTO>>> ProfilVideoListesi(PerformerIdDTO performerId, int dilId);
    Task<OdiResponse<List<TopluProfilVideoAlbumDTO>>> TopluProfilVideoListesi(List<PerformerIdDTO> performerIdList, int dilId);
    OdiResponse<List<string>> ShowreelsHashTags();
    Task<OdiResponse<List<ProfilVideoTipiOutputDTO>>> ProfilVideoTipleri(int dilId);

    #endregion

    #region Performer Profil

    Task<OdiResponse<ProfilDolulukOraniOutputDTO>> ProfilDolulukOrani(PerformerIdDTO model);

    #endregion
}