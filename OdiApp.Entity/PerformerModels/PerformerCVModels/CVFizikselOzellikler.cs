using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class CVFizikselOzellik : StringBaseModel
{
    public string PerformerCVId { get; set; }
    public string FizikselOzellikKodu { get; set; }
    public string FizikselOzellikTipKodu { get; set; }

}