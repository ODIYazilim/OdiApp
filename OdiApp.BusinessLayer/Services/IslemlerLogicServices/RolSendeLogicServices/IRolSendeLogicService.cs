using OdiApp.DTOs.IslemlerDTOs.RolSendeDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.RolSendeLogicServices
{
    public interface IRolSendeLogicService
    {
        Task<OdiResponse<NoContent>> YeniRolSende(RolSendeCreateDTO model, OdiUser user, string jwtToken);
    }
}