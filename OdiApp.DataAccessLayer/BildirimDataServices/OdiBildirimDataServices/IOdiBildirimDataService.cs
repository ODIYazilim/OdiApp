using OdiApp.EntityLayer.BildirimModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.OdiBildirimDataServices
{
    public interface IOdiBildirimDataService
    {
        Task<OdiBildirim> YeniOdiBildirim(OdiBildirim bildirim);
        Task<OdiBildirimHerkes> YeniOdiBildirimHerkes(OdiBildirimHerkes bildirim);
        Task<OdiBildirim> OdiBildirimGetir(string odiBildirmId);
        Task<OdiBildirimHerkes> OdiBildirimHerkesGetir(string odiBildirimHerkesId);
        Task<List<OdiBildirim>> OdiBildirimListesi(string kullaniciId, DateTime bildirimTarihi);
        Task<List<OdiBildirimHerkes>> OdiBildirimHerkesListesi(DateTime bildirimTarihi);
        Task<OdiBildirim> OdiBildirimGuncelle(OdiBildirim bildirim);
        Task<bool> OdiBildirimSil(OdiBildirim bildirim);
        Task<bool> OdiBildirimHepsiOkundu(string kullaniciId);
    }
}
