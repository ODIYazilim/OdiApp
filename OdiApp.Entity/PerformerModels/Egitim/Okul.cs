using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.Egitim;

[Table("Okullar")]
public class Okul : BaseModel
{
    public int EgitimTipiId { get; set; }
    public string? OkulAdi { get; set; }
    public string? Aciklama { get; set; }
    public int Sira { get; set; }
    public List<OkulBolum>? Bolumler { get; set; }
}