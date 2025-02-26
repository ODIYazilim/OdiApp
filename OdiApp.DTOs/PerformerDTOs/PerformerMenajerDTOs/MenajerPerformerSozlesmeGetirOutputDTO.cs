namespace OdiApp.DTOs.PerformerDTOs.PerformerMenajerDTOs;

public class MenajerPerformerSozlesmeGetirOutputDTO
{
    public string PerformerMenajerSozlesmeId { get; set; }
    public string MenajerId { get; set; }
    public string MenajerAdSoyad { get; set; }
    public string MenajerProfilFotografi { get; set; }
    public string MenajerTelefonNumarasi { get; set; }
    public string MenajerEmail { get; set; }
    public DateTime SozleşmeImzaTarihi { get; set; }
    public DateTime SozlesmeBitisTarihi { get; set; }
    public int SozlesmeSuresi { get; set; }
    public int KalanGun { get; set; }
    public string SozlesmeDosyasi { get; set; }
}