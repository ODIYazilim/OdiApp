using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.KullaniciBasicDataServices
{
    public interface IKullaniciBasicDataService
    {
        Task<KullaniciBasic> KullaniciEkle(KullaniciBasic kullaniciBasic);
        Task<KullaniciBasic> KullaniciGetir(string kullaniciId);
        Task<KullaniciBasic> KullaniciGuncelle(KullaniciBasic kullaniciBasic);
    }
}
