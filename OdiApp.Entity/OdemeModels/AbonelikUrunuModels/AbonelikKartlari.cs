using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels
{
    public class AbonelikKartlari : StringBaseModel
    {
        public int BinNumber { get; set; } // alınan kart numarasının ilk 6 rakamı
        public int LastFourDigit { get; set; }
        public DateTime KartKaydiTarihi { get; set; }
        public bool Aktif { get; set; }
    }
}