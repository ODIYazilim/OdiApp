using OdiApp.EntityLayer.Base;
using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekTemsilcisiLogicServices;

public interface IYetenekTemsilcisiLogicService
{
    Task<OdiResponse<PagedData<KullaniciBilgileriDTO>>> PerformerListesi(PerformerListesiInputDTO model, string jwt);
    Task<OdiResponse<PerformerListesiSayilariOutputDTO>> PerformerListesiSayilari(MenajerIdDTO model, string jwt);
    Task<OdiResponse<bool>> PerformerYetenekTemsilcisiAtama(PerformerYetenekTemsilcisiAtamaDTO model, OdiUser user);
    Task<OdiResponse<PerformerMenajerListItemOutputDTO>> PerformerMenajerGetir(KullaniciIdDTO model);
    Task<OdiResponse<List<PerformerMenajerListItemOutputDTO>>> PerformerMenajerListesiGetir(List<KullaniciIdDTO> model);
    Task<OdiResponse<List<MenajerPerformerListItemOutputDTO>>> MenajerPerformerListesiGetir(List<KullaniciIdDTO> model);
}