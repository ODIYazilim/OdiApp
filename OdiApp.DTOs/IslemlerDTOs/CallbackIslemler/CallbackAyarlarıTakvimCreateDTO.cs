namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackAyarlarıTakvimCreateDTO
    {
        public string ProjeId { get; set; }
        public CallbackAyarlariCreateDTO CallbackAyarlari { get; set; }
        public List<CallbackNotCreateDTO>? CallbackNotlari { get; set; }
        public List<CallbackSenaryoCreateDTO>? CallbackSenaryolari { get; set; } //senaryo dosya tipi 13
        public List<CallbackSaatCreateDTO> CallbackTakvimi { get; set; }
    }
}
