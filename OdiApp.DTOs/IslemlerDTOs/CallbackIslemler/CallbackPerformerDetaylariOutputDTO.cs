namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackPerformerDetaylariOutputDTO
    {
        public string CallbackNotu { get; set; }
        public string Senaryo { get; set; }
        public string GorusmeYeri { get; set; }
        public string GorusmeAdresi { get; set; }
        public CallbackOutputDTO callback { get; set; }

    }
}
