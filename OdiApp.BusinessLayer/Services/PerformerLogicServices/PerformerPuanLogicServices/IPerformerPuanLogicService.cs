using OdiApp.DTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerPuanDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerPuanLogicServices;

public interface IPerformerPuanLogicService
{
    Task<OdiResponse<List<PerformerPuanListOutputDTO>>> AdminOyVerenOyListesiGetir(OyverenOyListesiGetirInputDTO model);
    Task<OdiResponse<List<PerformerPuanListOutputDTO>>> AdminPerformerOyListesiGetir(PerformerOyListesiGetirInputDTO model);

    Task<OdiResponse<bool>> PerformerIcinPuanVer(PerformerIcinPuanVerInputDTO model, OdiUser user);
    Task<OdiResponse<PerformerPuanOutputDTO>> PerformerPuanGetir(PerformerIdDTO model);
    Task<OdiResponse<List<PerformerPuanOutputDTO>>> PerformerListesiPuanGetir(List<PerformerIdDTO> model);
}