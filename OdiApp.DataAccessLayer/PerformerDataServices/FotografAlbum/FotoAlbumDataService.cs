using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.FotografAlbum;

namespace OdiApp.DataAccessLayer.PerformerDataServices.FotografAlbum;

public class FotoAlbumDataService : IFotoAlbumDataService
{
    ApplicationDbContext _dbContext;
    public FotoAlbumDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region FOTOĞRAF ALBUMU

    public async Task<FotoAlbum> YeniFotoAlbum(FotoAlbum album)
    {
        await _dbContext.FotoAlbumleri.AddAsync(album);
        await _dbContext.SaveChangesAsync();
        album = await _dbContext.FotoAlbumleri.Include(x => x.Fotograflar).Include(x => x.AlbumTipi).FirstOrDefaultAsync(x => x.Id == album.Id);
        return album;
    }

    public async Task<FotoAlbum> FotoAlbumGuncelle(FotoAlbum album)
    {
        _dbContext.FotoAlbumleri.Update(album);
        await _dbContext.SaveChangesAsync();
        album = await _dbContext.FotoAlbumleri.Include(x => x.Fotograflar).Include(x => x.AlbumTipi).FirstOrDefaultAsync(x => x.Id == album.Id);
        return album;
    }

    public async Task<FotoAlbum> FotoAlbumGetir(int fotoAlbumId)
    {
        FotoAlbum album = await _dbContext.FotoAlbumleri.Include(x => x.Fotograflar).Include(x => x.AlbumTipi).FirstOrDefaultAsync(x => x.Id == fotoAlbumId);
        return album;
    }

    public async Task<List<FotoAlbum>> FotoAlbumListe(string kullaniciId)
    {
        return await _dbContext.FotoAlbumleri.Include(x => x.Fotograflar).Include(x => x.AlbumTipi).Where(x => x.KullaniciId == kullaniciId).OrderBy(x => x.AlbumTipi.Sira).ToListAsync();
    }

    public async Task<bool> FotoAlbumSil(int albumId)
    {
        FotoAlbum album = await _dbContext.FotoAlbumleri.FirstOrDefaultAsync(x => x.Id == albumId);
        if (album == null) return false;

        _dbContext.FotoAlbumleri.Remove(album);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    #endregion

    #region FOTOĞRAFLAR

    public async Task<FotoAlbumFotograf> YeniAlbumFotografi(FotoAlbumFotograf foto)
    {
        await _dbContext.FotoAlbumFotograflar.AddAsync(foto);
        await _dbContext.SaveChangesAsync();
        return foto;
    }

    public async Task<FotoAlbumFotograf> AlbumFotografiGuncelle(FotoAlbumFotograf foto)
    {
        _dbContext.FotoAlbumFotograflar.Update(foto);
        await _dbContext.SaveChangesAsync();
        return foto;
    }

    public async Task<List<FotoAlbumFotograf>> AlbumFotograflari(int albumId)
    {
        return await _dbContext.FotoAlbumFotograflar.Where(x => x.AlbumId == albumId).ToListAsync();
    }

    public async Task<bool> AlbumFotografiSil(int fotoId)
    {
        FotoAlbumFotograf foto = await _dbContext.FotoAlbumFotograflar.FirstOrDefaultAsync(x => x.Id == fotoId);
        if (foto == null) return false;
        _dbContext.FotoAlbumFotograflar.Remove(foto);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    #endregion
}
