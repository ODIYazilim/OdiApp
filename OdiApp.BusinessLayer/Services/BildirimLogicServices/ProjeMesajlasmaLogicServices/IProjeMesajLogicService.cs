using OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.ProjeMesajlasmaLogicServices
{
    public interface IProjeMesajLogicService
    {
        Task<OdiResponse<ProjeMesajOutputDTO>> YeniProjeMesaj(ProjeMesajCreateDTO projeProjeMesajDTO, OdiUser user, string jwtToken);
        Task<OdiResponse<ProjeMesajDetayOutputDTO>> YeniProjeMesajDetay(ProjeMesajDetayCreateDTO projeProjeMesajDetayDTO, OdiUser user, string jwtToken);
        Task<OdiResponse<List<ProjeMesajProjeDTO>>> ProjeMesajListesi(ProjeMesajListesiInputDTO requestModel);
        Task<OdiResponse<PagedData<ProjeMesajDetayOutputDTO>>> ProjeMesajDetayListesi(ProjeMesajDetayListesiInputDTO projeProjeMesajDetayListesiInputDTO);
        Task<OdiResponse<bool>> ProjeMesajGoruldu(ProjeMesajDetayIdDTO projeProjeMesajDetayId);
        Task<OdiResponse<bool>> ProjeMesajGorulduListe(List<ProjeMesajDetayIdDTO> projeProjeMesajDetayIdListe);
        Task<OdiResponse<bool>> ProjeMesajSilme(ProjeMesajIdDTO projeProjeMesajId);
        Task<OdiResponse<bool>> ProjeMesajSilmeListe(List<ProjeMesajIdDTO> projeProjeMesajIdList);
        Task<OdiResponse<bool>> ProjeMesajDetaySilme(ProjeMesajDetayIdDTO projeProjeMesajDetayId);
        Task<OdiResponse<bool>> ProjeMesajDetaySilmeListe(List<ProjeMesajDetayIdDTO> projeProjeMesajDetayIdListe);
    }
}