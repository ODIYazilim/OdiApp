namespace OdiApp.DTOs.PerformerDTOs;

public class VideoAlbumVideoDTO
{
    public int VideoId { get; set; }
    public int AlbumId { get; set; }
    public string VideoAdi { get; set; }
    public string DosyaYolu { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
    public string VideoDili { get; set; }
    public string Tag { get; set; }
}