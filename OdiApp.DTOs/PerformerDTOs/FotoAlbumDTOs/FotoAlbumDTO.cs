namespace OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;

public class FotoAlbumDTO
{
    public int FotoAlbumId { get; set; }
    public int FotoAlbumTipiId { get; set; }
    public string? FotoAlbumTipi { get; set; }
    public string KullaniciId { get; set; }
    public string? AlbumAdi { get; set; }
    public bool Premium { get; set; }
    public List<FotoAlbumFotografDTO>? Fotograflar { get; set; }
}