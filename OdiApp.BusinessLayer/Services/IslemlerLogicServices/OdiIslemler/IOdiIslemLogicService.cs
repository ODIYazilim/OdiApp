using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiIzlemeListesiDTO;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler
{
    public interface IOdiIslemLogicService
    {

        Task<OdiResponse<List<OdiTalepOutputDTO>>> YeniOdiTalep(List<OdiTalepCreateDTO> odiTalepList, OdiUser user);
        Task<OdiResponse<List<OdiTalepOutputDTO>>> YapimOdiTalepListesi(KullaniciIdDTO kid);
        Task<OdiResponse<List<OdiTalepOutputDTO>>> MenajerOdiTalepListesi(KullaniciIdDTO kid);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerGoruldu(OdiTalepIdDTO talepId, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerRed(OdiTalepRedInputDTO red, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepPerformeraIlet(OdiTalepPerformeraIletInput ilet, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepPerformerGoruldu(OdiTalepIdDTO talepId, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepPerformerRed(OdiTalepRedInputDTO red, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerPerformerRedOnayi(OdiTalepIdDTO talepId, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerRedIptal(OdiTalepIdDTO talepId, OdiUser user);
        Task<OdiResponse<OdiTalepOutputDTO>> OdiTalepMenajerPerformerRedIptal(OdiTalepIdDTO talepId, OdiUser user);
        //
        Task<OdiResponse<List<IzlemeListesiItem>>> MenajerIzlemeListesi(MenajerIdDTO menajerId);
        Task<OdiResponse<List<IzlemeListesiItem>>> PerformerOdiIzlemeListesi(PerformerIdDTO model);
        Task<OdiResponse<List<IzlemeListesiItem>>> YapımIzlemeListesi(ProjeIdDTO projeId, string jwtToken);
    }
}