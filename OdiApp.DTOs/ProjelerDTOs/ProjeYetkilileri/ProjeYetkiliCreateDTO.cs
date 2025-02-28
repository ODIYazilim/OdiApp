namespace OdiApp.DTOs.ProjelerDTOs.ProjeYetkilileri
{
    public class ProjeYetkiliCreateDTO
    {
        public int YetkiliTipi { get; set; }
        public string ProjeId { get; set; }
        public string? YetkiliId { get; set; }
        public string? YetkiliAdi { get; set; }
    }
}