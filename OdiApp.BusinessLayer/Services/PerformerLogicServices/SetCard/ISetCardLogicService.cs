using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.PerformerDTOs.SetCard;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.SetCard;

public interface ISetCardLogicService
{
    Task<OdiResponse<List<SetCardOutputDTO>>> SetCardGetir(List<KullaniciIdDTO> idList, int dilId);
}