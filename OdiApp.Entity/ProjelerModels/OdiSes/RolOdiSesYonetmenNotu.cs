using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiSes
{
    [Table("RolOdiSesYonetmenNotlari")]
    public class RolOdiSesYonetmenNotu : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string YonetmenNotu { get; set; }
    }
}