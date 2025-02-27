namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackCreateDTO
    {
        public string OpsiyonId { get; set; }
        public string CallbackSaatId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }

        public DateTime CallbackGonderimTarihi { get; set; }

    }
}
