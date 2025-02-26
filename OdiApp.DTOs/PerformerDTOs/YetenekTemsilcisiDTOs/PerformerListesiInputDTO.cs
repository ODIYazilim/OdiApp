namespace OdiApp.DTOs.PerformerDTOs.YetenekTemsilcisiDTOs;

public class PerformerListesiInputDTO
{
    public string YetenekTemsilcisiId { get; set; } //Menajerin ID'si
    public PerformerListelemeTipi PerformerListelemeTipi { get; set; } //Hangi performer listesi getirileceğini belirler
    public int Page { get; set; } = 1; // Sayfa numarası
    public int Limit { get; set; } = 10; // Sayfa başına kayıt limiti
}

public enum PerformerListelemeTipi
{
    Hepsi = 0,
    Onaylanan = 1,
    OnaylananPasif = 2,
    DegisiklikYapan = 3,
    OnayBekleyen = 4,
    Reddedilen = 5,
    EksikProfil = 6,
    Dondurulan = 7,
    Engellenen = 8,
    Sozlesmeliler = 9
}