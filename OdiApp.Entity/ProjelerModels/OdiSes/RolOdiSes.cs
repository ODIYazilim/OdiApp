using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiSes
{
    [Table("RolOdiSesler")]
    public class RolOdiSes : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string Replik { get; set; }
        public int Sure { get; set; }
        public int Sira { get; set; }
    }
}