using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.YetenekModels;

[Table("YetenekTipleri")]
public class YetenekTipi : BaseModel
{
    public string YetenekTipiKodu { get; set; }
    public string Tip { get; set; }
    public string? Aciklama { get; set; }
    public int Sira { get; set; }
    public int DilId { get; set; }
}