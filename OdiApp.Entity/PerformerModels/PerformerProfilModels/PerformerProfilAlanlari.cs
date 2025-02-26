using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;

public class PerformerProfilAlanlari : BaseModel
{
    public string PerfomerKayitTuru { get; set; }
    public string ProfilAlanAdi { get; set; } //PerformerProfilAlanAdlari
    public bool KullanimDurumu { get; set; }
    public bool Zorunlu { get; set; }
    public bool Aktif { get; set; }
}