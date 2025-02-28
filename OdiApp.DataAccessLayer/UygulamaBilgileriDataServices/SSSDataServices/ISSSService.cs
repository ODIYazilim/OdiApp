using OdiApp.DTOs.UygulamaBilgileriDTOs.SSSDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SSSDataServices
{
    public interface ISSSService
    {
        public Task<List<SSS>> SSSEkle(SSS model);
        public Task<List<SSS>> SSSGuncelle(SSS model);
        public Task<List<SSS>> SSSListesi(int dilId = -1, string kayitGrubu = "", bool onlyAktif = false);
        public Task<List<SSSOutputDTO>> SSSOutputListesi(SSSListeInputDTO model, int dilId = -1);
    }
}
