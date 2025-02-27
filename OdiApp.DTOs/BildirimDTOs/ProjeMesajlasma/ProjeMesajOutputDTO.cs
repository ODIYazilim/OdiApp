namespace OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma
{
    public class ProjeMesajOutputDTO
    {
        public string ProjeMesajId { get; set; }
        public string Kullanici1Id { get; set; }
        public string Kullanici1AdSoyad { get; set; }
        public string Kullanici1ProfilResmi { get; set; } //presigned url olacak. Amazons3
        public string Kullanici1ProfilResmiDosyaYolu { get; set; }
        public string Kullanici2Id { get; set; }
        public string Kullanici2AdSoyad { get; set; }
        public string Kullanici2ProfilResmi { get; set; } //presigned url olacak. Amazons3
        public string Kullanici2ProfilResmiDosyaYolu { get; set; }
        public string PerformerId { get; set; }
        public string PerformerAdSoyad { get; set; }
        public string PerformerProfilResmi { get; set; } //presigned url olacak. Amazons3
        public string PerformerProfilResmiDosyaYolu { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeResmi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public int YeniMesajSayisi { get; set; }
        public DateTime SonMesajTarihi { get; set; }
        public string SonMesaj { get; set; } // eğer son mesaj text ise, son mesaj gelecek; eğer son mesaj dosya ise "Dosya Eki" yazılacak
    }
}