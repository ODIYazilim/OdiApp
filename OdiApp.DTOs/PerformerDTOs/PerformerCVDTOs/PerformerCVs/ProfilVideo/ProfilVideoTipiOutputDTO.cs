namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs.ProfilVideo;

public class ProfilVideoTipiOutputDTO
{
    public string VideoTipi { get; set; }
    public string VideoTipiKodu { get; set; }
    public int Sira { get; set; }
    public int PremiumVideoLimit { get; set; }
    public int NormalVideoLimit { get; set; }
    public List<string> OnerilenEtiketler { get; set; }
}