using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;

public class ProfilOnayOutputDTO
{
    public string ProfilOnayId { get; set; }
    public string PerformerId { get; set; }
    public string YetenekTemsilcisiId { get; set; }
    public DateTime OnayGonderimTarihi { get; set; }
    public bool Onay { get; set; }
    public DateTime? OnaylanmaTarihi { get; set; }
    public string? OnaylayanId { get; set; }
    public bool Red { get; set; }
    public string? RedSebebiMetni { get; set; }
    public DateTime? RedTarihi { get; set; }
    public string? ReddedenId { get; set; }
    public bool Incelemede { get; set; }
    public string? InceleyenId { get; set; }
    public DateTime? IncelemeTarihi { get; set; }
    public bool Aktif { get; set; }
    public DateTime? SonuclamaTarihi { get; set; }
    public bool OnayliPasif { get; set; } //Onaylı fakat son 3 aydır giriş yapılmamış
    public KullaniciBilgileriDTO Performer { get; set; }
    public List<ProfilOnayRedNedeniOutputDTO> RedNedeniListesi { get; set; }
}