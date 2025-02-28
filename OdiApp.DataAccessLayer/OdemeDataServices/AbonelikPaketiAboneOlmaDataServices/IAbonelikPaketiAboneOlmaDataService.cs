using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikPaketiAboneOlmaDataServices
{
    public interface IAbonelikPaketiAboneOlmaDataService
    {
        Task<AbonelikPaketiAboneOlma> YeniAbonelikPaketiAboneOlma(AbonelikPaketiAboneOlma model);
        Task<AbonelikPaketiAboneOlma> AbonelikPaketiAboneOlmaGuncelle(AbonelikPaketiAboneOlma model);
        Task<List<AbonelikPaketiAboneOlma>> AbonelikPaketiAboneOlmaListesiGetir();
        Task<List<AbonelikPaketiAboneOlma>> AbonelikPaketiAboneOlmaListesiGetirByKullaniciId(string kullaniciId);
        Task<AbonelikPaketiAboneOlma> AbonelikPaketiAboneOlmaGetirByAboneReferenceCode(string refCode);
    }
}
