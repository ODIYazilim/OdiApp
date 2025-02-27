using OdiApp.DTOs.BildirimDTOs.Mesajlasma;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.MesajlasmaLogicServices
{
    public interface IMesajLogicService
    {
        Task<OdiResponse<MesajOutputDTO>> YeniMesaj(MesajCreateDTO mesajDTO, OdiUser user);
        Task<OdiResponse<MesajDetayOutputDTO>> YeniMesajDetay(MesajDetayCreateDTO mesajDetayDTO, OdiUser user);
        Task<OdiResponse<List<MesajOutputDTO>>> MesajListesi(KullaniciIdDTO kullaniciIdDTO);
        Task<OdiResponse<PagedData<MesajDetayOutputDTO>>> MesajDetayListesi(MesajDetayListesiInputDTO mesajDetayListesiInputDTO);
        Task<OdiResponse<bool>> MesajGoruldu(MesajDetayIdDTO mesajDetayId);
        Task<OdiResponse<bool>> MesajGorulduListe(List<MesajDetayIdDTO> mesajDetayIdListe);
        Task<OdiResponse<bool>> MesajSilme(MesajIdDTO mesajId);
        Task<OdiResponse<bool>> MesajSilmeListe(List<MesajIdDTO> mesajIdList);
        Task<OdiResponse<bool>> MesajDetaySilme(MesajDetayIdDTO mesajDetayId);
        Task<OdiResponse<bool>> MesajDetaySilmeListe(List<MesajDetayIdDTO> mesajDetayIdListe);
    }
}