using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.PerformerModels.PerformerCVModels;

public class CVFormAlani : BaseModel
{
    public string KayitTuruKodu { get; set; }
    public string AlanAdi { get; set; }
    public string AlanKodu { get; set; }
    public string AlanTipi { get; set; } //Fiziksel ozellik, Kisisel Bilgiler vb
    public string DataType { get; set; }//select,int,date,string vb,
    public string BagliAlanKodu { get; set; }//slider ınt gibi bir biriyle bağlı iki alan kodu
    public string BagliAlanKoduAdi { get; set; }
    public int Sira { get; set; }
    public bool Active { get; set; }
    public string DefaultDeger { get; set; }
}