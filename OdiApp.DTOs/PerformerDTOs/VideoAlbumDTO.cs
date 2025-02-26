namespace OdiApp.DTOs.PerformerDTOs;

public class VideoAlbumDTO
{
    public int VideoAlbumId { get; set; }
    public int VideoAlbumTipiId { get; set; }
    public string VideoAlbumTipi { get; set; }
    public string KullaniciId { get; set; }
    public string AlbumAdi { get; set; }

    public List<VideoAlbumVideoDTO>? Videolar { get; set; }
}
