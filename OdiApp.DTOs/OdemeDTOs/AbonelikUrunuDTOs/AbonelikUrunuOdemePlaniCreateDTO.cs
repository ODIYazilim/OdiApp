namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class AbonelikUrunuOdemePlaniCreateDTO
    {
        public string AbonelikUrunId { get; set; }
        public string ProductReferenceCode { get; set; }
        public int AbonelikTipi { get; set; }
        public int DenemeGunSayisi { get; set; }
        public object KullaniciAbonelikUrunu { get; set; }
    }
}