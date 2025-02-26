
using Microsoft.AspNetCore.Identity;
using OdiApp.BusinessLayer.Services.Token;
using OdiApp.DTOs.GlobalDTOs;
using OdiApp.DTOs.IdentityDTOs;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.TokenDTOs;
using OdiApp.Entity.Identity;

namespace OdiApp.BusinessLayer.Services.Kullanici
{
    public interface IKullaniciLogicService
    {
        Task<OdiResponse<UserTokenDTO>> SignUp(SignupDTO signupDTO);
        Task<OdiResponse<KullaniciBilgileriDTO>> GetUserById(KullaniciIdDTO kullaniciId);
    }


    public class KullaniciLogicService : IKullaniciLogicService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authenticationService;
        public KullaniciLogicService(UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        public async Task<OdiResponse<UserTokenDTO>> SignUp(SignupDTO signupDTO)
        {
            var result = await this.YeniKullaniciEkle(signupDTO);
            if (!result)
            {
                return OdiResponse<UserTokenDTO>.Fail("Kullanıcı oluşturulurken hata oluştu", "", 500);
            }
            else
            {

                await _authenticationService.CreateUserTokenAsync(new LoginDTO { PhoneNumber = signupDTO.UlkeTelefonKodu.ToString() + signupDTO.TelefonNumarasi, Password = signupDTO.Sifre });
                return OdiResponse<UserTokenDTO>.Success("Kullanıcı eklendi", 200);
            }

        }

        public Task<OdiResponse<KullaniciBilgileriDTO>> GetUserById(KullaniciIdDTO kullaniciId)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> YeniKullaniciEkle(SignupDTO signupDTO)
        {
            var strUserName = signupDTO.UlkeTelefonKodu.ToString() + signupDTO.TelefonNumarasi;

            var user = new ApplicationUser
            {
                UserName = strUserName,
                AdSoyad = signupDTO.TamAdi,
                KayitTuruKodu = signupDTO.KayitTuruKodu,
                KayitGrubuKodu = signupDTO.KayitGrubuKodu,
                PhoneNumber = signupDTO.TelefonNumarasi,
                Email = signupDTO.Email,
                KVKK = signupDTO.KVKK,
                KullaniciSozlesmesi = signupDTO.KullaniciSozlesmesi,
                GizlilikSozlesmesi = signupDTO.GizlilikSozlesmesi,
                UlkeTelefonKodu = signupDTO.UlkeTelefonKodu,
                LastLogin = DateTime.Now,
                PreviusLogin = DateTime.Now,
                KayitTarihi = DateTime.Now,
                CocukMu = signupDTO.CocukMu,
                VeliAdSoyad = signupDTO.VeliAdSoyad,
                VeliTelefon = signupDTO.VeliTelefon,
                ProfilFotografi = "",
            };
            var result = await _userManager.CreateAsync(user, signupDTO.Sifre);
            return result.Succeeded ? true : false;
        }

        private async Task<KullaniciBilgileriDTO> KullaniciBilgileriDTOGetir(ApplicationUser user)
        {
            KullaniciBilgileriDTO userDto = new KullaniciBilgileriDTO();
            userDto.Id = user.Id;
            userDto.AdSoyad = user.AdSoyad;
            userDto.ProfilFotografiDosyaYolu = string.IsNullOrEmpty(user.ProfilFotografi) ? "" : user.ProfilFotografi;
            userDto.TelefonNumarasi = user.PhoneNumber;
            userDto.UlkeTelefonKodu = user.UlkeTelefonKodu;
            userDto.Email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
            userDto.SonGirisTarihi = user.LastLogin;
            userDto.KayitTarihi = user.KayitTarihi;
            userDto.KayitGrubuKoduListesi = user.KayitGrubuKodu.Split(",").ToList();
            userDto.KayitTuruKoduListesi = user.KayitTuruKodu.Split(",").ToList();
            userDto.CocukMu = user.CocukMu;
            userDto.Premium = user.Premium;
            userDto.OnayliKullanici = user.OnayliKullanici;
            userDto.OnayBekliyor = user.OnayBekliyor;
            userDto.Reddedildi = user.Reddedildi;
            userDto.DogumTarihi = user.DogumTarihi;
            userDto.Sehir = user.Sehir;
            userDto.VeliAdSoyad = user.VeliAdSoyad;
            userDto.VeliTelefon = user.VeliTelefon;

            //OdiResponse<FirmaKullanici> firmaKullanici = await _firmaLogicService.FirmaKullaniciGetir(user.Id);

            //if (firmaKullanici.Data != null)
            //{
            //    OdiResponse<Firma> firma = await _firmaLogicService.FirmaGetir(firmaKullanici.Data.FirmaKodu);

            //    if (firma.Data != null)
            //    {
            //        userDto.FirmaKodu = firma.Data.FirmaKodu;
            //        userDto.FirmaAdi = firma.Data.FirmaAdi;
            //    }
            //}
            //else
            //{
            //    OdiResponse<OnerilenFirma> onerilenFirma = await _firmaLogicService.OnerilenFirmaGetir(user.Id);

            //    if (onerilenFirma.Data != null)
            //    {
            //        userDto.FirmaKodu = "";
            //        userDto.FirmaAdi = onerilenFirma.Data.FirmaAdi;
            //    }
            //}

            return userDto;
        }
    }

}
