using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikUrunuOdemePlaniDataServices
{
    public interface IAbonelikUrunuOdemePlaniDataService
    {
        Task<AbonelikUrunuOdemePlani> YeniAbonelikUrunuOdemePlani(AbonelikUrunuOdemePlani model);
        Task<AbonelikUrunuOdemePlani> AbonelikUrunuOdemePlaniGuncelle(AbonelikUrunuOdemePlani model);
        Task<List<AbonelikUrunuOdemePlani>> AbonelikUrunuOdemePlaniListesiGetir();
        Task<AbonelikUrunuOdemePlani> AbonelikUrunuOdemePlaniGetirByReferenceCode(string referenceCode);
    }
}