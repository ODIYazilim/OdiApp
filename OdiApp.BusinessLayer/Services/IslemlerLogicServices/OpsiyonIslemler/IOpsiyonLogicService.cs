using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OpsiyonIslemler
{
    public interface IOpsiyonLogicService
    {
        Task<OdiResponse<List<OpsiyonListesiOutputDTO>>> OpsiyonListesineEkle(List<OpsiyonListesiCreateDTO> opsList, OdiUser user);
        Task<OdiResponse<List<OpsiyonListesiOutputDTO>>> OpsiyonListesiGetir(ProjeIdDTO projeId, string jwtToken);
        Task<OdiResponse<List<OpsiyonListesiOutputDTO>>> MenajerOpsiyonListesiGetir(MenajerOpsiyonListesiInputDTO input);

        Task<OdiResponse<bool>> MenajerInceledi(OpsiyonIdDTO opsId);
        Task<OdiResponse<bool>> OpsiyonuPerformeraIlet(OpsiyonIdDTO opsId);
        Task<OdiResponse<bool>> PerformerInceledi(OpsiyonIdDTO opsId);
        Task<OdiResponse<bool>> OpsiyonListesindenCikar(OpsiyonListesiIdDTO opsiyonListesiId);
        //
        Task<OdiResponse<OpsiyonOutputDTO>> OpsiyonGetir(OpsiyonIdDTO opsId);
        Task<OdiResponse<List<OpsiyonOutputDTO>>> YeniOpsiyon(List<OpsiyonCreateDTO> opsiyonCreateDTOList, OdiUser user);
        Task<OdiResponse<OpsiyonOutputDTO>> PerformeraYonlendir(OpsiyonIdDTO opsId);
        Task<OdiResponse<OpsiyonOutputDTO>> GeriCevir(OpsiyonGeriCevirDTO dto);
        Task<OdiResponse<bool>> MenajerOnayi(OpsiyonIdDTO opsId);
        Task<OdiResponse<OpsiyonOutputDTO>> Yanitla(OpsiyonYanitlaDTO dto, OdiUser user, string jwtToken);
    }
}