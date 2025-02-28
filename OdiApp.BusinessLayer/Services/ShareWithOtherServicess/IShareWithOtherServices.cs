using OdiApp.DTOs.ProjelerDTOs.ShareWithOtherServices;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.BusinessLayer.Services.ShareWithOtherServicess
{
    public interface IShareWithOtherServices
    {
        Task<OdiResponse<List<ProjeYetkiliBilgisiDTO>>> ProjeYetkilileriGetir(ProjeIdDTO projeId);
    }
}