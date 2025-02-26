namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class CVCreateDTO
{
    public string PerformerId { get; set; }
    public List<CVDataInputDTO> DataList { get; set; }
}