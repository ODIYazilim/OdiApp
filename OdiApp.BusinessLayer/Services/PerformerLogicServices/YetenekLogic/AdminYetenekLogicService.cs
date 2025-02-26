using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekData;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.YetenekModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekLogic;

public class AdminYetenekLogicService : IAdminYetenekLogicService
{
    IMapper _mapper;
    IYetenekDataService _yetenekService;
    public AdminYetenekLogicService(IMapper mapper, IYetenekDataService yetenekService)
    {
        _mapper = mapper;
        _yetenekService = yetenekService;
    }

    public async Task<OdiResponse<YetenekTipi>> YeniYetenekTipi(YetenekTipi yetenekTipi, OdiUser user)
    {
        yetenekTipi.EklenmeTarihi = DateTime.Now;
        yetenekTipi.GuncellenmeTarihi = DateTime.Now;
        yetenekTipi.Ekleyen = user.AdSoyad;
        yetenekTipi.Guncelleyen = user.AdSoyad;
        yetenekTipi.EkleyenId = user.Id;
        yetenekTipi.GuncelleyenId = user.Id;

        yetenekTipi = await _yetenekService.YeniYetenekTipi(yetenekTipi);
        if (yetenekTipi.Id > 0) return OdiResponse<YetenekTipi>.Success("Yetenek tipi eklendi", yetenekTipi, 200);
        else
            return OdiResponse<YetenekTipi>.Fail("Yetenek Tipi eklenemedi", "", 400);
    }
    public async Task<OdiResponse<Yetenek>> YeniYetenek(Yetenek yetenek, OdiUser user)
    {
        yetenek.EklenmeTarihi = DateTime.Now;
        yetenek.GuncellenmeTarihi = DateTime.Now;
        yetenek.Ekleyen = user.AdSoyad;
        yetenek.Guncelleyen = user.AdSoyad;
        yetenek.EkleyenId = user.Id;
        yetenek.GuncelleyenId = user.Id;

        yetenek = await _yetenekService.YeniYetenek(yetenek);
        if (yetenek.Id > 0) return OdiResponse<Yetenek>.Success("Yetenek eklendi", yetenek, 200);
        else
            return OdiResponse<Yetenek>.Fail("Yetenek eklenemedi", "", 400);
    }


}
