using OdiApp.DataAccessLayer.PerformerDataServices.PerformerFiltre;
using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerFiltre;

public class PerformerFiltreLogicService : IPerformerFiltreLogicService
{
    private readonly IPerformerFiltreDataService _performerFiltreDataService;

    public PerformerFiltreLogicService(IPerformerFiltreDataService performerFiltreDataService)
    {
        _performerFiltreDataService = performerFiltreDataService;
    }

    public async Task<OdiResponse<List<PerformerDisplayInfoDTO>>> ProjeOnerilenOyuncular()
    {
        List<PerformerDisplayInfoDTO> list = await _performerFiltreDataService.PerformerDisplayInfoList();
        return OdiResponse<List<PerformerDisplayInfoDTO>>.Success("Önerilen Oyunucular Getirildi", list, 200);
    }

    public async Task<OdiResponse<List<PerformerDisplayInfoDTO>>> PerformerDetayListesi(List<PerformerIdDTO> idList)
    {
        List<PerformerDisplayInfoDTO> list = await _performerFiltreDataService.PerformerDetayListesi(idList.Select(s => s.PerformerId).ToList());
        return OdiResponse<List<PerformerDisplayInfoDTO>>.Success("Performer Detayları Getirildi", list, 200);
    }
}