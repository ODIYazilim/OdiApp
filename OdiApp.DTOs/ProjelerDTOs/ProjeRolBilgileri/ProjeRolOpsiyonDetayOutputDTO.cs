using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolAnketSorusuDTO;

namespace OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri
{
    public class ProjeRolOpsiyonDetayOutputDTO
    {
        public string ProjeRolId { get; set; }
        public string RolAdi { get; set; }
        public int Butce60Gun { get; set; }
        public int PesinButce { get; set; }
        public int AlternatifButce { get; set; }
        public int OdemeSuresi { get; set; }
        public List<ProjeOpsiyonRolAnketSorusuOutputDTO>? AnketSorulari { get; set; }
    }
}