using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.KullaniciBasicLogicServices
{
    public interface IKullaniciBasicLogicService
    {
        Task<OdiResponse<KullaniciBasic>> KullaniciEkle(KullaniciBasicEkleDTO kullaniciBasicEkleDTO, OdiUser user);
        Task<OdiResponse<KullaniciBasic>> KullaniciGuncelle(KullaniciBasic kullaniciBasic, OdiUser user);
        Task<OdiResponse<KullaniciBasic>> ProfilFotografiGuncelle(ProfilFotoPostDTO profilFotoPostDTO, OdiUser user);
        Task<OdiResponse<KullaniciBasic>> AdSoyadGuncelle(KullaniciCVAdSoyadDTO kullaniciCVAdSoyadDTO, OdiUser user);
        Task<OdiResponse<KullaniciBasic>> EmailGuncelle(KullaniciEmailDTO kullaniciEmailDTO, OdiUser user);
        Task<OdiResponse<KullaniciBasic>> TelefonNumarasiGuncelle(KullaniciTelefonNumarasiDTO kullaniciTelefonNumarasiDTO, OdiUser user);
    }
}