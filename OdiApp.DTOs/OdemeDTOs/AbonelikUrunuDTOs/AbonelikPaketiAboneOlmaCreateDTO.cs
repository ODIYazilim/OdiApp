namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class AbonelikPaketiAboneOlmaCreateDTO
    {
        public string PlanReferenceCode { get; set; }
        public string KullaniciId { get; set; }
        public int AbonelikTipi { get; set; }
        public string KullaniciAbonelikUrunId { get; set; }
    }
}