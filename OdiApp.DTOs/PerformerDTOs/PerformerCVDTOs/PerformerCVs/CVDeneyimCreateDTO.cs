namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class CVDeneyimCreateDTO
{
    public string CVId { get; set; }
    public string DeneyimKodu { get; set; }

    public List<CVDeneyimDetayCreateDTO> Detaylar { get; set; }
}
