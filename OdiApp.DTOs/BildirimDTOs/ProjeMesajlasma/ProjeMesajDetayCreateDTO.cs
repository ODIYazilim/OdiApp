namespace OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma
{
    public class ProjeMesajDetayCreateDTO
    {
        public string? ProjeMesajId { get; set; } //frontend den Yeni mesajda bu alan gönderilmeyecek ,yeni mesaj detayda bu alan gönderilecek
        public string GonderenKullaniciId { get; set; }
        public string GonderilenKullaniciId { get; set; }
        public DateTime MesajGonderimTarihi { get; set; }
        public string? TextMesaj { get; set; }
        public string? DosyaMesaj { get; set; }
        public string DosyaTipi { get; set; } = "";
        public bool MesajDosyami { get; set; }
    }
}
