using OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels.ProjeMesajlasma;

namespace OdiApp.DataAccessLayer.BildirimDataServices.ProjeMesajlasmaDataServices
{
    public interface IProjeMesajlasmaDataService
    {
        Task<ProjeMesaj> YeniProjeMesaj(ProjeMesaj projeMesaj);
        Task<ProjeMesajDetay> YeniProjeMesajDetay(ProjeMesajDetay projeMesajDetay);
        Task<List<ProjeMesajDetay>> YeniProjeMesajDetay(List<ProjeMesajDetay> projeMesajDetay);
        Task<ProjeMesajOutputDTO> ProjeMesajGetir(string projeMesajId);
        Task<ProjeMesajOutputDTO> ProjeMesajGetir(string kullanici1Id, string kullanici2Id);
        Task<List<ProjeMesajOutputDTO>> ProjeMesajListesiWithKullaniciId(string kullaniciId);
        //Task<List<ProjeMesajOutputDTO>> ProjeMesajListesiWithFirmaKodu(string firmaKodu);
        Task<ProjeMesajDetayOutputDTO> ProjeMesajDetayGetir(string projeMesajDetayId);
        Task<PagedData<ProjeMesajDetayOutputDTO>> ProjeMesajDetayListesi(string kullanici1Id, string kullanici2Id, int pageNo, int recordsPerPage);
        Task<List<ProjeMesajDetayOutputDTO>> ProjeMesajDetayListesi(List<string> projeMesajDetayIdList);
        Task<bool> ProjeMesajGoruldu(string projeMesajDetayId);
        Task<bool> ProjeMesajGoruldu(List<string> projeMesajDetayIdList);
        Task<bool> ProjeMesajSilme(string projeMesajId);
        Task<bool> ProjeMesajSilme(List<string> projeMesajIdList);
        Task<bool> ProjeMesajDetaySilme(string projeMesajDetayId);
        Task<bool> ProjeMesajDetaySilme(List<string> projeMesajDetayIdList);
    }
}