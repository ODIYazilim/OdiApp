using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.KullaniciBasicDataServices
{
    public interface IKullaniciBasicDataService
    {
        Task<KullaniciBasic> KullaniciEkle(KullaniciBasic kullaniciBasic);
        Task<List<KullaniciBasic>> KullaniciListesiGetir(List<string> kullaniciId);
        Task<List<KullaniciBasic>> KullaniciListesiGetirByKayitGrubu(string kayitGrubu);
        Task<KullaniciBasic> KullaniciGetir(string kullaniciId);
        Task<KullaniciBasic> KullaniciGuncelle(KullaniciBasic kullaniciBasic);
    }
}