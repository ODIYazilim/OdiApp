using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerFiltre;

public interface IPerformerFiltreDataService
{
    Task<List<PerformerDisplayInfoDTO>> PerformerDisplayInfoList();
    Task<List<PerformerDisplayInfoDTO>> PerformerDetayListesi(List<string> idList);
}
