using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;

public class PerformerMenajerSozlesmeOutputDTO
{
    public string PerformerMenajerSozlesmeId { get; set; }
    public string MenajerId { get; set; }
    public string MenajerAdSoyad { get; set; }
    public string PerformerId { get; set; }
    public DateTime SozleşmeImzaTarihi { get; set; }
    public int SozlesmeSuresi { get; set; }
    public DateTime SozlesmeBitisTarihi { get; set; }
    public string SozlesmeDosyasi { get; set; }
    public string SozlesmeyiEkleyenId { get; set; }
    public string SozlesmeyiEkleyenAdSoyad { get; set; }
    public KullaniciBilgileriDTO Performer { get; set; }
}