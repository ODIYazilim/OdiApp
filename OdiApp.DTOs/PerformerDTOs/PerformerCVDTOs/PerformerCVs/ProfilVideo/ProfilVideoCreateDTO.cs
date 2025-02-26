namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;

public class ProfilVideoCreateDTO
{
    public string PerformerId { get; set; }
    public string? Dil { get; set; }
    public string VideoTipiKodu { get; set; }
    public string VideoURL { get; set; }
    public string? VideoTags { get; set; }
}
