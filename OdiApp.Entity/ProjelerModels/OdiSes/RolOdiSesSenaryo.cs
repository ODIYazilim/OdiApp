using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiSes
{
    [Table("RolOdiSesSenaryolar")]
    public class RolOdiSesSenaryo : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string SenaryoAdi { get; set; }
        public string Senaryo { get; set; } //html formatında
        public bool Dosyami { get; set; }
    }
}