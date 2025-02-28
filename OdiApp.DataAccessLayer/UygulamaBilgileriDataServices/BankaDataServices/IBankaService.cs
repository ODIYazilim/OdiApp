using OdiApp.DTOs.UygulamaBilgileriDTOs.BankaDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.BankaDataServices
{
    public interface IBankaService
    {
        public Task<List<Banka>> BankaEkle(Banka model);
        public Task<List<Banka>> BankaGuncelle(Banka model);
        public Task<List<Banka>> BankaListesi(bool onlyAktif = false);
        public Task<List<BankaOutputDTO>> BankaOutputListesi();
    }
}