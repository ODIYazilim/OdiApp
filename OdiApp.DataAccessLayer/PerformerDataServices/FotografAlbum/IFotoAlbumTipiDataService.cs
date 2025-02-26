using OdiApp.EntityLayer.PerformerModels.FotografAlbum;

namespace OdiApp.DataAccessLayer.PerformerDataServices.FotografAlbum;

public interface IFotoAlbumTipiDataService
{
    //Task<List<FotoAlbumTipiLabel>> FotoAlbumTipiListe(int dilId);
    Task<List<FotoAlbumTipi>> FotoAlbumTipiListe();
}