namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class PaketAbonelikOdemeOutputDTO
    {
        public string IslemId { get; set; } //AbonelikPaketiAboneOlmalari Id
        public string KullaniciId { get; set; }
        public string KullaniciAbonelikUrunuAdi { get; set; } //tablodaki KullaniciAbonelikUrunuId ve AbonelikTipi kullanılarak, ilgili tablolardan alınacak
        public string OdenenFiyat { get; set; }
        public int OdemePeriyodu { get; set; }
        public DateTime? AbonelikBaslangicTarihi { get; set; }  //EklenmeTarihi
        public string KartBinNumber { get; set; }  //EklenmeTarihi
        public string KartLastFourDigit { get; set; }
    }
}