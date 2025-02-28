using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiVideo
{
    [Table("RolOdiVideoYonetmenNotlari")]
    public class RolOdiVideoYonetmenNotu : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string YonetmenNotu { get; set; }
    }
}