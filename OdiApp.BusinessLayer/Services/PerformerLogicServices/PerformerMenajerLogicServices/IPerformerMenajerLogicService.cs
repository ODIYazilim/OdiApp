using OdiApp.DTOs.PerformerDTOs.MenajerAbonelikDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerMenajerLogicServices;

public interface IPerformerMenajerLogicService
{
    Task<OdiResponse<MenajerAbonelikKalanKullanimOutputDTO>> MenajerAbonelikKalanKullanimSayilariGetir(YetenekTemsilcisiIdDTO model, string jwtToken);
    Task<OdiResponse<bool>> MenajerAbonelikPerformerPremiumDagitma(PerformerPremiumDagitmaInputDTO model, string jwtToken, OdiUser user);

    #region Performer Menajer Sözleşme

    Task<OdiResponse<PerformerMenajerSozlesmeOutputDTO>> PerformerMenajerSozlesmeEkle(PerformerMenajerSozlesmeCreateDTO model, OdiUser user, string jwt);
    Task<OdiResponse<PerformerMenajerSozlesmeOutputDTO>> PerformerMenajerSozlesmeGuncelle(PerformerMenajerSozlesmeUpdateDTO model, OdiUser user, string jwt);
    //Task<OdiResponse<PerformerMenajerSozlesmeOutputDTO>> PerformerMenajerSozlesmeGetir(PerformerMenajerSozlesmeGetirInputDTO model, string jwt);
    //Task<OdiResponse<MenajerPerformerSozlesmeGetirOutputDTO>> MenajerPerformerSozlesmeGetir(MenajerPerformerSozlesmeGetirInputDTO model);
    Task<OdiResponse<List<MenajerPerformerSozlesmeGetirOutputDTO>>> MenajerPerformerSozlesmeListesiGetir(MenajerPerformerSozlesmeGetirInputDTO model);
    //Task<OdiResponse<List<PerformerMenajerSozlesmeOutputDTO>>> PerformerMenajerSozlesmeListesiGetir(MenajerIdDTO model, string jwt);

    #endregion
}