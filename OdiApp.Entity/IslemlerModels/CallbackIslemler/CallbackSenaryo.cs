using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.CallbackIslemler
{
    [Table("CallbackSenaryolari")]
    public class CallbackSenaryo : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string RolId { get; set; }
        public string Senaryo { get; set; }
    }
}