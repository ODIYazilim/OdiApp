using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    [Table("PerformerOdiFotograflar")]
    public class PerformerOdiFotograf : StringBaseModel
    {
        public string PerformerOdiId { get; set; }
        public string Fotograf { get; set; }
        public string FotografAciklamasi { get; set; }
        public DateTime Tarih { get; set; }
    }
}