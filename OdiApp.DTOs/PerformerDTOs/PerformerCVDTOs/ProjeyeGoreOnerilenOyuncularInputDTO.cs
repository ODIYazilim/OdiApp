namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs;

public class ProjeyeGoreOnerilenOyuncularInputDTO
{
    public string ProjeRolId { get; set; }
    public int Page { get; set; } = 1; // Sayfa numarası
    public int Limit { get; set; } = 10; // Sayfa başına kayıt limiti
}