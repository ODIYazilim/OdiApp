using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiSoru
{
    [Table("RolOdiSoruAciklamalar")]
    public class RolOdiSoruAciklama : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string SoruAciklama { get; set; }
    }
}