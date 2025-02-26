using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.DeneyimDTOs;
using OdiApp.EntityLayer.PerformerModels.Deneyimler;

namespace OdiApp.DataAccessLayer.PerformerDataServices.DeneyimDataServices;

public interface IDeneyimDataService
{
    Task<List<DeneyimDTO>> DeneyimListesi(int dilId);
    Task<List<Deneyim>> DeneyimTipiListesi(int dilId);
    Task<List<CVDeneyimOutputDTO>> CVDeneyimListesi(string cvId, int dilId);
}