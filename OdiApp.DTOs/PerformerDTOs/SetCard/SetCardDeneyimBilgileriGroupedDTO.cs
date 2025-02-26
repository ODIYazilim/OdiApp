namespace OdiApp.DTOs.PerformerDTOs.SetCard;

public class SetCardDeneyimBilgileriGroupedDTO
{
    public string DeneyimAdi { get; set; }
    public string CVDeneyimId { get; set; }
    public List<SetCardDeneyimBilgileriGroupedOzellikDTO> Ozellikler { get; set; }
}