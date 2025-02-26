using OdiApp.EntityLayer.Base;
namespace OdiApp.EntityLayer.PerformerModels.Deneyimler;

public class DeneyimDetay : BaseModel
{
    public string DeneyimKodu { get; set; }
    public string FormAlaniKodu { get; set; }
    public int Sira { get; set; }
    public bool Aktif { get; set; }
}