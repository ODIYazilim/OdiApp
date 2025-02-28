namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class PaketSatinAlmaOdemeOutputDTO
    {
        public string IslemId { get; set; } //AbonelikPaketiSatinAlmalari Id
        public string KullaniciId { get; set; }
        public string KullaniciAbonelikUrunuAdi { get; set; } //tablodaki KullaniciAbonelikUrunuId ve AbonelikTipi kullanılarak, ilgili tablolardan alınacak
        public string OdemeAlinanMiktar { get; set; }
        public DateTime? SatinAlmaTarihi { get; set; }  //EklenmeTarihi
        public string CardType { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public string BinNumber { get; set; }
        public string LastFourDigits { get; set; }
    }
}