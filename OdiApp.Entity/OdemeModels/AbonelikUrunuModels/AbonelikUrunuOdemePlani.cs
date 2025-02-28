using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels
{
    public class AbonelikUrunuOdemePlani : StringBaseModel
    {
        public string AbonelikUrunId { get; set; }
        public string ProductReferenceCode { get; set; }
        public string ReferenceCode { get; set; }
        public int AbonelikTipi { get; set; }
        public int OdemePeriodu { get; set; }
        public decimal AbonelikFiyati { get; set; }
        public int DenemeGunSayisi { get; set; }
        public bool Aktif { get; set; }
        public string KullaniciAbonelikUrunId { get; set; }
    }
}