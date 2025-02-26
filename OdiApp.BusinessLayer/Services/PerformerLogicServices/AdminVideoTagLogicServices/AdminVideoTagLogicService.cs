using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.VideoTagDataServices;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTagLogicServices;

public class AdminVideoTagLogicService : IAdminVideoTagLogicService
{
    private readonly IMapper _mapper;
    //private readonly IVideoTagDataService _videoTagDataService;

    public AdminVideoTagLogicService(IMapper mapper, IVideoTagDataService videoTagDataService)
    {
        _mapper = mapper;
        //_videoTagDataService = videoTagDataService;
    }

    //public async Task<OdiResponse<VideoTag>> VideoTagOlustur(VideoTag model, OdiUser user)
    //{
    //    DateTime date = DateTime.Now;

    //    model.EklenmeTarihi = date;
    //    model.Ekleyen = user.AdSoyad;
    //    model.EkleyenId = user.Id;

    //    model.GuncellenmeTarihi = date;
    //    model.Guncelleyen = user.AdSoyad;
    //    model.GuncelleyenId = user.Id;

    //    model = await _videoTagDataService.YeniVideoTag(model);

    //    return OdiResponse<VideoTag>.Success("Video tag oluşturuldu.", model, 200);
    //}

    //public async Task<OdiResponse<VideoTag>> VideoTagGuncelle(VideoTag model, OdiUser user)
    //{
    //    VideoTag videoTag = await _videoTagDataService.VideoTagGetir(model.Id);

    //    if (videoTag == null) return OdiResponse<VideoTag>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

    //    videoTag = _mapper.Map<VideoTag, VideoTag>(model, videoTag);

    //    DateTime date = DateTime.Now;

    //    videoTag.GuncellenmeTarihi = date;
    //    videoTag.Guncelleyen = user.AdSoyad;
    //    videoTag.GuncelleyenId = user.Id;

    //    await _videoTagDataService.VideoTagGuncelle(videoTag);

    //    return OdiResponse<VideoTag>.Success("Video tag güncellendi.", videoTag, 200);
    //}

    //public async Task<OdiResponse<VideoTag>> VideoTagDurumDegistir(VideoTagIdDTO model, OdiUser user)
    //{
    //    VideoTag videoTag = await _videoTagDataService.VideoTagGetir(model.VideoTagId);

    //    if (videoTag == null) return OdiResponse<VideoTag>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

    //    videoTag.Aktif = !videoTag.Aktif;

    //    DateTime date = DateTime.Now;

    //    videoTag.GuncellenmeTarihi = date;
    //    videoTag.Guncelleyen = user.AdSoyad;
    //    videoTag.GuncelleyenId = user.Id;

    //    await _videoTagDataService.VideoTagGuncelle(videoTag);

    //    return OdiResponse<VideoTag>.Success("Video tag aktif durumu güncellendi.", videoTag, 200);
    //}

    //public async Task<OdiResponse<List<VideoTag>>> VideoTagListele(DilIdDTO model)
    //{
    //    List<VideoTag> videoTagList = await _videoTagDataService.VideoTagListesiGetir(model.DilId);

    //    return OdiResponse<List<VideoTag>>.Success("Video tag listesi getirildi.", videoTagList, 200);
    //}

    //public async Task<OdiResponse<VideoTag>> VideoTagGetir(VideoTagIdDTO model)
    //{
    //    VideoTag videoTag = await _videoTagDataService.VideoTagGetir(model.VideoTagId);

    //    return OdiResponse<VideoTag>.Success("Video tag getirildi.", videoTag, 200);
    //}
}