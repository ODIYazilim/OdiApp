using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.BusinessLayer.Services.OdemeLogicServices.AbonelikUrunuLogicServices
{
    public interface IAbonelikUrunuLogicService
    {
        Task<OdiResponse<AbonelikUrunu>> OdemeYontemiAbonelikUrunuOlusturma(OdemeYontemiPerformerAbonelikUrunuCreateDTO model, OdiUser user);
        Task<OdiResponse<List<AbonelikUrunu>>> AbonelikUrunleriListeleme();
        Task<OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>> AbonelikOdemePlaniOlustur(AbonelikUrunuOdemePlaniCreateDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<bool>> OdemePlaniSil(ReferenceCodeDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<bool>> AbonelikUrunuSil(ReferenceCodeDTO model, OdiUser user);
        Task<OdiResponse<bool>> AbonelikPaketiSatinAlma(AbonelikPaketiSatinAlmaInputDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<bool>> AbonelikPaketiAbonelikBaslatma(AbonelikPaketiAbonelikBaslatmaInputDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<List<PaketAbonelikOdemeOutputDTO>>> PaketAbonelikOdemeListesi(KullaniciIdDTO model, string jwtToken);
        Task<OdiResponse<List<PaketSatinAlmaOdemeOutputDTO>>> PaketSatinAlmaOdemeListesi(KullaniciIdDTO model, string jwtToken);
        Task<OdiResponse<AbonelikKartBilgisiGetirOutputDTO>> AbonelikKartBilgisiGetir(AbonelikReferenceCodeDTO model);
        Task<OdiResponse<AboneligiSonlandirOutputDTO>> AboneligiSonlandir(AboneligiSonlandirInputDTO model, string jwtToken);
        Task<OdiResponse<AboneligiSonlandirOutputDTO>> AbonelikIptalTalebininAlinmasi(AbonelikYukseltmeTalepCreateDTO model, OdiUser user, string jwtToken);
        Task<OdiResponse<bool>> AbonelikYukseltme(AbonelikYukseltmeInputDTO model, OdiUser user, string jwtToken);
    }
}