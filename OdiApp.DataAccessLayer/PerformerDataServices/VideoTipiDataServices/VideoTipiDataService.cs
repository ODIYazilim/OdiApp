namespace OdiApp.DataAccessLayer.PerformerDataServices.VideoTipiDataServices;

public class VideoTipiDataService : IVideoTipiDataService
{
    private readonly ApplicationDbContext _dbContext;

    public VideoTipiDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public async Task<VideoTipi> YeniVideoTipi(VideoTipi model)
    //{
    //    await _dbContext.VideoTipleri.AddAsync(model);
    //    await _dbContext.SaveChangesAsync();
    //    return model;
    //}

    //public async Task<VideoTipi> VideoTipiGuncelle(VideoTipi model)
    //{
    //    _dbContext.VideoTipleri.Update(model);
    //    await _dbContext.SaveChangesAsync();
    //    return model;
    //}

    //public async Task<List<VideoTipi>> VideoTipiListesiGetir(int dilId)
    //{
    //    IQueryable<VideoTipi> query = _dbContext.VideoTipleri.AsNoTracking();

    //    if (dilId >= 0)
    //    {
    //        query = query.Where(x => x.DilId == dilId);
    //    }

    //    return await query.ToListAsync();
    //}

    //public async Task<VideoTipi> VideoTipiGetir(string id)
    //{
    //    return await _dbContext.VideoTipleri.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    //}
}