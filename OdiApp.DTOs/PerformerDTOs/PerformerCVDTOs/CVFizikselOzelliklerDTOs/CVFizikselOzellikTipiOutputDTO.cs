namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.CVFizikselOzelliklerDTOs;

public class CVFizikselOzellikTipiOutputDTO
{
    public string FizikselOzellikTipAdi { get; set; }
    public string FizikselOzellikTipKodu { get; set; }
    public List<CVFizikselOzellikOutputDTO> Liste { get; set; }
}