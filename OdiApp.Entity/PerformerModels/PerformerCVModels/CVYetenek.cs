using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

[Table("CVYetenekleri")]
public class CVYetenek : StringBaseModel
{
    public string CVId { get; set; }
    public string YetenekTipiKodu { get; set; }
    public string YetenekKodu { get; set; }
    public int Derece { get; set; }
}