using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;

public class PerformerAbonelik : StringBaseModel
{
    public string PerformerId { get; set; }
    public string MenajerId { get; set; }
    public string AbonelikUrunId { get; set; }
    public int KalanFotografSayisi { get; set; }
    public int KalanTanitimVideosuSayisi { get; set; }
    public int KalanShowreelSayisi { get; set; }
    public int KalanPerformerVideosuSayisi { get; set; }
    public int AbonelikVeren { get; set; }
    public bool UcretsizYenile { get; set; }
    public bool Yenile { get; set; }
    public DateTime AbonelikBaslangicTarihi { get; set; }
    public DateTime AbonelikBitisTarihi { get; set; }
    public bool Aktif { get; set; }
    public int SonlanmaSebebi { get; set; }
    public DateTime? AbonelikIptalTarihi { get; set; } // Aboneliği iptal ettirilme tarihi
    public string AbonelikReferenceCode { get; set; }
    public string KullaniciReferenceCode { get; set; }
}