using OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.CVFormAlanlariDataServices;

public interface ICVFormAlanlariDataService
{
    Task<List<CVFormAlanlariDTO>> CVFormAlanlariGetir(List<string> kayitTuruList, int dilId);
    Task<List<CVFormAlani>> CVFormAlanlariListesiGetir(List<string> kayitTuruKodlari, List<string> alanKodlari);
    Task<List<CVFormAlani>> CVFormAlanlariListesiGetirByAlanKodlari(List<string> alanKodlari);
    Task<List<CVFormAlani>> TumCVFormAlanlariListesiGetir();
}