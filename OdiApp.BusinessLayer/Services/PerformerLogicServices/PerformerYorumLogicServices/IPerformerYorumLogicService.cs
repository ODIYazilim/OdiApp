using OdiApp.DTOs.PerformerDTOs.PerformerYorumDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerYorumLogicServices;

public interface IPerformerYorumLogicService
{
    Task<OdiResponse<PerformerYorumOutputDTO>> PerformerYorumEkle(PerformerYorumCreateDTO model, OdiUser user);
    Task<OdiResponse<NoContent>> PerformerYorumSil(PerformerYorumIdDTO model);
    Task<OdiResponse<List<PerformerYorumOutputDTO>>> PerformerYorumListesiGetir(string performerId);
}