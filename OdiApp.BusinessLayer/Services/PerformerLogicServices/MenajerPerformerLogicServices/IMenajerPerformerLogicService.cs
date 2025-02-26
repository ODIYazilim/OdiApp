using OdiApp.DTOs.PerformerDTOs.MenajerPerformerDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.MenajerPerformerLogicServices;

public interface IMenajerPerformerLogicService
{
    Task<OdiResponse<MenajerPerformerNotOutputDTO>> MenajerPerformerNotEkleGuncelle(MenajerPerformerNotCreateOrUpdateDTO model, OdiUser user);
    Task<OdiResponse<MenajerPerformerNotOutputDTO>> MenajerPerformerNotGetir(MenajerPerformerNotGetirInputDTO model);
}