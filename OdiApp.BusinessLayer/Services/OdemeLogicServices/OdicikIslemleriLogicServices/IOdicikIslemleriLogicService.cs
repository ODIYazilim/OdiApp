using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.OdemeDTOs.OdicikİslemleriDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.OdemeLogicServices.OdicikIslemleriLogicServices
{
    public interface IOdicikIslemleriLogicService
    {
        Task<OdiResponse<bool>> OdicikEkleme(OdicikEklemeDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<bool>> OdicikHarcama(OdicikHarcamaDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<List<OdicikIslemleriOutputDTO>>> OdicikHareketleri(KullaniciIdDTO model);
        Task<OdiResponse<OdicikBakiyeOutputDTO>> OdicikBakiye(KullaniciIdDTO model);
    }
}