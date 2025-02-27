namespace OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler
{
    public class PerformerTakvimCreateDTO
    {
        public string PerformerId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string DolulukAciklamasi { get; set; }
        public int DolulukTuru { get; set; }
    }
}