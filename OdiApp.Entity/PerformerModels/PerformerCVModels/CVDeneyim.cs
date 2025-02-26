using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

[Table("CVDeneyimler")]
public class CVDeneyim : StringBaseModel
{
    public string CVId { get; set; }
    public string DeneyimKodu { get; set; }

    public List<CVDeneyimDetay> Detaylar { get; set; }
}