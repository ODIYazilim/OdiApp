using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

[Table("FizikselOzellikler")]
public class FizikselOzellik : BaseModel
{
    public string FizikselOzellikAdi { get; set; }
    public string FizikselOzellikTipKodu { get; set; }//4 karakter
    public string FizikselOzellikKodu { get; set; } //4 karakter
    public int DilId { get; set; }
    public bool Durum { get; set; }
    public int Sira { get; set; }
}