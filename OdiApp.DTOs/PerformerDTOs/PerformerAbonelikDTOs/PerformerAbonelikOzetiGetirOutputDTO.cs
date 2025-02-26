namespace OdiApp.DTOs.PerformerDTOs.PerformerAbonelikDTOs;

public class PerformerAbonelikOzetiGetirOutputDTO
{
    public string UrunAdi { get; set; }
    public int OdemePeriodu { get; set; }
    public int FotografSayisi { get; set; }
    public int TanitimVideosuSayisi { get; set; }
    public int ShowreelSayisi { get; set; }
    public int PerformansVideosuSayisi { get; set; }
    public int KalanFotografSayisi { get; set; }
    public int KalanTanitimVideosuSayisi { get; set; }
    public int KalanShowreelSayisi { get; set; }
    public int KalanPerformerVideosuSayisi { get; set; }
    public DateTime AbonelikBaslangicTarihi { get; set; }
    public DateTime AbonelikBitisTarihi { get; set; }
    public string KartBinNumber { get; set; }
    public string KartLastFourDigit { get; set; }
    public string KullaniciAbonelikId { get; set; }
    public string KullaniciAbonelikUrunId { get; set; }
    public string KullaniciReferanceCode { get; set; }
    public string PlanReferanceCode { get; set; }
    public string AbonelikReferenceCode { get; set; }
}