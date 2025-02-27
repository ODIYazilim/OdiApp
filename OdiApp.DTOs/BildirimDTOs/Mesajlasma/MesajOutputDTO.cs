namespace OdiApp.DTOs.BildirimDTOs.Mesajlasma
{
    public class MesajOutputDTO
    {
        public string MesajId { get; set; }
        public string Kullanici1Id { get; set; }
        public string Kullanici1AdSoyad { get; set; }
        public string Kullanici1ProfilResmi { get; set; } //presigned url olacak. Amazons3
        public string Kullanici1ProfilResmiDosyaYolu { get; set; }
        public string Kullanici2Id { get; set; }
        public string Kullanici2AdSoyad { get; set; }
        public string Kullanici2ProfilResmi { get; set; } //presigned url olacak. Amazons3
        public string Kullanici2ProfilResmiDosyaYolu { get; set; }
        public int YeniMesajSayisi { get; set; }
        public DateTime SonMesajTarihi { get; set; }
        public string SonMesaj { get; set; } // eğer son mesaj text ise, son mesaj gelecek; eğer son mesaj dosya ise "Dosya Eki" yazılacak
    }
}