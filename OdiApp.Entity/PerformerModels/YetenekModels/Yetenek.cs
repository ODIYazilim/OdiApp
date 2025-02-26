using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.YetenekModels;

[Table("Yetenekler")]
public class Yetenek : BaseModel
{
    public string YetenekTipiKodu { get; set; }
    public string YetenekKodu { get; set; }
    public string YetenekAdi { get; set; }
    public int Sira { get; set; }
    public int DilId { get; set; }
}