using OdiApp.EntityLayer.OdemeModels.OdicikModels;

namespace OdiApp.DataAccessLayer.OdemeDataServices.OdicikIslemleriDataServices
{
    public interface IOdicikIslemleriDataService
    {
        Task<OdicikIslemleri> YeniOdicikIslemler(OdicikIslemleri model);
        Task<List<OdicikIslemleri>> OdicikHareketleriGetir(string kullaniciId);
        Task<int> OdicikBakiyeToplamiGetir(string kullaniciId);
    }
}