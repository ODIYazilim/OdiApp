using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs.Interfaces;

public interface IPerformerCVDataService
{
    Task<PerformerCVOutputDTO> PerformerCVGetirByUserId(string userId);
    Task<PerformerCV> YeniPerformerCV(PerformerCV cv);
    Task<PerformerCV> PerformerCVGuncelle(PerformerCV cv);
    Task<PerformerCV> PerformerCVGetirById(string performerCVId);
    Task<bool> PerformerCVVarMi(string PerformerId);

    Task<CVDeneyim> YeniCVDeneyim(CVDeneyim deneyim);
    Task<bool> CVDeneyimSil(int CVDeneyimId);


}