using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.YetenekData;

public interface IYetenekDataService
{
    Task<YetenekTipi> YeniYetenekTipi(YetenekTipi yetenekTipi);
    Task<Yetenek> YeniYetenek(Yetenek yetenek);

    Task<List<YetenekTipiDTO>> YetenekListesi(int dilId);
}