using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;

public class ProfilOnayTalepDTO
{
    public string ProfilOnayId { get; set; }
    public string PerformerId { get; set; }
    public bool Firma { get; set; }
    public string YetenekTemsilcisiId { get; set; }
    public string YetenekTemsilcisiAdi { get; set; }
    public DateTime OnayGonderimTarihi { get; set; }
    public KullaniciBilgileriDTO Performer { get; set; }
}