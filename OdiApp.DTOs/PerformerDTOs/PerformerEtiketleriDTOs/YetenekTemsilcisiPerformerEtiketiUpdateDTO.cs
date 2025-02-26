namespace OdiApp.DTOs.PerformerDTOs.PerformerEtiketleriDTOs;

public class YetenekTemsilcisiPerformerEtiketiUpdateDTO
{
    public string PerformerId { get; set; }
    public string YetenekTemsilcisiId { get; set; }
    public List<YetenekTemsilcisiPerformerEtiketiUpdateItemDTO>? Etiketler { get; set; }
}