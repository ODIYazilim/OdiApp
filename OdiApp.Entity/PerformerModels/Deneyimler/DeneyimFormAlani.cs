using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.Deneyimler;

public class DeneyimFormAlani : BaseModel
{
    public string AlanAdi { get; set; }
    public string AlanKodu { get; set; }
    public string DataType { get; set; }
    public string KarakterSiniri { get; set; }
    public int DilId { get; set; }
    public bool Aktif { get; set; }
    public int Sira { get; set; }
}