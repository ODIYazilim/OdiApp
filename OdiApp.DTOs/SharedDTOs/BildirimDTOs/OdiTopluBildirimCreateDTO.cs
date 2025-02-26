using System;
using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.BildirimDTOs
{
    public class OdiTopluBildirimCreateDTO
    {
        public string GonderenKullaniciId { get; set; }
        public string GonderenKullaniciAdSoyad { get; set; }
        public string AltBaslik { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string DosyaYolu { get; set; }
        public bool Okundu { get; set; }
        public int BildirimTipi { get; set; }
        public DateTime BildirimTarihi { get; set; }

        public bool OneSignalBildirimGonder { get; set; }

        public List<OdiTopluBildirimKullaniciDTO> GonderilecekKullanicilar { get; set; }
    }
}