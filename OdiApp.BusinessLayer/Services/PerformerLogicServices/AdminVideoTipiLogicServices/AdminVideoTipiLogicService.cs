using AutoMapper;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTipiLogicServices;

public class AdminVideoTipiLogicService : IAdminVideoTipiLogicService
{
    private readonly IMapper _mapper;
    //private readonly IVideoTipiDataService _videoTipiDataService;

    public AdminVideoTipiLogicService(IMapper mapper/*, IVideoTipiDataService videoTipiDataService*/)
    {
        _mapper = mapper;
        //_videoTipiDataService = videoTipiDataService;
    }

    //public async Task<OdiResponse<VideoTipi>> VideoTipiOlustur(VideoTipi model, OdiUser user)
    //{
    //    DateTime date = DateTime.Now;

    //    model.EklenmeTarihi = date;
    //    model.Ekleyen = user.AdSoyad;
    //    model.EkleyenId = user.Id;

    //    model.GuncellenmeTarihi = date;
    //    model.Guncelleyen = user.AdSoyad;
    //    model.GuncelleyenId = user.Id;

    //    model = await _videoTipiDataService.YeniVideoTipi(model);

    //    return OdiResponse<VideoTipi>.Success("Video tipi oluşturuldu.", model, 200);
    //}

    //public async Task<OdiResponse<VideoTipi>> VideoTipiGuncelle(VideoTipi model, OdiUser user)
    //{
    //    VideoTipi videoTipi = await _videoTipiDataService.VideoTipiGetir(model.Id);

    //    if (videoTipi == null) return OdiResponse<VideoTipi>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

    //    videoTipi = _mapper.Map<VideoTipi, VideoTipi>(model, videoTipi);

    //    DateTime date = DateTime.Now;

    //    videoTipi.GuncellenmeTarihi = date;
    //    videoTipi.Guncelleyen = user.AdSoyad;
    //    videoTipi.GuncelleyenId = user.Id;

    //    await _videoTipiDataService.VideoTipiGuncelle(videoTipi);

    //    return OdiResponse<VideoTipi>.Success("Video tipi güncellendi.", videoTipi, 200);
    //}

    //public async Task<OdiResponse<VideoTipi>> VideoTipiDurumDegistir(VideoTipiIdDTO model, OdiUser user)
    //{
    //    VideoTipi videoTipi = await _videoTipiDataService.VideoTipiGetir(model.VideoTipiId);

    //    if (videoTipi == null) return OdiResponse<VideoTipi>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

    //    videoTipi.Aktif = !videoTipi.Aktif;

    //    DateTime date = DateTime.Now;

    //    videoTipi.GuncellenmeTarihi = date;
    //    videoTipi.Guncelleyen = user.AdSoyad;
    //    videoTipi.GuncelleyenId = user.Id;

    //    await _videoTipiDataService.VideoTipiGuncelle(videoTipi);

    //    return OdiResponse<VideoTipi>.Success("Video tipi aktif durumu güncellendi.", videoTipi, 200);
    //}

    //public async Task<OdiResponse<List<VideoTipi>>> VideoTipiListele(DilIdDTO model)
    //{
    //    List<VideoTipi> videoTipiList = await _videoTipiDataService.VideoTipiListesiGetir(model.DilId);

    //    return OdiResponse<List<VideoTipi>>.Success("Video tipi listesi getirildi.", videoTipiList, 200);
    //}

    //public async Task<OdiResponse<VideoTipi>> VideoTipiGetir(VideoTipiIdDTO model)
    //{
    //    VideoTipi videoTipi = await _videoTipiDataService.VideoTipiGetir(model.VideoTipiId);

    //    return OdiResponse<VideoTipi>.Success("Video tipi getirildi.", videoTipi, 200);
    //}
}