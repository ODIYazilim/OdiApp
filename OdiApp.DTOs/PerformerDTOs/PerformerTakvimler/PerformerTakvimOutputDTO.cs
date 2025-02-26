namespace OdiApp.DTOs.PerformerDTOs.PerformerTakvimler;

public class PerformerTakvimOutputDTO
{
    public string PerformerTakvimId { get; set; }
    public string PerformerId { get; set; }
    public DateTime BaslangicTarihi { get; set; }
    public DateTime BitisTarihi { get; set; }
    public string DolulukAciklamasi { get; set; }
    public int DolulukTuru { get; set; }
}
