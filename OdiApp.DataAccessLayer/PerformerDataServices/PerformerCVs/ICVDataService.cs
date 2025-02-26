using OdiApp.EntityLayer.Base;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVEgitim;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.CVYetenek;
using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;

public interface ICVDataService
{
    Task<List<CVDataBasicDTO>> CVDataVerileriGetir();

    Task<PagedData<string>> FiltrelenenPerformerlarGetir(FiltrelenenPerformerlarInputDTO model, ProjeOutputDTO? proje);

    Task<CV> CVGetir(string performerId);
    Task<bool> CVVarmi(string performerId);
    Task<CV> YeniCV(CV cv);
    Task<CV> CVGuncelle(CV cv);

    #region CV Deneyim
    Task<CVDeneyim> YeniCVDeneyim(CVDeneyim deneyim);
    Task<bool> CVDeneyimSil(string CVDeneyimId);

    #endregion

    #region CV Yetenek
    Task<List<CVYetenekOutputDTO>> CVYetenekListesi(string cvId, int dilId);
    Task<CVYetenek> YeniCVYetenek(CVYetenek yetenek);
    Task<bool> CVYetenekSil(string cvYetenekId);
    Task<CVYetenekVideo> YeniCVYetenekVideosu(CVYetenekVideo cvYetenekVideo);
    Task<bool> CVYetenekVideosuSil(string cvYetenekVideosuId);
    Task<bool> CheckCVYetenekVideosu(string cvYetenekId);
    #endregion

    #region CV Egitim
    Task<CVEgitim> YeniCVEgitim(CVEgitim cv);
    Task<bool> CVEgitimSil(string cvEgitimId);
    Task<List<CVEgitimOutputDTO>> CVEgitimListesi(string cvId);


    #endregion

    #region ProfilVideoları

    Task<ProfilVideo> YeniProfilVideosu(ProfilVideo video);
    Task<ProfilVideo> ProfilVideosuGuncelle(ProfilVideo video);
    Task<ProfilVideo> ProfilVideoGetir(string profilVideoId);
    Task<bool> ProfilVideosuSil(string profilVideoId);
    Task<bool> ProfilVideosuSil(ProfilVideo model);
    Task<List<ProfilVideoOutputDTO>> ProfilVideolariListesi(string performerId);
    Task<List<ProfilVideoTipiOutputDTO>> VideoTipiListesi(int dilId);

    #endregion
}