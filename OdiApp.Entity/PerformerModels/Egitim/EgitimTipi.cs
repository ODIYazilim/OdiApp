using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.Egitim;

[Table("EgitimTipleri")]
public class EgitimTipi : BaseModel
{
    public string Tip { get; set; }
    public string? Aciklama { get; set; }
    public int Sira { get; set; }
    public List<Okul>? Okullar { get; set; }
}