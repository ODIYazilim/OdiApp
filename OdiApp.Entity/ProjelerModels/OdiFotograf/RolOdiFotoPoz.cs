using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiFotograf
{
    [Table("RolOdiFotoPozlar")]
    public class RolOdiFotoPoz : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string Poz { get; set; }
    }
}
