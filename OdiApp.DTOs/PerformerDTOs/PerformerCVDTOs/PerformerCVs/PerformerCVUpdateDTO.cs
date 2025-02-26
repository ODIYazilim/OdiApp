namespace OdiApp.DTOs.PerformerDTOs.PerformerCVDTOs.PerformerCVs;

public class PerformerCVUpdateDTO
{
    public string PerformerCVId { get; set; }
    public string MenajerId { get; set; }

    public string PerformerId { get; set; }
    public DateTime DOGT { get; set; } //Doğum Tarihi
    public int CBSY { get; set; } //Cast Baslangic Yasi
    public int CBTY { get; set; }//Cast Bitis Yasi

    public string YSHR { get; set; }//Yaşadığı Şehir
    public string CNSY { get; set; }//Cinsiyet
    public int BOYU { get; set; }// Boy
    public int KILO { get; set; }// Kilo
    public int AYKN { get; set; }//Ayak No
    public bool SAJM { get; set; }// Sözleşmeli Ajans Menajer
    public string UYRK { get; set; } //uyruk
    public int GOGS { get; set; }// göğüs
    public int BELI { get; set; }// Bel
    public int KALC { get; set; }// Kalça

    //Fiziksel
    public string GOZR { get; set; } //  Göz rengi
    public string SACR { get; set; }//sacrengi
    public string TENR { get; set; } //ten rengi
    public string SACS { get; set; }//sac şeşkli
    public string SKLS { get; set; }//sakal sekli
    public string BEDN { get; set; }//beden

    //ses
    public string SESR { get; set; } //Ses Rengi
    public string SESD { get; set; } //Ses Derinliği
}
