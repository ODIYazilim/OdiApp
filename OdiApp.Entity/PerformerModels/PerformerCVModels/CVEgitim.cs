using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

[Table("CVEgitimler")]
public class CVEgitim : StringBaseModel
{
    public string CVId { get; set; }
    public int EgitimTipiId { get; set; }
    public int OkulId { get; set; }
    public int BolumId { get; set; }
    public string Yil { get; set; }
}