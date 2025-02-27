using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.BildirimModels
{
    [Table("OdiBildirim")]
    public class OdiBildirim : StringBaseModel
    {
        public string KullaniciId { get; set; }
        public string KullaniciAdSoyad { get; set; } //
        public string GonderenKullaniciId { get; set; }//
        public string GonderenKullaniciAdSoyad { get; set; }// 
        public string AltBaslik { get; set; }//
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string DosyaYolu { get; set; }
        public bool Okundu { get; set; }
        public int BildirimTipi { get; set; }
    }
}