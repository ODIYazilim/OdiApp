using OdiApp.EntityLayer.PerformerModels.FotografAlbum;

namespace OdiApp.DataAccessLayer.PerformerDataServices.FotografAlbum;

public interface IFotoAlbumDataService
{
    Task<FotoAlbum> YeniFotoAlbum(FotoAlbum album);
    Task<FotoAlbum> FotoAlbumGuncelle(FotoAlbum album);
    Task<FotoAlbum> FotoAlbumGetir(int fotoAlbumId);
    Task<List<FotoAlbum>> FotoAlbumListe(string kullaniciId);
    Task<bool> FotoAlbumSil(int albumId);
    //
    Task<FotoAlbumFotograf> YeniAlbumFotografi(FotoAlbumFotograf foto);
    Task<FotoAlbumFotograf> AlbumFotografiGuncelle(FotoAlbumFotograf foto);
    Task<bool> AlbumFotografiSil(int fotoId);
    Task<List<FotoAlbumFotograf>> AlbumFotograflari(int albumId);
}