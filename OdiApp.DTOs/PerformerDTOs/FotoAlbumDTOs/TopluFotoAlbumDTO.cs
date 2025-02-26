namespace OdiApp.DTOs.PerformerDTOs.FotoAlbumDTOs;

public class TopluFotoAlbumDTO
{
    public string KullaniciId { get; set; }
    public string KullaniciAdSoyad { get; set; }
    public string KullaniciEmail { get; set; }
    public string KullaniciTelefon { get; set; }
    public List<FotoAlbumDTO> Albumler { get; set; }
}