using OdiApp.DTOs.IslemlerDTOs.CallbackIslemler;
using OdiApp.EntityLayer.IslemlerModels.CallbackIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.CallbackIslemler
{
    public interface ICallbackDataService
    {
        Task<CallbackAyarlari> YeniCallbackAyarlari(CallbackAyarlari ayarlar);
        CallbackAyarlari CallbackAyarlariGuncelle(CallbackAyarlari ayarlar);
        Task<CallbackAyarlari> CallbackAyarlariGetir(string projeId);
        Task<List<CallbackNot>> YeniCallbackNotlari(List<CallbackNot> notlar);
        Task<List<CallbackSenaryo>> YeniCallbackSenaryolari(List<CallbackSenaryo> senaryolar);
        Task<List<CallbackSaat>> YeniCallbackTakvimi(List<CallbackSaat> takvimTarihSaatleri);
        Task<CallbackSaat> CallbackSaatiGetir(string callbackSaatId);
        Task<CallbackSaat> CallbackSaatGuncelle(CallbackSaat saat);

        Task<List<CallbackSaat>> CallbackTakvimiGetir(string projeId);

        Task<List<CallbackTakvimSaatleriOutputDTO>> CallbackTakvimSaatGetir(string projeId);

        Task<bool> CheckCallbackAyarlari(string projeId);
        Task SaveChangesAsync();
        Task DeleteCallbackAyarlariTakvim();

        Task CallbackSaatleriKilitle(List<string> saatIdleri);
        Task CallbackSaatleriKilidiAc(List<string> saatIdleri);

        Task<List<CallbackGonderilecekPerformerOutput>> CallbackGonderilecekPerformerListesi(string projeId);

        Task<List<Callback>> YeniCallback(List<Callback> callbackList);
        Task<Callback> CallbackGuncelle(Callback callback);
        Task<List<CallbackOutputDTO>> YapimCallbackListesiGetir(string projeId);
        Task<List<CallbackOutputDTO>> MenajerCallbackListesiGetir(string menajerId);

        Task<CallbackOutputDTO> CallbackOutputGetir(string callbackId);
        Task<CallbackOutputDTO> CallbackOutputGetir(string projeId, string performerId);
        Task<bool> CallbackGonderilmismi(string performerId, string projeId);
        Task<Callback> CallbackGetir(string callbackId);
        Task<Callback> CallbackGetir(string projeId, string performerId);
        Task<CallbackNot> CallbackNotGetir(string projeId, string rolId);
        Task<CallbackSenaryo> CallbackSenaryoGetir(string projeId, string rolId);
    }
}