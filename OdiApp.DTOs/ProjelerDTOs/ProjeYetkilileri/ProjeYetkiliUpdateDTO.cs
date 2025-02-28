namespace OdiApp.DTOs.ProjelerDTOs.ProjeYetkilileri
{
    public class ProjeYetkiliUpdateDTO
    {
        public string ProjeYetkiliId { get; set; }
        public int YetkiliTipi { get; set; }
        public string ProjeId { get; set; }
        public string? YetkiliId { get; set; }
        public string? YetkiliAdi { get; set; }
    }
}