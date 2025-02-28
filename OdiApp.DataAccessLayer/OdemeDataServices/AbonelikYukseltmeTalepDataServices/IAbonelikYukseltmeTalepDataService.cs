using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.AbonelikYukseltmeTalepDataServices
{
    public interface IAbonelikYukseltmeTalepDataService
    {
        Task<AbonelikYukseltmeTalep> YeniAbonelikYukseltmeTalep(AbonelikYukseltmeTalep model);
        Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGuncelle(AbonelikYukseltmeTalep model);
        Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGetirById(string id);
        Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGetirByKullaniciId(string kullaniciId);
        Task<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepGetirByKullaniciveAbonelikId(string kullaniciId, string abonelikId);
    }
}