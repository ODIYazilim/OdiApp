namespace OdiApp.DTOs.IslemlerDTOs.RolSendeDTOs
{
    public class RolSendeCreateDTO
    {
        public string ProjeId { get; set; }
        public string RolId { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }
        public string OnaylayanId { get; set; }
        public DateTime RolSendeTarihi { get; set; }
    }
}