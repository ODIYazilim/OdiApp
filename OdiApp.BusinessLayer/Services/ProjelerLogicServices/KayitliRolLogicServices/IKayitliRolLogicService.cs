using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.KayitliRolLogicServices
{
    public interface IKayitliRolLogicService
    {
        Task<OdiResponse<bool>> RolKaydet(ProjeRolIdDTO projeRolIdDTO, OdiUser user);
    }
}