using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    [Table("ProjeRolAnketSorulari")]
    public class ProjeRolAnketSorusu : StringBaseModel
    {
        public string ProjeRolId { get; set; }
        public string? Soru { get; set; }
        public int Sira { get; set; }
    }
}