namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class CVUpdateDTO
{
    public string PerformerId { get; set; }
    public List<CVDataInputDTO> DataList { get; set; }
}