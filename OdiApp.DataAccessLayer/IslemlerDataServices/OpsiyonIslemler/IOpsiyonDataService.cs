using OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler;
using OdiApp.DTOs.IslemlerDTOs.PerformerIslemler;
using OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OpsiyonIslemler
{
    public interface IOpsiyonDataService
    {
        Task<List<OpsiyonListesi>> OpsiyonListesineEkle(List<OpsiyonListesi> opsList);
        Task<List<OpsiyonListesiViewDTO>> OpsiyonListesiGetir(List<string> yetkiliIdleri, string projeId);

        Task<OpsiyonListesi> OpsiyonListesiGetir(string OpsiyonListId);
        Task<List<OpsiyonListesiViewDTO>> MenajerOpsiyonListesiGetir(string menajerId, string projeId);
        Task<bool> OpsiyonListesindenCikar(OpsiyonListesi opsList);
        Task<bool> CheckOpsiyonListesi(string performerId, string projeRolId);
        Task<List<OpsiyonViewDTO>> YeniOpsiyon(List<Opsiyon> opsiyonList);
        Task<Opsiyon> OpsiyonGetir(string opsiyonId);
        Task<OpsiyonViewDTO> OpsiyonViewGetir(string OpsiyonListesiId);
        Task<OpsiyonViewDTO> OpsiyonViewGetirById(string opsiyonId);
        Task<bool> CheckOpsiyon(string projeId, string menajerId, string performerId);
        Task<Opsiyon> OpsiyonGuncelle(Opsiyon opsiyon);
        Task<OpsiyonAnketSorulari> OpsiyonAnketSorusuGetir(string SoruId);
        Task<OpsiyonAnketSorulari> OpsiyonAnketSorusuGuncelle(OpsiyonAnketSorulari soru);
        //PerfomerİSlemler için
        Task<RolOpsiyonBilgisiDTO> RolOpsiyonBilgisiGetir(string rolId);
    }
}