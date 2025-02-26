using OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public interface IFotoAlbumLogicService
{
    Task<OdiResponse<List<FotoAlbumTipiDTO>>> FotoAlbumTipiListesi();
    Task<OdiResponse<FotoAlbumDTO>> YeniFotoAlbum(FotoAlbumDTO album, OdiUser user);
    Task<OdiResponse<FotoAlbumDTO>> FotoAlbumGuncel(FotoAlbumDTO album, OdiUser user);
    Task<OdiResponse<NoContent>> FotoAlbumSil(FotoAlbumIdDTO fotoAlbumId);
    Task<OdiResponse<List<FotoAlbumDTO>>> FotoAlbumListesi(KullaniciIdDTO kullaniciId);
    Task<OdiResponse<List<TopluFotoAlbumDTO>>> TopluFotoAlbumListesi(List<KullaniciIdDTO> kullaniciIdList);

    //
    Task<OdiResponse<FotoAlbumFotografDTO>> YeniAlbumFotografi(FotoAlbumFotografDTO foto, OdiUser user);
    Task<OdiResponse<FotoAlbumFotografDTO>> AlbumFotografiGuncelle(FotoAlbumFotografDTO foto, OdiUser user);
    Task<OdiResponse<NoContent>> AlbumFotografiSil(FotoAlbumFotografIdDTO fotoId);
    Task<OdiResponse<List<FotoAlbumFotografDTO>>> AlbumFotograflari(FotoAlbumIdDTO fotoAlbumId);
}