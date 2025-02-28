using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikPaketiSatinAlmaDataServices
{
    public interface IAbonelikPaketiSatinAlmaDataService
    {
        Task<AbonelikPaketiSatinAlma> YeniAbonelikPaketiSatinAlma(AbonelikPaketiSatinAlma model);
        Task<AbonelikPaketiSatinAlma> AbonelikPaketiSatinAlmaGuncelle(AbonelikPaketiSatinAlma model);
        Task<List<AbonelikPaketiSatinAlma>> AbonelikUrunListesiGetir();
        Task<List<AbonelikPaketiSatinAlma>> AbonelikUrunListesiGetirByKullaniciId(string kullaniciId);
    }
}
