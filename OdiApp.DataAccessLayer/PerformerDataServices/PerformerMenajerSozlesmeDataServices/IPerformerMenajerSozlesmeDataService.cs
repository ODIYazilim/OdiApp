using OdiApp.EntityLayer.PerformerModels.PerformerMenajerModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerMenajerSozlesmeDataServices;

public interface IPerformerMenajerSozlesmeDataService
{
    Task<PerformerMenajerSozlesme> YeniPerformerMenajerSozlesme(PerformerMenajerSozlesme model);
    Task<PerformerMenajerSozlesme> PerformerMenajerSozlesmeGuncelle(PerformerMenajerSozlesme model);
    Task<List<PerformerMenajerSozlesme>> PerformerMenajerSozlesmeListesiGetir(string menajerId);
    Task<PerformerMenajerSozlesme> PerformerMenajerSozlesmeGetirById(string id);
    Task<PerformerMenajerSozlesme> PerformerMenajerSozlesmeGetirByMenajerPerformerId(string performerId, string menajerId);
    Task<List<PerformerMenajerSozlesme>> PerformerMenajerSozlesmeListesiGetirByMenajerPerformerId(string performerId, string menajerId);
}