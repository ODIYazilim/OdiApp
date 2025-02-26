using OdiApp.EntityLayer.Base;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;

public interface IKullaniciBasicDataService
{
    Task<KullaniciBasic> KullaniciEkle(KullaniciBasic kullaniciBasic);
    Task<KullaniciBasic> KullaniciGetir(string kullaniciId);
    Task<List<KullaniciBasic>> KullaniciListesiGetir(List<string> kullaniciId);
    Task<KullaniciBasic> KullaniciGuncelle(KullaniciBasic kullaniciBasic);
    Task<List<KullaniciBasic>> KullaniciListesiGetirByKayitGrubuKodu(string kayitGrubuKodu);
}