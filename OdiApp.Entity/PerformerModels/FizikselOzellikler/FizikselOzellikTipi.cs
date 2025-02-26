using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

[Table("FizikselOzellikTipleri")]
public class FizikselOzellikTipi : BaseModel
{
    public string FizikselOzellikTipAdi { get; set; }
    public string FizikselOzellikTipKodu { get; set; }//4 karakter 
    public int DilId { get; set; }
    public bool Durum { get; set; }
}