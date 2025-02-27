namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackTarihSaatCreateDTO
    {
        public string ProjeId { get; set; }
        public List<DateTime> YeniCallbackTarihleri { get; set; }
        public List<CallbackSaatCreateDTO> YeniCallbackSaatleri { get; set; }
    }
}
