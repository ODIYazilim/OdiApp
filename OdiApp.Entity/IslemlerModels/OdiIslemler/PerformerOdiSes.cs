using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.OdiIslemler
{
    [Table("PerformerOdiSesler")]
    public class PerformerOdiSes : StringBaseModel
    {
        public string PerformerOdiId { get; set; }
        public string Video { get; set; }
        public DateTime Tarih { get; set; }
    }
}