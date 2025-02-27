using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.CallbackIslemler
{
    [Table("CallbackNotlari")]
    public class CallbackNot : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string RolId { get; set; }
        public string Not { get; set; }
    }
}