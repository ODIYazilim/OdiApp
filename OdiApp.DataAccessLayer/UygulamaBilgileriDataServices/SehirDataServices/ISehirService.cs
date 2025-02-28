using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SehirDataServices
{
    public interface ISehirService
    {
        Task<List<Sehir>> SehirListesi();
    }
}
