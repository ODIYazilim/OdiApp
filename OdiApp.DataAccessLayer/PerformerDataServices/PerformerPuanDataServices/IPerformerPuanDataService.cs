using OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerPuanDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerPuanModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerPuanDataServices;

public interface IPerformerPuanDataService
{
    Task<PerformerPuan> YeniPerformerPuan(PerformerPuan model);
    Task<PerformerPuan> PerformerPuanGuncelle(PerformerPuan model);
    Task<PerformerPuan> PerformerPuanGetir(string id);
    Task<List<PerformerPuan>> PerformerPuanListesiGetirByOyVeren(string oyverenId);
    Task<List<PerformerPuan>> PerformerPuanListesiGetirByPerformer(string performerId, string? oyverenKayitGrubu, string? oyverenKayitTuru);
    Task<PerformerPuanOutputDTO> PerformerPuanGetirByPerformerId(string performerId);
    Task<List<PerformerPuanOutputDTO>> PerformerListesiPuanGetir(List<string> performerIdList);
}