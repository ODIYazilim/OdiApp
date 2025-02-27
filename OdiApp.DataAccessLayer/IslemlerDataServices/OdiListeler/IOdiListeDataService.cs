using OdiApp.DTOs.IslemlerDTOs.OdiListeler;
using OdiApp.EntityLayer.IslemlerModels.OdiListeler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OdiListeler
{
    public interface IOdiListeDataService
    {
        Task<OdiListe> YeniOdiListe(OdiListe liste);
        Task<List<OdiListeDetay>> YeniOdiListeDetay(List<OdiListeDetay> listeDetayList);
        Task<List<OdiListeAdlariOutputDTO>> OdiListeListesi(string KullaniciId);
        Task<OdiListeOutputDTO> OdiListeGetirById(string listeId);
        Task<List<OdiListeDetay>> OdiListeDetayListesi(string odiListeId);
        Task<bool> OdiListeSil(string odiListeId);
        Task<bool> OdiListeDetaySil(List<string> odiListeDetayIdList);
        Task<bool> CheckOdiListeDetay(string odiListeId, string odiTalepId);

    }
}
