using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.Egitim;

[Table("OkulBolumler")]
public class OkulBolum : BaseModel
{
    public int OkulId { get; set; }
    public string? Bolum { get; set; }
    public int Sira { get; set; }
}