using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels
{
    public class AbonelikPaketiSatinAlma : StringBaseModel
    {
        public string OdemeAlinanMiktar { get; set; } //iyzicoda müşteriden çekilen fiyat
        public string PaymentId { get; set; }
        public string CardType { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public string BinNumber { get; set; }
        public string LastFourDigits { get; set; }
        public int AbonelikTuru { get; set; }
        public string KullaniciAbonelikUrunId { get; set; }
        public string KullaniciAbonelikId { get; set; } //globaldeki Yapimabonelikleri tablosuna kayıti id si
        public string KullaniciId { get; set; }
    }
}