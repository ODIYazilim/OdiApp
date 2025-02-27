namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackSaatOutputDTO
    {
        public string CallbackSaatId { get; set; }
        public string ProjeId { get; set; }
        public DateTime TarihSaat { get; set; }
        public bool Dolu { get; set; }
        public string Not { get; set; }
        public bool Kilitli { get; set; }

        public CallbackOutputDTO? Callback { get; set; }
    }
}
