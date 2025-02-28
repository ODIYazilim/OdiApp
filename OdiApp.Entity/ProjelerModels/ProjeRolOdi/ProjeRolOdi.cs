using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi
{
    [Table("ProjeRolOdileri")]
    public class ProjeRolOdi : StringBaseModel
    {
        public string ProjeRolId { get; set; }
        public DateTime SonOdilemeTarihi { get; set; }
    }
}