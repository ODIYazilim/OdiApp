using OdiApp.DTOs.IslemlerDTOs.OdiListeler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiListeler
{
    public interface IOdiListeLogicService
    {
        Task<OdiResponse<OdiListeIdDTO>> YeniOdiListe(OdiListeCreateDTO odiListe, OdiUser user);
        Task<OdiResponse<bool>> YeniOdiListeDetay(List<OdiListeDetayCreateDTO> detayList, OdiUser user);
        Task<OdiResponse<List<OdiListeAdlariOutputDTO>>> OdiListeleriGetir(KullaniciIdDTO kullaniciId);
        Task<OdiResponse<OdiListeOutputDTO>> OdiListeDetayGetir(OdiListeIdDTO listeId);
        Task<OdiResponse<bool>> OdiListeSil(OdiListeIdDTO listeId);
        Task<OdiResponse<bool>> OdiListeDetaySil(List<OdiListeDetayIdDTO> detayIdList);
    }
}
