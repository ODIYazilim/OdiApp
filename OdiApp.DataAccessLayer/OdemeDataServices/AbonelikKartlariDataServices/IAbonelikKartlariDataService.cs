using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikKartlariDataServices
{
    public interface IAbonelikKartlariDataService
    {
        Task<AbonelikKartlari> YeniAbonelikKartlari(AbonelikKartlari model);
        Task<AbonelikKartlari> AbonelikKartlariGuncelle(AbonelikKartlari model);
        Task<List<AbonelikKartlari>> AbonelikKartListesiGetir();
        Task<AbonelikKartlari> AbonelikKartiGetir(string id);
    }
}