using OdiApp.DTOs.UygulamaBilgileriDTOs.SosyalMedyaDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SosyalMedyaDataServices
{
    public interface ISosyalMedyaService
    {
        public Task<List<SosyalMedya>> SosyalMedyaEkle(SosyalMedya model);
        public Task<List<SosyalMedya>> SosyalMedyaGuncelle(SosyalMedya model);
        public Task<List<SosyalMedya>> SosyalMedyaListesi(bool onlyAktif = false);
        public Task<List<SosyalMedyaOutputDTO>> SosyalMedyaOutputListesi();
    }
}