using AutoMapper;
using OdiApp.DataAccessLayer.BildirimDataServices.KullaniciBasicDataServices;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.KullaniciBasicLogicServices
{
    public class KullaniciBasicLogicService : IKullaniciBasicLogicService
    {
        private readonly IKullaniciBasicDataService _kullaniciBasicDataService;
        private readonly IMapper _mapper;

        public KullaniciBasicLogicService(IKullaniciBasicDataService kullaniciBasicDataService, IMapper mapper)
        {
            _kullaniciBasicDataService = kullaniciBasicDataService;
            _mapper = mapper;
        }

        public async Task<OdiResponse<KullaniciBasic>> KullaniciEkle(KullaniciBasicEkleDTO kullaniciBasicEkleDTO, OdiUser user)
        {
            KullaniciBasic kullaniciBasic = _mapper.Map<KullaniciBasic>(kullaniciBasicEkleDTO);

            DateTime dateTime = DateTime.Now;

            kullaniciBasic.EklenmeTarihi = dateTime;
            kullaniciBasic.Ekleyen = user.AdSoyad;
            kullaniciBasic.EkleyenId = user.Id;

            kullaniciBasic.GuncellenmeTarihi = dateTime;
            kullaniciBasic.Guncelleyen = user.AdSoyad;
            kullaniciBasic.GuncelleyenId = user.Id;

            kullaniciBasic = await _kullaniciBasicDataService.KullaniciEkle(kullaniciBasic);

            return OdiResponse<KullaniciBasic>.Success("Kullanıcı güncellendi.", kullaniciBasic, 200);
        }

        public async Task<OdiResponse<KullaniciBasic>> KullaniciGuncelle(KullaniciBasic yeniKullaniciBasic, OdiUser user)
        {
            KullaniciBasic kullaniciBasic = await _kullaniciBasicDataService.KullaniciGetir(yeniKullaniciBasic.KullaniciId);
            if (kullaniciBasic == null) return OdiResponse<KullaniciBasic>.Fail("Bu id ile bir kullanici bulunamadı", "Not Found", 404);

            kullaniciBasic = _mapper.Map<KullaniciBasic, KullaniciBasic>(yeniKullaniciBasic, kullaniciBasic);

            kullaniciBasic.GuncellenmeTarihi = DateTime.Now;
            kullaniciBasic.Guncelleyen = user.AdSoyad;
            kullaniciBasic.GuncelleyenId = user.Id;

            kullaniciBasic = await _kullaniciBasicDataService.KullaniciGuncelle(kullaniciBasic);

            return OdiResponse<KullaniciBasic>.Success("Kullanıcı güncellendi.", kullaniciBasic, 200);
        }

        public async Task<OdiResponse<KullaniciBasic>> ProfilFotografiGuncelle(ProfilFotoPostDTO profilFotoPostDTO, OdiUser user)
        {
            KullaniciBasic kullaniciBasic = await _kullaniciBasicDataService.KullaniciGetir(profilFotoPostDTO.KullaniciId);
            if (kullaniciBasic == null) return OdiResponse<KullaniciBasic>.Fail("Bu id ile bir kullanici bulunamadı", "Not Found", 404);

            kullaniciBasic.KullaniciProfilResmi = profilFotoPostDTO.DosyaYolu;

            kullaniciBasic.GuncellenmeTarihi = DateTime.Now;
            kullaniciBasic.Guncelleyen = user.AdSoyad;
            kullaniciBasic.GuncelleyenId = user.Id;

            kullaniciBasic = await _kullaniciBasicDataService.KullaniciGuncelle(kullaniciBasic);

            return OdiResponse<KullaniciBasic>.Success("Kullanıcı profil fotoğrafı güncellendi.", kullaniciBasic, 200);
        }

        public async Task<OdiResponse<KullaniciBasic>> AdSoyadGuncelle(KullaniciCVAdSoyadDTO kullaniciCVAdSoyadDTO, OdiUser user)
        {
            KullaniciBasic kullaniciBasic = await _kullaniciBasicDataService.KullaniciGetir(kullaniciCVAdSoyadDTO.KullaniciId);
            if (kullaniciBasic == null) return OdiResponse<KullaniciBasic>.Fail("Bu id ile bir kullanici bulunamadı", "Not Found", 404);

            kullaniciBasic.KullaniciAdSoyad = kullaniciCVAdSoyadDTO.KullaniciAdSoyad;

            kullaniciBasic.GuncellenmeTarihi = DateTime.Now;
            kullaniciBasic.Guncelleyen = user.AdSoyad;
            kullaniciBasic.GuncelleyenId = user.Id;

            kullaniciBasic = await _kullaniciBasicDataService.KullaniciGuncelle(kullaniciBasic);

            return OdiResponse<KullaniciBasic>.Success("Kullanıcı ad soyad güncellendi.", kullaniciBasic, 200);
        }

        public async Task<OdiResponse<KullaniciBasic>> EmailGuncelle(KullaniciEmailDTO kullaniciEmailDTO, OdiUser user)
        {
            KullaniciBasic kullaniciBasic = await _kullaniciBasicDataService.KullaniciGetir(kullaniciEmailDTO.KullaniciId);
            if (kullaniciBasic == null) return OdiResponse<KullaniciBasic>.Fail("Bu id ile bir kullanici bulunamadı", "Not Found", 404);

            kullaniciBasic.KullaniciEmail = kullaniciEmailDTO.KullaniciEmail;

            kullaniciBasic.GuncellenmeTarihi = DateTime.Now;
            kullaniciBasic.Guncelleyen = user.AdSoyad;
            kullaniciBasic.GuncelleyenId = user.Id;

            kullaniciBasic = await _kullaniciBasicDataService.KullaniciGuncelle(kullaniciBasic);

            return OdiResponse<KullaniciBasic>.Success("Kullanıcı e-mail güncellendi.", kullaniciBasic, 200);
        }

        public async Task<OdiResponse<KullaniciBasic>> TelefonNumarasiGuncelle(KullaniciTelefonNumarasiDTO kullaniciTelefonNumarasiDTO, OdiUser user)
        {
            KullaniciBasic kullaniciBasic = await _kullaniciBasicDataService.KullaniciGetir(kullaniciTelefonNumarasiDTO.KullaniciId);
            if (kullaniciBasic == null) return OdiResponse<KullaniciBasic>.Fail("Bu id ile bir kullanici bulunamadı", "Not Found", 404);

            kullaniciBasic.KullaniciTelefon = kullaniciTelefonNumarasiDTO.KullaniciTelefon;

            kullaniciBasic.GuncellenmeTarihi = DateTime.Now;
            kullaniciBasic.Guncelleyen = user.AdSoyad;
            kullaniciBasic.GuncelleyenId = user.Id;

            kullaniciBasic = await _kullaniciBasicDataService.KullaniciGuncelle(kullaniciBasic);

            return OdiResponse<KullaniciBasic>.Success("Kullanıcı telefon numarası güncellendi.", kullaniciBasic, 200);
        }
    }
}