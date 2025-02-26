namespace OdiApp.DataAccessLayer.PerformerDataServices.VideoAlbumm;

public class VideoAlbumTipiDataService : IVideoAlbumTipiDataService
{
    ApplicationDbContext _dbContext;
    public VideoAlbumTipiDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //public async Task<List<VideoAlbumTipiLabel>> VideoAlbumTipiListe(int dilId)
    //{
    //    return await _dbContext.VideoAlbumTipiLabels.Where(x => x.DilId == dilId).ToListAsync();
    //}
}
