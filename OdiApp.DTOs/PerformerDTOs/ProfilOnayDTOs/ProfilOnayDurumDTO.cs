namespace OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;

public class ProfilOnayDurumDTO
{
    public string PerformerId { get; set; }
    public string PerformerAdSoyad { get; set; }
    public string MenajerId { get; set; }
    public string MenajerAdSoyad { get; set; }
    public int Durum { get; set; }
    // 0 => beklemede
    // 1 => incelemede
    // 3 => onaylandı
    // 4 => reddedildi
}