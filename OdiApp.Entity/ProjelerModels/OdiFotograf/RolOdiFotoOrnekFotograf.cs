using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiFotograf
{
    [Table("RolOdiFotoOrnekFotograflar")]
    public class RolOdiFotoOrnekFotograf : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string OrnekFoto { get; set; }
    }
}