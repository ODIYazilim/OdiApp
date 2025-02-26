using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.SesRengiModels;

public class SesRengi : BaseModel
{
    public int DilId { get; set; }
    public string SesRengiKodu { get; set; }
    public string SesRengiAdi { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
}