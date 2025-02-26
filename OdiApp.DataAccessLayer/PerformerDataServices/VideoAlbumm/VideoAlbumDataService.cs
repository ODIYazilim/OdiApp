namespace OdiApp.DataAccessLayer.PerformerDataServices.VideoAlbumm;

public class VideoAlbumDataService : IVideoAlbumDataService
{
    ApplicationDbContext _dbContext;
    public VideoAlbumDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //#region VİDEO ALBUMLER
    //public async Task<VideoAlbum> YeniVideoAlbum(VideoAlbum album)
    //{
    //    await _dbContext.VideoAlbumleri.AddAsync(album);
    //    await _dbContext.SaveChangesAsync();
    //    album = await _dbContext.VideoAlbumleri.Include(x => x.Videolar).Include(x => x.AlbumTipi).FirstOrDefaultAsync(x => x.Id == album.Id);
    //    return album;
    //}
    //public async Task<VideoAlbum> VideoAlbumGuncelle(VideoAlbum album)
    //{
    //    _dbContext.VideoAlbumleri.Update(album);
    //    await _dbContext.SaveChangesAsync();
    //    album = await _dbContext.VideoAlbumleri.Include(x => x.Videolar).Include(x => x.AlbumTipi).FirstOrDefaultAsync(x => x.Id == album.Id);
    //    return album;
    //}
    //public async Task<List<VideoAlbum>> VideoAlbumListe(string kullaniciId)
    //{
    //    return await _dbContext.VideoAlbumleri.Include(x => x.Videolar).Include(x => x.AlbumTipi).Where(x => x.KullaniciId == kullaniciId).ToListAsync();
    //}

    //public async Task<bool> VideoAlbumSil(int albumId)
    //{
    //    VideoAlbum album = _dbContext.VideoAlbumleri.FirstOrDefault(x => x.Id == albumId);
    //    if (album == null) return false;

    //    _dbContext.VideoAlbumleri.Remove(album);
    //    await _dbContext.SaveChangesAsync();
    //    return true;
    //}

    //#endregion

    //#region VİDEOLAR

    //public async Task<VideoAlbumVideo> YeniAlbumVideo(VideoAlbumVideo video)
    //{
    //    await _dbContext.VideoAlbumVideolar.AddAsync(video);
    //    await _dbContext.SaveChangesAsync();
    //    return video;
    //}
    //public async Task<VideoAlbumVideo> AlbumVideoGuncelle(VideoAlbumVideo video)
    //{
    //    _dbContext.VideoAlbumVideolar.Update(video);
    //    await _dbContext.SaveChangesAsync();
    //    return video;
    //}
    //public async Task<List<VideoAlbumVideo>> AlbumVideolari(int albumId)
    //{
    //    return await _dbContext.VideoAlbumVideolar.Where(x => x.AlbumId == albumId).ToListAsync();
    //}

    //public async Task<bool> AlbumVideoSil(int videoId)
    //{
    //    VideoAlbumVideo video = await _dbContext.VideoAlbumVideolar.FirstOrDefaultAsync(x => x.Id == videoId);
    //    if (video == null) return false;
    //    _dbContext.VideoAlbumVideolar.Remove(video);
    //    await _dbContext.SaveChangesAsync();
    //    return true;
    //}
    //#endregion
}
