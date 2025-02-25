using OdiApp.DTOs.IdentityDTOs;
using OdiApp.DTOs.Kullanici;

namespace OdiApp.DataAccessLayer.Kullanici
{
    public interface IKullaniciDataService
    {
        Task<KullaniciBilgileriDTO> YeniKullaniciOlustur(SignupDTO yeniKullanici);
    }
    public class KullaniciDataService : IKullaniciDataService
    {
        public Task<KullaniciBilgileriDTO> YeniKullaniciOlustur(SignupDTO yeniKullanici)
        {
            throw new NotImplementedException();
        }
    }
}
