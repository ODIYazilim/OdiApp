namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackOnaylaDTO
    {
        public string CallbackId { get; set; }
        public bool MenajerOnayladi { get; set; }
        public bool PerformerOnayladi { get; set; }
        public string YeniSaatId { get; set; }
        public bool CallbackSaatiDegistirildi { get; set; }
    }
}
