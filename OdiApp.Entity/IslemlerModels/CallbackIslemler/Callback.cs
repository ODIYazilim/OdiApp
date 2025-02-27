using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.IslemlerModels.CallbackIslemler
{
    [Table("Callback")]
    public class Callback : StringBaseModel
    {
        public string OpsiyonId { get; set; }
        public string CallbackSaatId { get; set; }
        public string ProjeId { get; set; }
        public string ProjeAdi { get; set; }
        public string ProjeRolId { get; set; }
        public string ProjeRolAdi { get; set; }
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }

        public bool MenajerGordu { get; set; }
        public bool PerformerGordu { get; set; }

        public bool TarihDegistirili { get; set; }
        public bool Onaylandi { get; set; }
        public bool MenajerOnayladi { get; set; }
        public bool PerformerOnayladi { get; set; }
        public bool Reddedildi { get; set; }
        public string? RedSebebi { get; set; }
        public bool MenajerReddetti { get; set; }
        public bool PerformerReddetti { get; set; }

        public bool Kapandi { get; set; }
        public DateTime CallbackKapanmaTarihi { get; set; }
        public DateTime CallbackGonderimTarihi { get; set; }

    }
}