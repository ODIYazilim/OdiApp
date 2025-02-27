namespace OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler
{
    public class OpsiyonListesiCreateDTO
    {
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }
        public string ListeyeEkleyenKullaniciId { get; set; }
    }
}
