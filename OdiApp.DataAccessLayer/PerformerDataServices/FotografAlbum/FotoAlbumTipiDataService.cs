using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.FotografAlbum;

namespace OdiApp.DataAccessLayer.PerformerDataServices.FotografAlbum;

public class FotoAlbumTipiDataService : IFotoAlbumTipiDataService
{
    ApplicationDbContext _dbContext;

    public FotoAlbumTipiDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public async Task<List<FotoAlbumTipiLabel>> FotoAlbumTipiListe(int dilId)
    //{
    //    return await _dbContext.FotoAlbumTipiLabels.Where(x => x.DilId == dilId).OrderBy(o => o.Sira).ToListAsync();
    //}

    public async Task<List<FotoAlbumTipi>> FotoAlbumTipiListe()
    {
        return await _dbContext.FotoAlbumTipleri.Where(x => x.Aktif).OrderBy(o => o.Sira).ToListAsync();
    }
}