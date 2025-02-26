namespace OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;

public class ProfilOnayRedInputDTO
{
    public string ProfilOnayId { get; set; }
    public string RedSebebiMetni { get; set; }
    public List<int>? OnTanimliRedSebepleriIdListesi { get; set; }
}