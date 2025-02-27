using OdiApp.EntityLayer.IslemlerModels.PerformerListeler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.PerformerListeler
{
    public interface IPerformerListeDataService
    {
        Task<List<PerformerListe>> PerformerListeListesi(string kullaniciId);
        Task<PerformerListe> PerformerListeGetirWithDetay(string performerListId);
        Task<PerformerListe> PerformerListeGetir(string performerListId);
        Task<PerformerListe> YeniPerformerListe(PerformerListe performerListe);
        Task<PerformerListe> PerformerListeGuncelle(PerformerListe performerListe);
        Task<bool> PerformerListeSil(string performerListeId);
        Task<List<PerformerListeDetay>> YeniPerformerListeDetay(List<PerformerListeDetay> performerListeDetayList);
        Task<bool> PerformerListeDetaySil(List<PerformerListeDetay> performerListeDetayList);
        Task<List<PerformerListeDetay>> PerformerListeDetayGetir(List<string> performerListeDetayIdList);
    }
}