namespace OdiApp.DTOs.PerformerDTOs.PerformerPuan
{
    public class PerformerPuanOutputDTO
    {
        public string PerformerId { get; set; }
        public decimal OrtalamaPuan { get; set; }
        public decimal IlgiCekicilikPuani { get; set; }
        public int IlgiCekicilikPuaniVerenSayisi { get; set; }
        public decimal BasariPuani { get; set; }
        public int BasariPuaniVerenSayisi { get; set; }
        public decimal YetenekPuani { get; set; }
        public int YetenekPuaniVerenSayisi { get; set; }
    }
}
