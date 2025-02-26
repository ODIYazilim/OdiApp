namespace OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;

public class PerformerMenajerSozlesmeUpdateDTO
{
    public string PerformerMenajerSozlesmeId { get; set; }
    public string MenajerId { get; set; }
    public string PerformerId { get; set; }
    public DateTime SozleşmeImzaTarihi { get; set; }
    public int SozlesmeSuresi { get; set; }
    public DateTime SozlesmeBitisTarihi { get; set; }
    public string SozlesmeDosyasi { get; set; }
}