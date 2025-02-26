using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikDataServices;

public interface IPerformerAbonelikDataService
{
    Task<PerformerAbonelik> YeniPerformerAbonelik(PerformerAbonelik model);
    Task<PerformerAbonelik> PerformerAbonelikGuncelle(PerformerAbonelik model);
    Task<PerformerAbonelik> PerformerAbonelikGetirById(string id);
    Task<PerformerAbonelik> PerformerAbonelikGetirByPerformerId(string performerId);
    Task<PerformerAbonelikSureUzatma> YeniPerformerAbonelikSureUzatma(PerformerAbonelikSureUzatma model);
    Task<bool> PerformerAbonelikKayitKontrolu(string performerAbonelikUrunuId, string performerId);
}