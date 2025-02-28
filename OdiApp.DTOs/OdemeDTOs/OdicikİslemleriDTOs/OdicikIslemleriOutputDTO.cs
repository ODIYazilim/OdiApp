namespace OdiApp.DTOs.OdemeDTOs.OdicikİslemleriDTOs
{
    public class OdicikIslemleriOutputDTO
    {
        public string OdicikIslemleriId { get; set; }
        public string KullaniciId { get; set; }
        public int OdicikMiktari { get; set; }
        public string IslemTipi { get; set; } //örn; giden, gelen
        public string IslemKodu { get; set; } //örn; proje, callback
        public string Aciklama { get; set; }
        public DateTime? EklenmeTarihi { get; set; }
    }
}