namespace OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma
{
    public class ProjeMesajProjeDTO
    {
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeResmi { get; set; }
        public List<ProjeMesajOutputDTO> ProjeMesajlari { get; set; }
    }
}