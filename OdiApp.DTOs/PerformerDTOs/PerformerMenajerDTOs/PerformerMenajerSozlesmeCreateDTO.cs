namespace OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;

public class PerformerMenajerSozlesmeCreateDTO
{
    public string MenajerId { get; set; }
    public string PerformerId { get; set; }
    public DateTime SozleşmeImzaTarihi { get; set; }
    public int SozlesmeSuresi { get; set; }
    public DateTime SozlesmeBitisTarihi { get; set; }
    public string SozlesmeDosyasi { get; set; }
}