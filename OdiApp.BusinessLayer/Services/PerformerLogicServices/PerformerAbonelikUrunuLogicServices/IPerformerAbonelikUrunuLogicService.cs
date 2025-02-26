using OdiApp.DTOs.PerformerDTOs.PerformerAbonelikUrunDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikUrunuLogicServices;

public interface IPerformerAbonelikUrunuLogicService
{
    Task<OdiResponse<string>> YeniPerformerAbonelikUrunu(PerformerAbonelikUrunuCreateDTO model, OdiUser user);
    Task<OdiResponse<bool>> PerformerAbonelikUrunuGuncelle(PerformerAbonelikUrunuUpdateDTO model, OdiUser user);
    Task<OdiResponse<bool>> PerformerAbonelikUrunDurumGuncelle(PerformerAbonelikUrunuIdDTO model, OdiUser user);
    Task<OdiResponse<List<PerformerAbonelikUrunuOutputDTO>>> PerformerAbonelikUrunuListele();
    Task<OdiResponse<string>> PerformerAbonelikUrunuIsimGetir(AbonelikUrunuIdDTO model);
}