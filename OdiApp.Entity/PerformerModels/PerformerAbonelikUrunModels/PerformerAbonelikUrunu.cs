using OdiApp.EntityLayer.Base;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;

public class PerformerAbonelikUrunu : AbonelikUrunu
{
    public int FotografSayisi { get; set; }
    public int TanitimVideosuSayisi { get; set; }
    public int ShowreelSayisi { get; set; }
    public int PerformansVideosuSayisi { get; set; }
    public bool SunumKartiOlusturmaIzni { get; set; }
    public bool SunumKartiPaylasmaIzni { get; set; }
    public bool DisSesEklemeIzni { get; set; }
}