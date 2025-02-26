using OdiApp.EntityLayer.Base;
using OdiApp.DTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekTemsilcisiModels;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;

public interface IYetenekTemsilcisiDataService
{
    Task<PagedData<string>> PerformerListesi(PerformerListesiInputDTO model, List<string>? aktifKullaniciIdList);
    Task<PerformerListesiSayilariOutputDTO> PerformerListesiSayilari(string menajerId, List<string>? aktifKullaniciIdList);
    Task<PerformerYetenekTemsilcisi> YeniPerformerYetenekTemsilcisi(PerformerYetenekTemsilcisi model);
    Task<PerformerMenajerListItemOutputDTO> PerformerMenajerGetir(string performerId);
    Task<List<PerformerMenajerListItemOutputDTO>> PerformerMenajerListesiGetir(List<string> performerIdList);
    //Menajerlere ait performer id listesini getirir.
    Task<List<MenajerPerformerListItemOutputDTO>> MenajerPerformerListesiGetir(List<string> manajerIdList);
}