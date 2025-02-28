using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiVideo
{
    [Table("RolOdiVideolar")]
    public class RolOdiVideo : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string Baslik { get; set; }

        public List<RolOdiVideoDetay> DetayList { get; set; }
    }
}