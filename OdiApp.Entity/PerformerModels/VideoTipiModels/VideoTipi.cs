using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.VideoTipiModels;

public class VideoTipi : BaseModel
{
    public string TipAdi { get; set; }
    public string TipKodu { get; set; }
    public int DilId { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }

    // Premium kullanıcıların video ekleme sınırı
    public int PremiumVideoLimit { get; set; }

    // Normal kullanıcıların video ekleme sınırı
    public int NormalVideoLimit { get; set; }

    public string OnerilenEtiketler { get; set; }
}