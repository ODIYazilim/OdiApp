using OdiApp.DTOs.UygulamaBilgileriDTOs.UlkeDTOs;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.UlkeServices
{
    public interface IUlkeService
    {
        Task<List<UlkeDTO>> UlkeListesi();
    }
}