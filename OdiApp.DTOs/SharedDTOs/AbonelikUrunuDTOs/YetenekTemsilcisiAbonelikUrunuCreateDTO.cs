namespace OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs
{
    public class YetenekTemsilcisiAbonelikUrunuCreateDTO
    {
        public string UrunAdi { get; set; }
        public int YetenekSayisi { get; set; }
        public int PremiumProfilSayisi { get; set; }
        public int KomisyonKesintisi { get; set; }
        public int OdemePeriodu { get; set; }
        public decimal Fiyat { get; set; }
        public bool IndirimVarmi { get; set; }
        public int IndirimOrani { get; set; }
        public decimal IndirimliFiyat { get; set; }
        public int KDVOrani { get; set; }
        public decimal KDVliFiyat { get; set; }
        public decimal KDVliIndirimliFiyat { get; set; }
        public string ReferenceCode { get; set; }
    }
}