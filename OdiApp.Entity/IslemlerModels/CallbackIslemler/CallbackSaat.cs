using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.CallbackIslemler
{
    [Table("CallbackSaatleri")]
    public class CallbackSaat : StringBaseModel
    {
        public string ProjeId { get; set; }
        public DateTime TarihSaat { get; set; }
        public bool Dolu { get; set; }
        public string? Not { get; set; }
        public bool Kilitli { get; set; }
    }
}