using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler
{
    [Table("OpsiyonAnketSorulari")]
    public class OpsiyonAnketSorulari : StringBaseModel
    {
        public string OpsiyonId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
    }
}