using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.BildirimModels
{
    [Table("OdiBildirimHerkes")]
    public class OdiBildirimHerkes : StringBaseModel
    {
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string DosyaYolu { get; set; }
        public int BildirimTipi { get; set; }
    }
}