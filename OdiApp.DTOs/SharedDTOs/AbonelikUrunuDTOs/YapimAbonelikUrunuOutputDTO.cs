namespace OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs
{
    public class YapimAbonelikUrunuOutputDTO
    {
        public string YapimAbonelikUrunuId { get; set; }
        public string UrunAdi { get; set; }
        public int ProjeSayisi { get; set; }
        public int YetenekSayisi { get; set; }
        public int OdemePeriodu { get; set; }
        public decimal Fiyat { get; set; }
        public bool IndirimVarmi { get; set; }
        public int IndirimOrani { get; set; }
        public decimal IndirimliFiyat { get; set; }
        public int KDVOrani { get; set; }
        public decimal KDVliFiyat { get; set; }
        public decimal KDVliIndirimliFiyat { get; set; }
        public bool Aktif { get; set; }
        public string ReferenceCode { get; set; }
    }
}