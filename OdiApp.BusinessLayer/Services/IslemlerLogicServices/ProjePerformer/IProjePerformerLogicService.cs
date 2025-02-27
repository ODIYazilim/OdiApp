using OdiApp.DTOs.IslemlerDTOs.ProjePerformer;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.ProjePerformer
{
    public interface IProjePerformerLogicService
    {
        Task<OdiResponse<List<PerformerDisplayInfoDTO>>> ProjeOnerilenPerformerList(string jwtToken);
    }
}
