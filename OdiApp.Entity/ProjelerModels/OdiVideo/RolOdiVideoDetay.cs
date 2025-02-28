using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiVideo
{
    [Table("RolOdiVideoDetaylar")]
    public class RolOdiVideoDetay : StringBaseModel
    {
        public string RolOdiVideoId { get; set; }
        public string SesDosyasi { get; set; }
        public string Replik { get; set; }
        public int Sure { get; set; }
        public int Sira { get; set; }
        public bool Dosyami { get; set; }
    }
}