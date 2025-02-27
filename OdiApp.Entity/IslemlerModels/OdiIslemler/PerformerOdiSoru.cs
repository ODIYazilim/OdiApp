using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    [Table("PerformerOdiSorulari")]
    public class PerformerOdiSoru : StringBaseModel
    {
        public string PerformerOdiId { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public DateTime Tarih { get; set; }
    }
}