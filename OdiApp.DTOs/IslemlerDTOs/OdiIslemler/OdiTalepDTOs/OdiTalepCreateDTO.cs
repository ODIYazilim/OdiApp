namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs
{
    public class OdiTalepCreateDTO
    {
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string TalepGonderenId { get; set; }
        public string TalepGonderilenPerformerId { get; set; }
        public string TalepGonderilenMenajerId { get; set; }
    }
}
