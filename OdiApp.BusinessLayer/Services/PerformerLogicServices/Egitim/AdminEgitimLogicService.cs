using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.Egitim;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.Egitim;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.Egitim;

public class AdminEgitimLogicService : IAdminEgitimLogicService
{
    IMapper _mapper;
    IEgitimDataService _egitimService;

    public AdminEgitimLogicService(IMapper mapper, IEgitimDataService egitimService)
    {
        _egitimService = egitimService;
        _mapper = mapper;
    }
    public async Task<OdiResponse<EgitimTipi>> YeniEgitimTipi(EgitimTipi egitimTipi, OdiUser user)
    {
        egitimTipi.EklenmeTarihi = DateTime.Now;
        egitimTipi.GuncellenmeTarihi = DateTime.Now;
        egitimTipi.Ekleyen = user.AdSoyad;
        egitimTipi.Guncelleyen = user.AdSoyad;
        egitimTipi.EkleyenId = user.Id;
        egitimTipi.GuncelleyenId = user.Id;

        egitimTipi = await _egitimService.YeniEgitimTipi(egitimTipi);
        if (egitimTipi.Id > 0) return OdiResponse<EgitimTipi>.Success("Eğitim tipi eklendi", egitimTipi, 200);
        else
            return OdiResponse<EgitimTipi>.Fail("Eğitim Tipi eklenemedi", "", 400);
    }

    public async Task<OdiResponse<Okul>> YeniOkul(Okul okul, OdiUser user)
    {
        okul.EklenmeTarihi = DateTime.Now;
        okul.GuncellenmeTarihi = DateTime.Now;
        okul.Ekleyen = user.AdSoyad;
        okul.Guncelleyen = user.AdSoyad;
        okul.EkleyenId = user.Id;
        okul.GuncelleyenId = user.Id;

        okul = await _egitimService.YeniOkul(okul);
        if (okul.Id > 0) return OdiResponse<Okul>.Success("Eğitim tipi eklendi", okul, 200);
        else
            return OdiResponse<Okul>.Fail("Eğitim Tipi eklenemedi", "", 400);
    }

    public async Task<OdiResponse<OkulBolum>> YeniOkulBolum(OkulBolum bolum, OdiUser user)
    {
        bolum.EklenmeTarihi = DateTime.Now;
        bolum.GuncellenmeTarihi = DateTime.Now;
        bolum.Ekleyen = user.AdSoyad;
        bolum.Guncelleyen = user.AdSoyad;
        bolum.EkleyenId = user.Id;
        bolum.GuncelleyenId = user.Id;

        bolum = await _egitimService.YeniOkulBolum(bolum);
        if (bolum.Id > 0) return OdiResponse<OkulBolum>.Success("Eğitim tipi eklendi", bolum, 200);
        else
            return OdiResponse<OkulBolum>.Fail("Eğitim Tipi eklenemedi", "", 400);
    }
}
