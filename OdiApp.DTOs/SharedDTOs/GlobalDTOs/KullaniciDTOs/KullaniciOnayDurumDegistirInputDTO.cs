namespace OdiApp.DTOs.SharedDTOs.GlobalDTOs.KullaniciDTOs
{
    public class KullaniciOnayDurumDegistirInputDTO
    {
        public string KullaniciId { get; set; }
        public string? OnaylayanId { get; set; }
        public bool? OnayliKullanici { get; set; }
        public bool? OnayBekliyor { get; set; }
        public bool? Reddedildi { get; set; }
    }
}