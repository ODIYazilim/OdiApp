using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikUrunuDataServices
{
    public interface IAbonelikUrunuDataService
    {
        Task<AbonelikUrunu> YeniAbonelikUrunu(AbonelikUrunu model);
        Task<AbonelikUrunu> AbonelikUrunuGuncelle(AbonelikUrunu model);
        Task<List<AbonelikUrunu>> AbonelikUrunListesiGetir();
        Task<AbonelikUrunu> AbonelikUrunuGetirByReferenceCode(string referenceCode);
    }
}