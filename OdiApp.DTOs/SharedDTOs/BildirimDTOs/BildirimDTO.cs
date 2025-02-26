namespace OdiApp.DTOs.SharedDTOs.BildirimDTOs
{
    public class BildirimDTO
    {
        public string Baslik { get; set; }
        public int BildirimTipi { get; set; }
        public string BildirimTipiAciklama { get; set; }
        public string Icerik { get; set; }
        public bool Okundu { get; set; }
        public string Fotograf { get; set; }
    }
}