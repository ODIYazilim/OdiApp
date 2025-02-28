namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class AbonelikPaketiSatinAlmaInputDTO
    {
        public string PlanReferanceCode { get; set; }
        public string KullaniciId { get; set; }
        public int AbonelikTipi { get; set; }
        public string KullaniciAbonelikUrunId { get; set; }
        public IyzicoSatinAlmaInputDTO SatinAlmaBilgileri { get; set; }
    }
}