namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class AbonelikUrunuOdemePlaniOutputDTO
    {
        public string AbonelikUrunuOdemePlaniId { get; set; }
        public string AbonelikUrunId { get; set; }
        public string ReferenceCode { get; set; }
        public int AbonelikTipi { get; set; }
        public int OdemePeriodu { get; set; }
        public int AbonelikFiyati { get; set; }
        public int DenemeGunSayisi { get; set; }
        public bool Aktif { get; set; }
        public string KullaniciAbonelikUrunId { get; set; }
        public object KullaniciAbonelikUrunu { get; set; }
    }
}