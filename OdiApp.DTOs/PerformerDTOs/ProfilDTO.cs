using OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.DTOs.PerformerDTOs;

public class ProfilDTO
{
    public KullaniciBilgileriDTO PerformerBilgileri { get; set; } = new KullaniciBilgileriDTO();
    //public PerformerCVDTO PerformerCV { get; set; } = new PerformerCVDTO();
    public List<FotoAlbumDTO> FotografAlbumleri { get; set; } = new List<FotoAlbumDTO>();
    public List<VideoAlbumDTO> VideoAlbumleri { get; set; } = new List<VideoAlbumDTO>();
}
