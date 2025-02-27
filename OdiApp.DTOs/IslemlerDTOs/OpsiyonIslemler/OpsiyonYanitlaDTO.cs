namespace OdiApp.DTOs.IslemlerDTOs.OpsiyonIslemler
{
    public class OpsiyonYanitlaDTO
    {
        public OpsiyonYanitlaDetayDTO YanitlaDetay { get; set; }
        public List<OpsiyonAnketSorulariUpdateDTO>? AnketSorulari { get; set; }
    }
}