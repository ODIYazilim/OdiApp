namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;

public class TopluProfilVideoAlbumDTO
{
    public string KullaniciId { get; set; }
    public string KullaniciAdSoyad { get; set; }
    public string KullaniciEmail { get; set; }
    public string KullaniciTelefon { get; set; }
    public List<ProfilVideoAlbumDTO> Albumler { get; set; }
}
