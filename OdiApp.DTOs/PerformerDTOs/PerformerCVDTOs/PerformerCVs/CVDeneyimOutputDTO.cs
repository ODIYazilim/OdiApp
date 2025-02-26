namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class CVDeneyimOutputDTO
{
    public string CVDeneyimId { get; set; }
    public string DeneyimKodu { get; set; }
    public string DeneyimAdi { get; set; }

    public List<CVDeneyimDetayOutputDTO> Detaylar { get; set; }
}
