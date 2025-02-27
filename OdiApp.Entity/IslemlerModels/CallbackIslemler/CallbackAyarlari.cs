using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.CallbackIslemler
{
    [Table("CallbackAyarlari")]
    public class CallbackAyarlari : StringBaseModel
    {
        public string ProjeId { get; set; }
        public string CallbackTarihleri { get; set; }
        public DateTime BaslangicSaati { get; set; }
        public DateTime BitisSaati { get; set; }
        public int GorusmeAraligi { get; set; }
        public string GorusmeYeri { get; set; }
        public string GorusmeAdresi { get; set; }
    }
}