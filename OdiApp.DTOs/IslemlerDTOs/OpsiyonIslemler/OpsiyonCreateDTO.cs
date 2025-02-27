namespace OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler
{
    public class OpsiyonCreateDTO
    {
        public string OpsiyonListesiId { get; set; }
        public string OpsiyonGonderenId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string MenajerId { get; set; }
        public string PerformerId { get; set; }

        public DateTime CekimBaslagicTarihi { get; set; }
        public DateTime CekimBitisTarihi { get; set; }

        public int ProjeButcesi { get; set; }
        public int OdemeSuresi { get; set; }
        public List<OpsiyonAnketSorulariCreateDTO> AnketSorulari { get; set; }
    }
}
