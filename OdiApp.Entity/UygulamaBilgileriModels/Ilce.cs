using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    [Table("Ilceler")]
    public class Ilce : BaseModel
    {
        public int SehirId { get; set; }
        public string IlceAdi { get; set; }
    }
}