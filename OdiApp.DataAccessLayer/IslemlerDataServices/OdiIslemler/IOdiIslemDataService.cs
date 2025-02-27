using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler
{
    public interface IOdiIslemDataService
    {
        Task<List<OdiTalep>> YeniOdiTalep(List<OdiTalep> odiTalepList);

        Task<OdiTalep> OdiTalepGuncelle(OdiTalep odiTalep);

        Task<OdiTalep> OdiTalepGetirById(string odiTalepId);
        Task<List<OdiTalep>> OdiTalepListesiGetirByProjePerformer(string performerId, string projeId);
        Task<OdiTalepOutputDTO> OdiTalepGetir(string odiTalepId);
        Task<bool> CheckOdiTalep(string performerId, string rolId);
        Task<List<OdiTalepOutputDTO>> OdiTalepListesiGetirByGonderen(string gonderenId);
        Task<List<OdiTalepOutputDTO>> OdiTalepListesiGetirByMenajer(string menajerId);
        Task<List<OdiTalepPerformerIslemOutputDTO>> OdiTalepListesiGetirByPerformer(string performerId);
        Task<List<OdiTalepOutputDTO>> OdiTalepListesiGetirByGonderen(string gonderenId, int number);//son x kadar odi yi getirir

        //
        Task<List<(OdiTalepOutputDTO, PerformerOdi)>> MenajerIzlemeListesi(string menajerId);
        Task<List<(OdiTalepOutputDTO, PerformerOdi)>> PerformerIzlemeListesi(string performerId);
        Task<List<(OdiTalepOutputDTO, PerformerOdi)>> YapimIzlemeListesi(List<string> yetkililer);
    }
}
