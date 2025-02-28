using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    [Table("KayitTurleri")]
    public class KayitTuru : BaseModel
    {
        public int DilId { get; set; }
        public string GrupKodu { get; set; }
        public string TurKodu { get; set; }
        public string Tur { get; set; }
        public bool Aktif { get; set; }
        public int Sira { get; set; }
    }
}