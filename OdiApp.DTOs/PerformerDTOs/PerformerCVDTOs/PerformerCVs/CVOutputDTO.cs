namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class CVOutputDTO
{
    public string CVId { get; set; }
    public string PerformerId { get; set; }
    public bool MenajerGordu { get; set; }
    public DateTime? MenajerGorduTarih { get; set; }
    public string? MenajerId { get; set; }

    public List<CVFormAlanlariDTO> cVFormAlanlariDTOs { get; set; }
}