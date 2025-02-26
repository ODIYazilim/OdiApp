using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikUrunuDataServices;

public interface IPerformerAbonelikUrunuDataService
{
    Task<PerformerAbonelikUrunu> YeniPerformerAbonelikUrunu(PerformerAbonelikUrunu model);
    Task<PerformerAbonelikUrunu> PerformerAbonelikUrunuGuncelle(PerformerAbonelikUrunu model);
    Task<PerformerAbonelikUrunu> PerformerAbonelikUrunuGetir(string id);
    Task<PerformerAbonelikUrunu> PerformerAbonelikUrunuGetirByPeriod(int period);
    Task<List<PerformerAbonelikUrunu>> PerformerAbonelikUrunListesiGetir(bool onlyAktif = true);
}
