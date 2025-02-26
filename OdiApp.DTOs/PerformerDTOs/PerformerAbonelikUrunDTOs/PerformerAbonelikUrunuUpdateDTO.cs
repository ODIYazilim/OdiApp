namespace OdiApp.DTOs.PerformerDTOs.PerformerAbonelikUrunDTOs;

public class PerformerAbonelikUrunuUpdateDTO
{
    public string PerformerAbonelikUrunuId { get; set; }
    public string UrunAdi { get; set; }
    public int FotografSayisi { get; set; }
    public int TanitimVideosuSayisi { get; set; }
    public int ShowreelSayisi { get; set; }
    public int PerformansVideosuSayisi { get; set; }
    public bool SunumKartiOlusturmaIzni { get; set; }
    public bool SunumKartiPaylasmaIzni { get; set; }
    public bool DisSesEklemeIzni { get; set; }
    public int OdemePeriodu { get; set; }
    public decimal Fiyat { get; set; }
    public bool IndirimVarmi { get; set; }
    public int IndirimOrani { get; set; }
    public decimal IndirimliFiyat { get; set; }
    public int KDVOrani { get; set; }
    public decimal KDVliFiyat { get; set; }
    public decimal KDVliIndirimliFiyat { get; set; }
}
