using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikLogicServices;

public interface IPerformerAbonelikLogicService
{
    Task<OdiResponse<PerformerAbonelik>> PerformerAbonelikBitisTarihiGuncelle(PerformerAbonelikTarihDTO model, OdiUser user);
    Task<OdiResponse<string>> PerformerAbonelikOlustur(PerformerAbonelikCreateDTO model, OdiUser user);
    Task<OdiResponse<AboneligiSonlandirOutputDTO>> PerformerAbonelikBitir(AboneligiSonlandirInputDTO model, OdiUser user);
    Task<OdiResponse<PerformerAbonelikOzetiGetirOutputDTO>> PerformerAbonelikOzetiGetir(KullaniciIdDTO model, string jwtToken);
    Task<OdiResponse<bool>> PerformerAbonelikYenilemeyiKapat(AbonelikIdDTO model, OdiUser user);
}