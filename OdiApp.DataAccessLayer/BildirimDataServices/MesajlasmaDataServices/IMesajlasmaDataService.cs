using OdiApp.DTOs.BildirimDTOs.Mesajlasma;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.BildirimModels.Mesajlasma;

namespace OdiApp.DataAccessLayer.BildirimDataServices.MesajlasmaDataServices
{
    public interface IMesajlasmaDataService
    {
        Task<Mesaj> YeniMesaj(Mesaj mesaj);
        Task<MesajDetay> YeniMesajDetay(MesajDetay mesajDetay);
        Task<List<MesajDetay>> YeniMesajDetay(List<MesajDetay> mesajDetay);
        Task<MesajOutputDTO> MesajGetir(string mesajId);
        Task<MesajOutputDTO> MesajGetir(string kullanici1Id, string kullanici2Id);
        Task<List<MesajOutputDTO>> MesajListesi(string kullaniciId);
        Task<MesajDetayOutputDTO> MesajDetayGetir(string mesajDetayId);
        Task<PagedData<MesajDetayOutputDTO>> MesajDetayListesi(string kullanici1Id, string kullanici2Id, int pageNo, int recordsPerPage);
        Task<List<MesajDetayOutputDTO>> MesajDetayListesi(List<string> mesajDetayIdList);
        Task<bool> MesajGoruldu(string mesajDetayId);
        Task<bool> MesajGoruldu(List<string> mesajDetayIdList);
        Task<bool> MesajSilme(string mesajId);
        Task<bool> MesajSilme(List<string> mesajIdList);
        Task<bool> MesajDetaySilme(string mesajDetayId);
        Task<bool> MesajDetaySilme(List<string> mesajDetayIdList);
    }
}