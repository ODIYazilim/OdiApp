using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerPuanModels;

public class PerformerPuan : StringBaseModel
{
    public string PerformerId { get; set; }
    public int İlgiCekicilikPuani { get; set; }
    public int YetenekPuani { get; set; }
    public int BasariPuani { get; set; }
    public string OyVerenId { get; set; }
    public string OyVerenKayitGrubu { get; set; }
    public string OyVerenKayitTuru { get; set; }
}