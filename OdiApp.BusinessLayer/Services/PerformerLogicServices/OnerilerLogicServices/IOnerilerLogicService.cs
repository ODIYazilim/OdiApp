using OdiApp.DTOs.PerformerDTOs.OnerilerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.OnerilerLogicServices;

public interface IOnerilerLogicService
{
    Task<OdiResponse<NoContent>> OneriTalepEt(OneriTalepEtDTO model, OdiUser user);
    Task<OdiResponse<NoContent>> OneriGonder(OneriGonderDTO model, OdiUser user);
    Task<OdiResponse<List<PerformerDisplayInfoDTO>>> MenajerPerformerOneriListele(MenajerPerformerOneriListeleInputDTO model);
}