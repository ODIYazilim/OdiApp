using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.TelefonKoduDataServices
{
    public interface ITelefonKoduService
    {
        Task<List<TelefonKodu>> AktifTelefonKodlariListesi();
        Task<List<TelefonKodu>> TelefonKoduListesi();
        Task<TelefonKodu> TelefonKoduGetir(int id);
        Task<TelefonKodu> YeniTelefonKodu(TelefonKodu telefonKodu);
        Task<TelefonKodu> TelefonKoduGuncelle(TelefonKodu telefonKodu);
        Task<bool> TelefonKoduSil(TelefonKodu telefonKodu);
    }
}
