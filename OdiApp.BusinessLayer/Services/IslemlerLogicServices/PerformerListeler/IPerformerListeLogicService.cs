using OdiApp.DTOs.IslemlerDTOs.PerformerListeler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerListeler
{
    public interface IPerformerListeLogicService
    {
        Task<OdiResponse<PerformerListeListesiOutputDTO>> PerformerListeListesi(KullaniciIdDTO kullaniciIdDTO);
        Task<OdiResponse<PerformerListeWithDetayOutputDTO>> PerformerListeWithPerformerDetay(PerformerListeIdDTO performerListeId, string jwt);
        Task<OdiResponse<PerformerListeOutputDTO>> YeniPerformerListe(PerformerListeCreateDTO performerListe, OdiUser user);
        Task<OdiResponse<PerformerListeOutputDTO>> PerformerListeGuncelle(PerformerListeUpdateDTO performerListe, OdiUser user);
        Task<OdiResponse<NoContent>> PerformerListeSil(PerformerListeIdDTO performerListeId);
        Task<OdiResponse<PerformerListeOutputDTO>> YeniPerformerListeDetay(List<PerformerListeDetayCreateDTO> performerListeDetayList, OdiUser user);
        Task<OdiResponse<PerformerListeOutputDTO>> PerformerListeDetaySil(List<PerformerListeDetayIdDTO> performerListDetayIdList);
    }
}