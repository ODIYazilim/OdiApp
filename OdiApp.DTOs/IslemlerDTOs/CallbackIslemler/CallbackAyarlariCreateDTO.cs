namespace OdiApp.DTOs.IslemlerDTOs.CallbackIslemler
{
    public class CallbackAyarlariCreateDTO
    {
        public string ProjeId { get; set; }
        public List<DateTime> CallbackTarihleri { get; set; }
        public DateTime BaslangicSaati { get; set; }
        public DateTime BitisSaati { get; set; }
        public int GorusmeAraligi { get; set; }
        public string GorusmeYeri { get; set; }
        public string GorusmeAdresi { get; set; }
    }
}
