using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    [Table("PerformerOdiVideolar")]
    public class PerformerOdiVideo : StringBaseModel
    {
        public string PerformerOdiId { get; set; }
        public string Video { get; set; }
        public DateTime Tarih { get; set; }
    }
}