namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class CVFormAlanlariDTO
{
    public string KayitTuruKodu { get; set; }
    public string AlanAdi { get; set; }
    public string AlanKodu { get; set; }
    public string AlanTipi { get; set; } //Fiziksel ozellik, Kisisel Bilgiler vb
    public string DataType { get; set; }//select,int,date,string vb,
    public string BagliAlanKodu { get; set; }
    public string BagliAlanKoduAdi { get; set; }
    public int Sira { get; set; }

    public object Deger { get; set; }//select ise deger olarak kod yazılacak
    public object Deger2 { get; set; }//select ise deger olarak kod yazılacak
    public string DefaultDeger { get; set; }
    public List<CVFormAlanlariSelectModel>? SelectModel { get; set; }
}

public class CVFormAlanlariSelectModel
{
    public string OzellikAdi { get; set; }
    public string OzellikKodu { get; set; }
    public int Sira { get; set; }
}
