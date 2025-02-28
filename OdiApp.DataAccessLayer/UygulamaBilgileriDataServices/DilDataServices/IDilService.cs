using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.DilDataServices
{
    public interface IDilService
    {
        Task<List<Dil>> AktifDilListesi();
        Task<List<Dil>> DilListesi();
        Task<Dil> DilGetir(int id);
        Task<Dil> YeniDil(Dil dil);
        Task<Dil> DilGuncelle(Dil dil);
        Task<bool> DilSil(Dil dil);
    }
}
