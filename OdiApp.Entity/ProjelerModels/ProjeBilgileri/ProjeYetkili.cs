using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri
{
    [Table("ProjeYetkilileri")]
    public class ProjeYetkili : StringBaseModel
    {
        public int YetkiliTipi { get; set; }
        public string ProjeId { get; set; }
        public string? YetkiliId { get; set; }
        public string? YetkiliAdi { get; set; }
    }
}