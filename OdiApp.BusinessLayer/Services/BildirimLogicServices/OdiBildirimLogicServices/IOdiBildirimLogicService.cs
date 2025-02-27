using OdiApp.DTOs.BildirimDTOs.OdiBildirimDTOS;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.OdiBildirimLogicServices
{
    public interface IOdiBildirimLogicService
    {
        Task<OdiResponse<OdiBildirimOutputDTO>> YeniOdiBildirim(OdiBildirimCreateDTO bildirim, OdiUser user);
        Task<OdiResponse<bool>> YeniTopluBildirim(OdiTopluBildirimCreateDTO bildirim, OdiUser user);
        Task<OdiResponse<OdiBildirimHerkesOutputDTO>> YeniOdiBildirimHerkes(OdiBildirimHerkesCreateDTO bildirim, OdiUser user);
        Task<OdiResponse<List<OdiBildirimTumOutputDTO>>> OdiBildirimListesi(OdiBildirimListInputDTO bildirimListInputDTO);
        Task<OdiResponse<bool>> OdiBildirimOkundu(string OdiBildirimId);
        Task<OdiResponse<bool>> OdiBildirimHepsiOkundu(KullaniciIdDTO idDTO);
        Task<OdiResponse<bool>> OdiBildirimSil(string OdiBildirimId);
    }
}