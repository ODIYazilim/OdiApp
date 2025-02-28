using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.KayitTuruDataServices
{
    public interface IKayitTuruService
    {
        #region Kayıt Grubu

        Task<List<KayitGrubu>> AktifKayitGrubuListesi(int dilId);
        Task<List<KayitGrubu>> KayitGrubuListesi(int dilId);
        Task<KayitGrubu> KayitGrubuGetir(int id);
        Task<KayitGrubu> YeniKayitGrubu(KayitGrubu kayitGrubu);
        Task<KayitGrubu> KayitGrubuGuncelle(KayitGrubu kayitGrubu);
        Task<bool> KayitGrubuSil(int kayitGrubuId);

        #endregion

        #region Kayıt Türü

        Task<List<KayitTuru>> AktifKayitTuruListesi(int dilId, string grupKodu);
        Task<List<KayitTuru>> KayitTuruListesi(int dilId, string grupKodu);
        Task<KayitTuru> KayitTuruGetir(int id);
        Task<KayitTuru> YeniKayitTuru(KayitTuru kayitTuru);
        Task<KayitTuru> KayitTuruGuncelle(KayitTuru kayitTuru);
        Task<bool> KayitTuruSil(int kayitTuruId);

        #endregion
    }
}
