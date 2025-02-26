using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerFiltre;

public interface IPerformerFiltreLogicService
{
    Task<OdiResponse<List<PerformerDisplayInfoDTO>>> ProjeOnerilenOyuncular();
    Task<OdiResponse<List<PerformerDisplayInfoDTO>>> PerformerDetayListesi(List<PerformerIdDTO> idList);
}