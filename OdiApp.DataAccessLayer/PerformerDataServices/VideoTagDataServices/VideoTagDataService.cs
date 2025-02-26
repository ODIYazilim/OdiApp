namespace OdiApp.DataAccessLayer.PerformerDataServices.VideoTagDataServices;

public class VideoTagDataService : IVideoTagDataService
{
    private readonly ApplicationDbContext _dbContext;

    public VideoTagDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public async Task<VideoTag> YeniVideoTag(VideoTag model)
    //{
    //    await _dbContext.VideoTaglari.AddAsync(model);
    //    await _dbContext.SaveChangesAsync();
    //    return model;
    //}

    //public async Task<VideoTag> VideoTagGuncelle(VideoTag model)
    //{
    //    _dbContext.VideoTaglari.Update(model);
    //    await _dbContext.SaveChangesAsync();
    //    return model;
    //}

    //public async Task<List<VideoTag>> VideoTagListesiGetir(int dilId)
    //{
    //    IQueryable<VideoTag> query = _dbContext.VideoTaglari.AsNoTracking();

    //    if (dilId >= 0)
    //    {
    //        query = query.Where(x => x.DilId == dilId);
    //    }

    //    return await query.ToListAsync();
    //}

    //public async Task<VideoTag> VideoTagGetir(string id)
    //{
    //    return await _dbContext.VideoTaglari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    //}
}