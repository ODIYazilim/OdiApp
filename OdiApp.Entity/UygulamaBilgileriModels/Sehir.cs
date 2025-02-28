using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    [Table("Sehirler")]
    public class Sehir : BaseModel
    {
        public string SehirAdi { get; set; }

        public List<Ilce> Ilceler { get; set; }
    }
}