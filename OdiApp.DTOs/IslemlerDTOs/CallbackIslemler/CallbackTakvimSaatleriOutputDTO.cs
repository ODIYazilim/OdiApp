namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackTakvimSaatleriOutputDTO
    {
        public DateTime Tarih { get; set; }
        public string TarihLabel { get; set; }
        public List<CallbackSaatOutputDTO> CallbackSaatleri { get; set; }
    }
}
