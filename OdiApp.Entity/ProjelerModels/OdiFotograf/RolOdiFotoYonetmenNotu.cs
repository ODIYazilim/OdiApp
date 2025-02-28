using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiFotograf
{
    [Table("RolOdiFotoYonetmenNotlari")]
    public class RolOdiFotoYonetmenNotu : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string YonetmenNotu { get; set; }
    }
}