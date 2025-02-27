namespace OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler
{
    public class OpsiyonListesiViewDTO
    {
        public string OpsiyonListesiId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string PerformerId { get; set; }
        public string PerformerAdSoyad { get; set; }
        public string PerformerProfilResmi { get; set; }
        public string MenajerId { get; set; }
        public string MenajerAdSoyad { get; set; }
        public string ListeyeEkleyenKullaniciId { get; set; }
        public string ListeyeEkleyenAdSoyad { get; set; }
        public DateTime ListeyeEklenemeTarihi { get; set; }

        public OpsiyonViewDTO? Opsiyon { get; set; }

    }
}
