using AutoMapper;
using Odi.Shared.Services.Interface;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public class VideoAlbumLogicService : IVideoAlbumLogicService
{
    //private readonly IVideoAlbumTipiDataService _albumTipiService;
    //private readonly IVideoAlbumDataService _albumDataService;
    private readonly IMapper _mapper;
    private readonly IAmazonS3Service _amazonS3Service;

    //public VideoAlbumLogicService(IMapper mapper, IAmazonS3Service amazonS3Service, IVideoAlbumTipiDataService albumTipiService, IVideoAlbumDataService albumDataService)
    //{
    //    _mapper = mapper;
    //    _albumTipiService = albumTipiService;
    //    _albumDataService = albumDataService;
    //    _amazonS3Service = amazonS3Service;
    //}
    //public async Task<OdiResponse<List<VideoAlbumTipiDTO>>> VideoAlbumTipiListesi(int dilId)
    //{
    //    return OdiResponse<List<VideoAlbumTipiDTO>>.Success("Video Albüm Tipleri Listelenedi", _mapper.Map<List<VideoAlbumTipiDTO>>(await _albumTipiService.VideoAlbumTipiListe(dilId)), 200);
    //}

    //#region VİDEO ALBUMLER
    //public async Task<OdiResponse<VideoAlbumDTO>> YeniVideoAlbum(VideoAlbumDTO album, OdiUser user)
    //{
    //    VideoAlbum alb = _mapper.Map<VideoAlbum>(album);
    //    alb.EklenmeTarihi = DateTime.Now;
    //    alb.Ekleyen = user.AdSoyad;
    //    alb.EkleyenId = user.Id;

    //    alb.GuncellenmeTarihi = DateTime.Now;
    //    alb.Guncelleyen = user.AdSoyad;
    //    alb.GuncelleyenId = user.Id;

    //    alb = await _albumDataService.YeniVideoAlbum(alb);
    //    return OdiResponse<VideoAlbumDTO>.Success("Yeni Albüm oluşturuldu", _mapper.Map<VideoAlbumDTO>(alb), 200);
    //}
    //public async Task<OdiResponse<VideoAlbumDTO>> VideoAlbumGuncel(VideoAlbumDTO album, OdiUser user)
    //{
    //    VideoAlbum alb = new VideoAlbum();
    //    alb = _mapper.Map<VideoAlbumDTO, VideoAlbum>(album, alb);
    //    alb.GuncellenmeTarihi = DateTime.Now;
    //    alb.Guncelleyen = user.AdSoyad;
    //    alb.GuncelleyenId = user.Id;

    //    alb = await _albumDataService.VideoAlbumGuncelle(alb);

    //    return OdiResponse<VideoAlbumDTO>.Success("Albüm güncellendi", _mapper.Map<VideoAlbumDTO>(alb), 200);
    //}
    //public async Task<OdiResponse<List<VideoAlbumDTO>>> VideoAlbumListesi(KullaniciIdDTO kullaniciId)
    //{
    //    List<VideoAlbum> list = await _albumDataService.VideoAlbumListe(kullaniciId.KullaniciId);
    //    foreach (VideoAlbum item in list)
    //    {
    //        item.Videolar = await _albumDataService.AlbumVideolari(item.Id);
    //    }
    //    foreach (VideoAlbum album in list)
    //    {
    //        foreach (var foto in album.Videolar)
    //        {
    //            foto.DosyaYolu = _amazonS3Service.GetPreSignedUrl(foto.DosyaYolu);
    //        }
    //    }

    //    return OdiResponse<List<VideoAlbumDTO>>.Success("Video Albüm Listesi getirildi", _mapper.Map<List<VideoAlbumDTO>>(list), 200);
    //}
    //public async Task<OdiResponse<NoContent>> VideoAlbumSil(VideoAlbumIdDTO videoAlbumId)
    //{
    //    bool result = await _albumDataService.VideoAlbumSil(videoAlbumId.VideoAlbumId);
    //    if (result) return OdiResponse<NoContent>.Success("Video albümü silindi", 200);
    //    else return OdiResponse<NoContent>.Fail("Bu id ile video albümü bulunamadı", "Not Found", 404);
    //}

    //#endregion

    //#region VİDEOLAR
    //public async Task<OdiResponse<VideoAlbumVideoDTO>> YeniAlbumVideo(VideoAlbumVideoDTO video, OdiUser user)
    //{
    //    VideoAlbumVideo vi = _mapper.Map<VideoAlbumVideo>(video);
    //    vi.EklenmeTarihi = DateTime.Now;
    //    vi.Ekleyen = user.AdSoyad;
    //    vi.EkleyenId = user.Id;

    //    vi.GuncellenmeTarihi = DateTime.Now;
    //    vi.Guncelleyen = user.AdSoyad;
    //    vi.GuncelleyenId = user.Id;

    //    vi = await _albumDataService.YeniAlbumVideo(vi);

    //    vi.DosyaYolu= _amazonS3Service.GetPreSignedUrl(vi.DosyaYolu);
    //    return OdiResponse<VideoAlbumVideoDTO>.Success("Yeni Video eklendi", _mapper.Map<VideoAlbumVideoDTO>(vi), 200);
    //}
    //public async  Task<OdiResponse<VideoAlbumVideoDTO>> AlbumVideoGuncelle(VideoAlbumVideoDTO video, OdiUser user)
    //{
    //    VideoAlbumVideo vi = _mapper.Map<VideoAlbumVideo>(video);

    //    vi.GuncellenmeTarihi = DateTime.Now;
    //    vi.Guncelleyen = user.AdSoyad;
    //    vi.GuncelleyenId = user.Id;

    //    vi = await _albumDataService.AlbumVideoGuncelle(vi);

    //    vi.DosyaYolu = _amazonS3Service.GetPreSignedUrl(vi.DosyaYolu);
    //    return OdiResponse<VideoAlbumVideoDTO>.Success("Video Güncellendi", _mapper.Map<VideoAlbumVideoDTO>(vi), 200);
    //}
    //public async Task<OdiResponse<List<VideoAlbumVideoDTO>>> AlbumVideolari(VideoAlbumIdDTO videoAlbumId)
    //{
    //    List<VideoAlbumVideo> list = await _albumDataService.AlbumVideolari(videoAlbumId.VideoAlbumId);
    //    foreach (var video in list)
    //    {
    //        video.DosyaYolu = _amazonS3Service.GetPreSignedUrl(video.DosyaYolu);
    //    }
    //    return OdiResponse<List<VideoAlbumVideoDTO>>.Success("Video Listesi getirildi", _mapper.Map<List<VideoAlbumVideoDTO>>(list), 200);
    //}

    //public async Task<OdiResponse<NoContent>> AlbumVideoSil(VideoAlbumVideoIdDTO videoId)
    //{
    //    bool result = await _albumDataService.AlbumVideoSil(videoId.VideoId);
    //    if (result) return OdiResponse<NoContent>.Success("Video albümü silindi", 200);
    //    else return OdiResponse<NoContent>.Fail("Bu id ile video albümü bulunamadı", "Not Found", 404);
    //}
    //#endregion
}
