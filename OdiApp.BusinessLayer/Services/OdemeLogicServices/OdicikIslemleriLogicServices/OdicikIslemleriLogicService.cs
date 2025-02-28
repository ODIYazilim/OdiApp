using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.OdemeDataServices.OdicikIslemleriDataServices;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.OdemeDTOs.OdicikİslemleriDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.OdemeModels.OdicikModels;

namespace OdiApp.BusinessLayer.Services.OdemeLogicServices.OdicikIslemleriLogicServices
{
    public class OdicikIslemleriLogicService : IOdicikIslemleriLogicService
    {
        private readonly IMapper _mapper;
        private readonly IOdicikIslemleriDataService _odicikIslemleriDataService;
        private readonly IUseOtherService _useOtherService;
        private readonly IConfiguration _configuration;

        public OdicikIslemleriLogicService(IMapper mapper, IOdicikIslemleriDataService odicikIslemleriDataService, IUseOtherService useOtherService, IConfiguration configuration)
        {
            _mapper = mapper;
            _odicikIslemleriDataService = odicikIslemleriDataService;
            _useOtherService = useOtherService;
            _configuration = configuration;
        }

        public async Task<OdiResponse<bool>> OdicikEkleme(OdicikEklemeDTO model, OdiUser user, string jwtToken)
        {
            OdicikIslemleri odicikIslemleri = _mapper.Map<OdicikIslemleri>(model);

            odicikIslemleri.IslemTipi = OdicikIslemTipleri.Gelen;

            odicikIslemleri.Ekleyen = user.AdSoyad;
            odicikIslemleri.EkleyenId = user.Id;
            odicikIslemleri.EklenmeTarihi = DateTime.Now;
            odicikIslemleri.Guncelleyen = user.AdSoyad;
            odicikIslemleri.GuncelleyenId = user.Id;
            odicikIslemleri.GuncellenmeTarihi = DateTime.Now;

            await _odicikIslemleriDataService.YeniOdicikIslemler(odicikIslemleri);

            dynamic dync = await _useOtherService.PostMethod(model, _configuration.GetSection("IdentityServerURL").Value + "/api/odicik-islemleri/odicik-ekleme", jwtToken);

            return OdiResponse<bool>.Success("Yeni işlem kaydı oluşturuldu.", true, 200);
        }

        public async Task<OdiResponse<bool>> OdicikHarcama(OdicikHarcamaDTO model, OdiUser user, string jwtToken)
        {
            int islemlerToplami = await _odicikIslemleriDataService.OdicikBakiyeToplamiGetir(model.KullaniciId);

            if (islemlerToplami < model.OdicikMiktari)
                return OdiResponse<bool>.Fail("Bakiye yetersiz.", "Bad Request", 400);

            OdicikIslemleri odicikIslemleri = _mapper.Map<OdicikIslemleri>(model);

            odicikIslemleri.OdicikMiktari = odicikIslemleri.OdicikMiktari * -1;
            odicikIslemleri.IslemTipi = OdicikIslemTipleri.Giden;

            odicikIslemleri.Ekleyen = user.AdSoyad;
            odicikIslemleri.EkleyenId = user.Id;
            odicikIslemleri.EklenmeTarihi = DateTime.Now;
            odicikIslemleri.Guncelleyen = user.AdSoyad;
            odicikIslemleri.GuncelleyenId = user.Id;
            odicikIslemleri.GuncellenmeTarihi = DateTime.Now;

            await _odicikIslemleriDataService.YeniOdicikIslemler(odicikIslemleri);

            dynamic dync = await _useOtherService.PostMethod(model, _configuration.GetSection("IdentityServerURL").Value + "/api/odicik-islemleri/odicik-harcama", jwtToken);

            return OdiResponse<bool>.Success("Yeni işlem kaydı oluşturuldu.", true, 200);
        }

        public async Task<OdiResponse<List<OdicikIslemleriOutputDTO>>> OdicikHareketleri(KullaniciIdDTO model)
        {
            List<OdicikIslemleri> odicikIslemleriList = await _odicikIslemleriDataService.OdicikHareketleriGetir(model.KullaniciId);
            return OdiResponse<List<OdicikIslemleriOutputDTO>>.Success("Odicik hareketleri getirildi.", _mapper.Map<List<OdicikIslemleriOutputDTO>>(odicikIslemleriList), 200);
        }

        public async Task<OdiResponse<OdicikBakiyeOutputDTO>> OdicikBakiye(KullaniciIdDTO model)
        {
            OdicikBakiyeOutputDTO outputDTO = new OdicikBakiyeOutputDTO();

            outputDTO.KullaniciId = model.KullaniciId;
            outputDTO.OdicikMiktari = await _odicikIslemleriDataService.OdicikBakiyeToplamiGetir(model.KullaniciId);

            return OdiResponse<OdicikBakiyeOutputDTO>.Success("Odicik bakiye getirildi.", outputDTO, 200);
        }
    }
}