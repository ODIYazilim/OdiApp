namespace OdiApp.DTOs.PerformerDTOs.PerformerPuanDTOs;

public class PerformerPuanListOutputDTO
{
    public string PerformerPuanId { get; set; }
    public string PerformerId { get; set; }
    public string PerformerAdSoyad { get; set; }
    public int İlgiCekicilikPuani { get; set; }
    public int YetenekPuani { get; set; }
    public int BasariPuani { get; set; }
    public string OyVerenId { get; set; }
    public string OyVerenAdSoyad { get; set; }
    public string OyVerenKayitGrubu { get; set; }
    public string OyVerenKayitTuru { get; set; }
}