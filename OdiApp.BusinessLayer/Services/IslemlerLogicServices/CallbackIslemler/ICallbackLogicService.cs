using OdiApp.DTOs.IslemlerDTOs.CallbackIslemler;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.CallbackIslemler
{
    public interface ICallbackLogicService
    {
        Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> YeniCallbackAyarlarıTakvimOlustur(CallbackAyarlarıTakvimCreateDTO input, OdiUser user);
        Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> CallbackTakvimiGetir(CallbackTakvimiGetirInput input);
        Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> CallbackSaatleriKilitle(CallbackSaatKilitleInputDTO saatIdListesi);
        Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> CallbackSaatleriKilidiAc(CallbackSaatKilitleInputDTO saatIdListesi);
        Task<OdiResponse<CallbackTarihEklemeAyarlariOutput>> CallbackTarihEklemeAyarlariGetir(ProjeIdDTO id);
        Task<OdiResponse<List<CallbackTakvimSaatleriOutputDTO>>> YeniCalllbackTarihSaatleriEkle(CallbackTarihSaatCreateDTO yeniCallbackTarihSaat, OdiUser user);
        Task<OdiResponse<CallbackSaatOutputDTO>> CallbackSaatiNotGuncelle(CallbackSaatNotuInputDTO input, OdiUser user);
        Task<OdiResponse<List<CallbackGonderilecekPerformerOutput>>> CallbackGonderilebilecekPerformerListesi(ProjeIdDTO projeId);
        Task<OdiResponse<List<CallbackOutputDTO>>> CallbackGonder(List<CallbackCreateDTO> callbackList, OdiUser user);
        Task<OdiResponse<List<CallbackOutputDTO>>> YapimCallbackListesi(ProjeIdDTO projeId);
        Task<OdiResponse<List<CallbackOutputDTO>>> MenajerCallbackListesi(KullaniciIdDTO menajerId);
        Task<OdiResponse<CallbackOutputDTO>> CallbackOnayla(CallbackOnaylaDTO onay, OdiUser user);
        Task<OdiResponse<CallbackOutputDTO>> CallbackRed(CallbackRedDTO red, OdiUser user);

        Task<OdiResponse<CallbackOutputDTO>> CallbackMenajerGordu(CallbackIdDTO callbackId);

        Task<OdiResponse<CallbackOutputDTO>> CallbackPerformerGordu(CallbackIdDTO callbackId);

        Task<OdiResponse<CallbackPerformerDetaylariOutputDTO>> CallbackPerformerDetaylari(CallbackPerformerDetaylariInputDTO input);
        Task DeleteCallbackAyarlariTakvim();
    }
}