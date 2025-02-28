using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    [Table("Diller")]
    public class Dil : BaseModel
    {
        public string Adi { get; set; }
        public bool Aktif { get; set; }
        public string Kisaltma { get; set; }
        public string BayrakLinki { get; set; }
    }
}