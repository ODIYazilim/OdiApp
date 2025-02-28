using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiVideo
{
    [Table("RolOdiVideoOrnekOyunlar")]
    public class RolOdiVideoOrnekOyun : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string OrnekOyun { get; set; }
        public string Baslik { get; set; }
    }
}