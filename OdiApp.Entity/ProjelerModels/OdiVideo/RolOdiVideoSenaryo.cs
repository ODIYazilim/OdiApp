using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiVideo
{
    [Table("RolOdiVideoSenaryolar")]
    public class RolOdiVideoSenaryo : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string SenaryoAdi { get; set; }
        public string Senaryo { get; set; } //html formatında
        public bool Dosyami { get; set; }
    }
}