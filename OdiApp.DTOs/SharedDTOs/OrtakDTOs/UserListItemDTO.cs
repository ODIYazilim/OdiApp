using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.OrtakDTOs
{
    public class UserListItemDTO
    {
        public string KullaniciId { get; set; }
        public string AdSoyad { get; set; }
        public string ProfilFotografi { get; set; }
        public string ProfilFotografiDosyaYolu { get; set; }
        public List<string> KayitGrubuKoduListesi { get; set; }
        public string TelefonNumarasi { get; set; }
        public string MenajerId { get; set; }
        public string MenajerAdSoyad { get; set; }
    }
}