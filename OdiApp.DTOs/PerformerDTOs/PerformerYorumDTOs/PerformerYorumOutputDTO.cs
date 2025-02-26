namespace OdiApp.DTOs.PerformerDTOs.PerformerYorumDTOs;

public class PerformerYorumOutputDTO
{
    public string PerformerYorumId { get; set; }
    public string YorumYapanId { get; set; }
    public string YorumYapanAdSoyad { get; set; }
    public string YorumYapanProfilFotografi { get; set; }
    public List<string> YorumYapanKayitTuruListesi { get; set; }
    public string PerformerId { get; set; }
    public string YorumMetni { get; set; }
    public DateTime EklenmeTarihi { get; set; }
}